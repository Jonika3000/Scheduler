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
            PickerEventDate.SelectedDateTime = System.DateTime.Now;
        }

        private void ButtonSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckErrors();
            var newEvent = new Event();
            newEvent.name = TextBoxEventName.Text;
            newEvent.dateTime = PickerEventDate.SelectedDateTime.Value;
            ((MainWindow)Application.Current.MainWindow).events.Add(newEvent);
            ((MainWindow)Application.Current.MainWindow).events =
                ((MainWindow)Application.Current.MainWindow).events.OrderBy(x => x.dateTime).ToList();
        }
        private void CheckErrors()
        {
            if (TextBoxEventName.Text == string.Empty)
                ErrorString("The name field cannot be empty");
            else if (PickerEventDate.SelectedDateTime == null)
                ErrorString("Event date not selected");
        }
        private void ErrorString(string error)
        {
            TextBlockError.Text = error;
            TextBlockError.Visibility = Visibility.Visible;
        }
    }
}
