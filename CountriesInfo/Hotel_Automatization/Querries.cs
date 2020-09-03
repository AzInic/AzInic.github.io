using Hotel_Automatization.Classes;
using Hotel_Automatization.Classes.DataBaseViews;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Automatization
{
    public static class Querries
    {
        //Контекст для работы с бд
        public static DatabaseContext db;

        public static void SaveCity(CityView city)
        {
            db.Cities.Add(city);
            db.SaveChanges();
        }

        public static void SaveRegion(RegionView region)
        {
            db.Regions.Add(region);
            db.SaveChanges();
        }

        public static void SaveCountry(CountryView country)
        {
            db.Countries.Add(country);
            db.SaveChanges();
        }

        //Поиск городов по имени
        public static List<CityView> FindCities(string name) =>
            (from it in db.Cities
             where it.Name == name
             select it).ToList();

        //Поиск города по имени
        public static CityView FindCity(string name) =>
            (from it in db.Cities
             where it.Name == name
             select it).First();

        //Поиск регионов по имени
        public static List<RegionView> FindRegions(string name) =>
            (from it in db.Regions
             where it.Name == name
             select it).ToList();

        //Поиск региона по имени
        public static RegionView FindRegion(string name) =>
            (from it in db.Regions
             where it.Name == name
             select it).First();

        //Поиск страны по имени
        public static List<CountryView> FindCountries(string name) =>
            (from it in db.Countries
             where it.Name == name
             select it).ToList();

        public static IEnumerable ShowAllData()=>
            (from it in db.Countries
             select new
             {
                 CountryName = it.Name,
                 Code = it.Code,
                 Capital = it.Capital.Name,
                 Square = it.Square,
                 People = it.PeopleAmount,
                 Region = it.Region.Name
             }).ToList();
    }
}
