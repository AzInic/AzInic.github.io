using Hotel_Automatization.Classes.DataBaseViews;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Automatization.Classes
{
    class CustomInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        Random rand = new Random();
        protected override void Seed(DatabaseContext context)
        {
            base.Seed(context);

            //Для гибкости разработки.
            //В будущем возможна начальная инициализация бд.
            //Querries.SaveCity(new CityView() { Name = "Тверь" });
        }
    }
}
