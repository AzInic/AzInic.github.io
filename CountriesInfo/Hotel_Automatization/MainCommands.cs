using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel_Automatization
{
    public class MainCommands
    {
        public static RoutedCommand SendRequest { get; set; }
        public static RoutedCommand ShowAllInfo { get; set; }

        // статический конструктор - для привязки команд,
        // т.е. для назначения им исполнителей
        static MainCommands()
        {
            SendRequest = new RoutedCommand("SendRequest", typeof(MainWindow));
            ShowAllInfo = new RoutedCommand("ShowAllInfo", typeof(MainWindow));
        }
    }
}
