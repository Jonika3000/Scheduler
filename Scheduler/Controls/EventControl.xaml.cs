using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Scheduler.Controls
{
    /// <summary>
    /// Логика взаимодействия для EventControl.xaml
    /// </summary>
    public partial class EventControl : UserControl
    {
        public EventControl()
        {
            InitializeComponent();
            SetRandomColor();
        }
        private void SetRandomColor()
        {
            var bc = new BrushConverter();
            var rnd = new Random().Next(0, 4);
            switch (rnd)
            {
                case 0:
                    MainBorder.Background = (Brush)bc.ConvertFrom("#F6BC7B");
                    break;
                case 1:
                    MainBorder.Background = (Brush)bc.ConvertFrom("#F6F67B");
                    break;
                case 2:
                    MainBorder.Background = (Brush)bc.ConvertFrom("#7BF68C");
                    break;
                case 3:
                    MainBorder.Background = (Brush)bc.ConvertFrom("#CD7BF6");
                    break;
                case 4:
                    MainBorder.Background = (Brush)bc.ConvertFrom("#7BC6F6");
                    break;
            }
        }
    }
}
