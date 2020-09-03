using Hotel_Automatization.Classes;
using Hotel_Automatization.Classes.DataBaseViews;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WebClientWf.Controllers;

namespace Hotel_Automatization
{
    /// <summary>
    /// Логика взаимодействия для WorkingWindow.xaml
    /// </summary>
    public partial class WorkingWindow : Window
    {
        // Получение данных и выполнение запросов
        private CountryController _myController;

        public WorkingWindow()
        {
            InitializeComponent();

            _myController = new CountryController(this);
        }

        //Получить файл для парсинга от restcountries.eu
        private async void SendRequest(object sender, ExecutedRoutedEventArgs e)
        {
            MainDg.ItemsSource = null;

            _myController.SetCountry(Param.Text);
            InfoTxblc.Text = "Выполнение запроса!";

            bool work = await _myController.DownloadData();

            _myController.WorkWithData(work);

            //Запросить сохранение данных в бд.
            if (work)
                AskAndSaveToDb();
        }

        //Запросить сохранение данных в бд.
        private void AskAndSaveToDb()
        {
            MessageBoxResult res = 
                MessageBox.Show("Хотели бы Вы сохранить данные?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //Не производим сохранение при отказе пользователя
            if (res == MessageBoxResult.No)
                return;

            //произведем сохранение данных полученной страны в бд
            try
            {
                dynamic x = MainDg.Items[0];
                SaveToDb(x);
            }
            catch(Exception ex) { InfoTxblc.Text = "Данные отсутствуют."; }
        }

        private void SaveToDb(dynamic country)
        {
            InfoTxblc.Text = "Сохраняем данные...";
            CityView capital = null;
            RegionView region = null;


            //Проверка наличия столицы в таблице Города
            if (Querries.FindCities(country.Capital).Count == 0)
            {
                capital = new CityView() { Name = country.Capital };
                Querries.SaveCity(capital);
            }
            //Если запись есть - ищем ее
            else
                capital = Querries.FindCity(country.Capital);


            //Проверка наличия региона в таблице Регионов
            if (Querries.FindRegions(country.Region).Count == 0)
            {
                region = new RegionView() { Name = country.Region };
                Querries.SaveRegion(region);
            }
            //Если запись есть - ищем ее
            else
                region = Querries.FindRegion(country.Region);


            //Проверка наличия страны в таблице Стран
            if (Querries.FindCountries(country.CountryName).Count == 0)
            {
                //Capital_Id нужен по ТЗ. В ином случае, не стал бы добавлять это поле
                Querries.SaveCountry(new CountryView()
                    { Name = country.CountryName, Capital = capital, Capital_Id = capital.Id, Code = country.Code,
                      PeopleAmount = country.People, Region = region, Square = country.Square
                }
                );
            }
            //Если запись есть - обновляем ее
            else
            {
                CountryView selectedCountry = Querries.FindCountries(country.CountryName as string).First();

                selectedCountry.Name =          country.CountryName;
                selectedCountry.Capital =       capital;
                selectedCountry.Capital_Id =    capital.Id;
                selectedCountry.Code =          country.Code;
                selectedCountry.PeopleAmount =  country.People;
                selectedCountry.Region =        region;
                selectedCountry.Square =        country.Square;

                Querries.db.SaveChanges();
            }

                InfoTxblc.Text = "Сохранение прошло успешно!";
        }

        //Для корректного отображения успешности асинхронных потоков
        public void ChangeWindow(string text) =>
              Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                        (ThreadStart)(() => InfoTxblc.Text = text));

        public void SetDataGrid(IEnumerable countryInfo) =>
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                        (ThreadStart)(() => MainDg.ItemsSource = countryInfo));

        //Очистка ресурсов приложения
        private void DisposeDb(object sender, System.ComponentModel.CancelEventArgs e) =>
            Querries.db.Dispose();

        //очистить место для ввода нового параметра запроса
        private void ClearOldParam(object sender, RoutedEventArgs e) =>
            (sender as TextBox).Text = "";

        //показать все сохраненные страны
        private void ShowData(object sender, ExecutedRoutedEventArgs e) =>
            MainDg.ItemsSource = Querries.ShowAllData();

    }
}
