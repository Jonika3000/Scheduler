using Notification.Wpf;
using Notification.Wpf.Classes;
using Scheduler.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text.Json;
using System.Windows;
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

        public MainWindow()
        {
            InitializeComponent();
            LoadEvents();
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
                    if (events.Count == 0)
                        break;
                }
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
        private void ShowNotification(Event e)
        {
            var notificationManager = new NotificationManager();
            var content = new NotificationContent
            {
                Title = "Reminder",
                Message = e.name,
                Type = NotificationType.Notification,
                /*TrimType = NotificationTextTrimType.Attach, */// will show attach button on message
                RowsCount = 4, //Will show 3 rows and trim after
                //LeftButtonAction = () => SomeAction(), //Action on left button click, button will not show if it null 
                //RightButtonAction = () => SomeAction(), //Action on right button click,  button will not show if it null
                //LeftButtonContent, // Left button content (string or what u want
                //RightButtonContent, // Right button content (string or what u want
                CloseOnClick = true, // Set true if u want close message when left mouse button click on message (base = true) 
                Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF3959CB"),
                Foreground = new SolidColorBrush(Colors.White),
                 
                //Icon = new SvgAwesome()
                //{
                //    Icon = EFontAwesomeIcon.Regular_Star,
                //    Height = 25,
                //    Foreground = new SolidColorBrush(Colors.Yellow)
                //},

                Image = new NotificationImage()
                {
                    Source = new BitmapImage(new Uri("Resources\\icons8_house_lannister_480px.png", UriKind.RelativeOrAbsolute)),
                    Position = ImagePosition.Top
                }

            }; 
            notificationManager.Show(content);
            SoundPlayer player = new SoundPlayer();
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            player.SoundLocation = projectDirectory + "\\Sounds\\NotificationSound.wav";
            player.PlaySync(); 
            clearEventsOverdue();
        } 
    }
}
