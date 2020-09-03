using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Automatization.Classes.DataBaseViews
{
    public class CountryView
    {

        //Id страны
        public int Id { get; set; }

        //Столица страны
        public virtual CityView Capital { get; set; }

        //Id столицы. Требуется по заданию.
        //В другом случае не стал бы вносить в класс
        public int Capital_Id { get; set; }

        //Регион страны.
        public virtual RegionView Region { get; set; }

        //название страны
        public string Name { get; set; }

        //код страны
        public string Code { get; set; }

        //Площадь страны
        public double Square { get; set; }

        //Население
        public int PeopleAmount { get; set; }
    }
}
