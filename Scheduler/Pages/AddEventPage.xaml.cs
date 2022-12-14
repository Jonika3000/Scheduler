using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Scheduler.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEventPage.xaml
    /// </summary>
    public partial class AddEventPage : Page
    {
        public AddEventPage()
        {
            InitializeComponent();
            PickerEventDate.SelectedDateTime = System.DateTime.Today;
        }

        private void ButtonSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!CheckErrors())
                return;
            var newEvent = new Event();
            newEvent.name = TextBoxEventName.Text;
            newEvent.dateTime = PickerEventDate.SelectedDateTime.Value;
            ((MainWindow)Application.Current.MainWindow).events.Add(newEvent);
            ((MainWindow)Application.Current.MainWindow).events =
                ((MainWindow)Application.Current.MainWindow).events.OrderBy(x => x.dateTime).ToList();
            Clear();
        }
        private void Clear()
        {
            TextBoxEventName.Text = string.Empty;
            PickerEventDate.SelectedDateTime = DateTime.Today;
        }
        private bool CheckErrors()
        {
            if (TextBoxEventName.Text == string.Empty)
            {
                ErrorString("The name field cannot be empty");
                return false;
            }
            else if (PickerEventDate.SelectedDateTime == null)
            {
                ErrorString("Event date not selected");
                return false;
            }
            else if (PickerEventDate.SelectedDateTime.Value.Date < DateTime.Now)
            {
                ErrorString("Can't put past date");
                return false;
            }
            return true;
        }
        private void ErrorString(string error)
        {
            TextBlockError.Text = error;
            TextBlockError.Visibility = Visibility.Visible;
            Clear();
        }
    }
}
