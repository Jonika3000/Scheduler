using Scheduler.Pages;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;


namespace Scheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Event> events = new List<Event>();
        public MainWindow()
        {
            InitializeComponent();
            LoadEvents();
            Container.Navigate(new HomePage());
        }
        private void Button_Clicl_Minimize(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Button_Click_Max(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void Button_Click_Stop(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void RButtonHome_Checked(object sender, RoutedEventArgs e)
        {
            Container.Navigate(new HomePage());
        }

        private void RButtonClock_Checked(object sender, RoutedEventArgs e)
        {
            Container.Navigate(new AddEventPage());
        }
        private void LoadEvents()
        {
            try
            {
                using (FileStream fs = new FileStream("events.json", FileMode.OpenOrCreate))
                {
                    events = JsonSerializer.Deserialize<List<Event>>(fs);
                }
            }
            catch
            {

            }

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (FileStream fs = new FileStream("events.json", FileMode.Create))
            {
                JsonSerializer.Serialize<List<Event>>(fs, events);
            }
        }
    }
}
