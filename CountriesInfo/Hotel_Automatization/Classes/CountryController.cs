using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

// https://app.quicktype.io/#l=cs&r=json2csharp
using System.Globalization;
using System.Threading;
using Hotel_Automatization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Hotel_Automatization.Classes;
using System.Collections;
using Hotel_Automatization.Classes.DataBaseViews;

namespace WebClientWf.Controllers
{
    public class CountryController
    {
        //Ввод пользователя
        private string _countryStr;

        //Полученные данные
        private List<Country> gotCountries;

        private WebClient _webClient;
        private string _uri;
        private string _fileName;           // файл со странами

        WorkingWindow _mainWin;             //окно отображения информации

        public CountryController() : this(null) { }

        public CountryController(WorkingWindow win, string uri = "https://restcountries.eu/rest/v2/name/USA",
            string fileName = @"..\..\country.json")
        {
            _webClient = new WebClient();
            _uri = uri;
            _fileName = fileName;
            _mainWin = win;

            gotCountries = new List<Country>();
        }

        //Выбор интересующей страны
        public void SetCountry(string name)
        {
            _countryStr = name;

            //Для создания ситуации когда сервис не доступен
            //_uri = "https://restcouвыntries.eu/rest/v2/name/" + _countryStr;
            _uri = "https://restcountries.eu/rest/v2/name/" + _countryStr;
        }

        /*
         Класс CountryController: логика отображения данных перемешана с логикой обращения 
         к внешнему сервису в рамках одного класса (а иногда и в рамках одного метода, напр.
         DownloadData, что не соответствует принципу S из SOLID).
        */

        // Разделил метод DownLoadData на два. 
        // Не совсем представляю как разделить логику обращения к сервису 
        // с логикой отображения данных.

        // В моем понимании, этот класс является чем-то в роде ModelView,
        // что соответсвует паттерну MVVM. 

        //Получением данных занимается один класс (WebClient),
        //Отображением данных - методы другого класса (ChangeWindow)

        //Возможно я ошибаюсь, но у меня нет идей как корректно 
        //поступить с точки зрения архитектуры в данном случае

        // Загрузка файла, получение коллекции данных из JSON
        public async Task<bool> DownloadData()
        {
            try
            {
                await _webClient.DownloadFileTaskAsync(_uri, _fileName);

            } 
            catch(WebException ex1)
            {
                if(ex1.Message != "Удаленный сервер возвратил ошибку: (404) Не найден.")
                    _mainWin.ChangeWindow($"Ошибка подключения. {ex1.Message}");
                else
                    _mainWin.ChangeWindow($"Страна < {_countryStr} > не найдена.");
                return false;
            }

            return true;
        } // DownloadData

        public void WorkWithData(bool work)
        {
            if (!work)
                return;

            string jsonString = File.ReadAllText(_fileName);

            // десериализация JSON 
            gotCountries.Clear();
            gotCountries.Add(Country.FromJson(jsonString));

            //Нас интересует последняя страна
            _mainWin.MainDg.ItemsSource =
                                (from it in gotCountries
                                 select new
                                 {
                                     CountryName = it.Name,

                                     //По заданию это стринг
                                     Code = $"{it.CallingCodes[0]}",
                                     Capital = it.Capital,
                                     Square = it.Area,
                                     People = it.Population,
                                     Region = it.Region
                                 });

            _mainWin.ChangeWindow($"Запрос обработан. Ответ получен.");
        }
    }// class RateController
}
