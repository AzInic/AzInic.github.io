using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using Newtonsoft.Json.Converters;

namespace Hotel_Automatization.Classes
{
    //генерация классов необходимых для работы
    //тут: https://app.quicktype.io/#l=cs&r=json2csharp
    public class Country
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("topLevelDomain")]
        public string[] TopLevelDomain { get; set; }

        [JsonProperty("alpha2Code")]
        public string Alpha2Code { get; set; }

        [JsonProperty("alpha3Code")]
        public string Alpha3Code { get; set; }

        [JsonProperty("callingCodes")]
        [JsonConverter(typeof(DecodeArrayConverter))]
        public long[] CallingCodes { get; set; }

        [JsonProperty("capital")]
        public string Capital { get; set; }

        [JsonProperty("altSpellings")]
        public string[] AltSpellings { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("subregion")]
        public string Subregion { get; set; }

        [JsonProperty("population")]
        public int Population { get; set; }

        [JsonProperty("latlng")]
        public long[] Latlng { get; set; }

        [JsonProperty("demonym")]
        public string Demonym { get; set; }

        [JsonProperty("area")]
        public double Area { get; set; }

        [JsonProperty("gini")]
        public double Gini { get; set; }

        [JsonProperty("timezones")]
        public string[] Timezones { get; set; }

        [JsonProperty("borders")]
        public string[] Borders { get; set; }

        [JsonProperty("nativeName")]
        public string NativeName { get; set; }

        [JsonProperty("numericCode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long NumericCode { get; set; }

        [JsonProperty("currencies")]
        public Currency[] Currencies { get; set; }

        [JsonProperty("languages")]
        public Language[] Languages { get; set; }

        [JsonProperty("translations")]
        public Translations Translations { get; set; }

        [JsonProperty("flag")]
        public Uri Flag { get; set; }

        [JsonProperty("regionalBlocs")]
        public object[] RegionalBlocs { get; set; }

        [JsonProperty("cioc")]
        public string Cioc { get; set; }

        public static Country FromJson(string json) => JsonConvert.DeserializeObject<Country[]>(json, Converter.Settings)[0];
    }
}
