using Notification.Wpf;
using Notification.Wpf.Classes;
using Scheduler.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Scheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Event> events = new List<Event>();
        public List<Event> eventsToday = new List<Event>();
        public string songPath;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            CreateListToday();
            clearEventsOverdue();
            Container.Navigate(new HomePage());
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromHours(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            var timerNotification = new DispatcherTimer();
            timerNotification.Interval = TimeSpan.FromMinutes(1);
            timerNotification.Tick += TimerNotification_Tick;
            timerNotification.Start();
        }

        private void TimerNotification_Tick(object? sender, EventArgs e)
        {
            foreach (var ev in eventsToday)
            {
                if (ev.dateTime.Hour == DateTime.Now.Hour && ev.dateTime.Minute == DateTime.Now.Minute)
                {
                    ShowNotification(ev);
                }
                if (eventsToday.Count == 0)
                    break;
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            CreateListToday();
        }

        public void CreateListToday()
        {
            eventsToday.Clear();
            foreach (var e in events)
            {
                if (e.dateTime.Date == DateTime.Today.Date)
                {
                    eventsToday.Add(e);

                }
            }
        }
        private void clearEventsOverdue()
        {
            foreach (var e in events)
            {
                if (e.dateTime < DateTime.Now)
                {
                    events.Remove(e);
                    
                }
                if (events.Count == 0)
                    break;
            }
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
        private void LoadData()
        {
            try
            {
                using (FileStream fs = new FileStream("events.json", FileMode.OpenOrCreate))
                {
                    events = JsonSerializer.Deserialize<List<Event>>(fs);
                }
                using (FileStream fs = new FileStream("song.json", FileMode.OpenOrCreate))
                {
                    songPath = JsonSerializer.Deserialize<string>(fs);
                }
                if(songPath == string.Empty || songPath == null)
                {
                    songPath = "default";
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
            using (FileStream fs = new FileStream("song.json", FileMode.Create))
            {
                JsonSerializer.Serialize<string>(fs, songPath);
            }
        }
        private void ShowNotification(Event e)
        {
            var notificationManager = new NotificationManager();
            var content = new NotificationContent
            {
                Title = "Reminder",
                Message = e.name,
                Type = NotificationType.Notification,
                 RowsCount = 4,  
                 CloseOnClick = true,  
                Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF3959CB"),
                Foreground = new SolidColorBrush(Colors.White), 
                Image = new NotificationImage()
                {
                    Source = new BitmapImage(new Uri("Resources\\icons8_house_lannister_480px.png", UriKind.RelativeOrAbsolute)),
                    Position = ImagePosition.Top
                }

            }; 
            notificationManager.Show(content);
            SoundPlayer player = new SoundPlayer(); 
            player.SoundLocation = songPath;
            try
            {
                player.PlaySync();
            } 
            catch(Exception ex) 
            {
                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                player.SoundLocation = projectDirectory + "\\Sounds\\NotificationSound.wav";
                player.PlaySync();
            } 
            clearEventsOverdue();
            CreateListToday();
            UpdatePageHome();

        } 
        public void RemoveEvent(string tagBorder,string nameEvent)
        {
            events.Remove(events.Where(e => e.dateTime.ToString("dddd, dd MMMM yyyy HH:mm:ss") == tagBorder &&
            e.name == nameEvent).FirstOrDefault());
            UpdatePageHome();
        }
        private void UpdatePageHome()
        {
            if (Container.Content != null)
            {
                if ((Container.Content as Page).Title == "HomePage")
                {
                    Container.Navigate(new HomePage());
                }
            }
        }

        private void RButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            Container.Navigate(new SettingsPage());
        }
    }
}
