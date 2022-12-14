using System.Windows;
using System.Windows.Controls;
using Scheduler.Controls;

namespace Scheduler.Pages
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private void LoadEvents()
        {
            var lstEv = ((MainWindow)Application.Current.MainWindow).events;
            foreach(var l in lstEv)
            {

                var newControl = new EventControl();
                newControl.EventName.Text = l.name;
                newControl.EventTime.Text = $"{l.dateTime.Hour.ToString()}:{l.dateTime.Minute}";
                StackPanelEvents.Children.Add(newControl);
            }
        }
    }
}
