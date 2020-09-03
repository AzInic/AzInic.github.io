using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Automatization.Classes.DataBaseViews
{
    public class CityView
    {
        //Id города
        public int Id { get; set; }

        //Столицей какой страны является этот город
        public virtual CountryView Country { get; set; }

        //название города
        public string Name { get; set; }
    }
}
