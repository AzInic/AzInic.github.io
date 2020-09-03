using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Automatization.Classes.DataBaseViews
{
    public class RegionView
    {
        public RegionView()
        {
            Countries = new HashSet<CountryView>();
        }

        //Id региона
        public int Id { get; set; }

        //Страны этого региона
        public virtual ICollection<CountryView> Countries { get; set; }

        //название региона
        public string Name { get; set; }

    }
}
