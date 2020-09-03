using Hotel_Automatization.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel_Automatization
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer timer;

        public MainWindow()
        {
            timer = new Timer(TimerJob);
            timer.Change(100, 500);

            InitializeComponent();
            GenerateDb();
        }

        async Task GenerateDb()
        {
            Database.SetInitializer(new CustomInitializer());

            bool success = await Task<bool>.Run(() =>
                {
                    try
                    {
                        Querries.db = new DatabaseContext();
                        Querries.db.Database.Initialize(true);

                        //Типо подключение не прошло
                        //throw new Exception();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    return true;
                });


            timer.Dispose();
            this.Hide();

            CheckIfCrushed(success);

            WorkingWindow win = new WorkingWindow();
            win.Show();

            //окно висит 
            this.Close();
        }

        //Если подключение к бд не прошло
        private void CheckIfCrushed(bool flag)
        {
            if (!flag)
            {
                MessageBox.Show("Что-то пошло не так!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        //обертка для таймера
        private void TimerJob(object obj) =>
            UpdateLoading();

        //для добавления точек к "Загрузка."
        public void UpdateLoading() =>
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                (ThreadStart)(() => {
                    string text = LoadingTxb.Text;
                    LoadingTxb.Text = text == "Загрузка."? "Загрузка.." : text == "Загрузка.." ? "Загрузка..." : "Загрузка."; 
                }));

        // обработчик нажатия ЛКМ для перемещения окна
        private void WindowMovingEngine(object sender, MouseButtonEventArgs e) =>
            DragMove();
    }
}
