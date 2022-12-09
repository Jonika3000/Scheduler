using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Scheduler.Controls
{
    /// <summary>
    /// Логика взаимодействия для HomeButton.xaml
    /// </summary>
    public partial class HomeButton : UserControl
    {
        public HomeButton()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Line.Visibility = Visibility.Visible;
           
        }

        private void RadioBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            Line.Visibility = Visibility.Hidden;
        }
    }
}
