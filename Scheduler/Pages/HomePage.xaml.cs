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
            StackPanelEvents.Children.Clear();
            LoadEvents();
            NoEventsStackPanel();
        }
        private void NoEventsStackPanel()
        {
            if (StackPanelEvents.Children.Count == 0)
            {
                var txtBlock = new TextBlock();
                txtBlock.Style = Resources["EventDay"] as Style;
                txtBlock.Text = "No events yet...";
                txtBlock.HorizontalAlignment = HorizontalAlignment.Center;
                StackPanelEvents.Children.Add(txtBlock);
            } 
        }
        private void LoadEvents()
        {
            var lstEv = ((MainWindow)Application.Current.MainWindow).events;
            for(int i = 0; i < lstEv.Count; i++)
            { 
                if(i>0)
                {
                    if (lstEv[i].dateTime.Date != lstEv[i-1].dateTime.Date)
                        StackPanelEvents.Children.Add(CreateTxtBlock(lstEv[i]));
                }
                else
                {
                    StackPanelEvents.Children.Add(CreateTxtBlock(lstEv[i])); 
                }
                var newControl = new EventControl();
                newControl.EventName.Text = lstEv[i].name;
                newControl.EventTime.Text = lstEv[i].dateTime.ToString("h:mm tt");
                StackPanelEvents.Children.Add(newControl);
            }
        }

        private TextBlock CreateTxtBlock(Event ev)
        {
            var txtBlock = new TextBlock();
            txtBlock.Style = Resources["EventDay"] as Style;
            txtBlock.Text = ev.dateTime.ToString("dddd, dd MMMM yyyy");
            return txtBlock;
        }
    }
}
