using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Scheduler.Controls
{
    /// <summary>
    /// Логика взаимодействия для EventControl.xaml
    /// </summary>
    public partial class EventControl : UserControl
    {
        List<string> colors = new List<string> { "#C19EBB" , "#9EB5C1" , "#C19E9E", "#73C15A", "#C15A5A",
        "#C18D5A", "#5AC191", "#5AC1BC"};
        public EventControl(string Tag)
        {
            InitializeComponent();
            SetRandomColor();
            MainBorder.Tag = Tag;
        }
        private void SetRandomColor()
        {
            var bc = new BrushConverter();
            MainBorder.Background = (Brush)bc.ConvertFrom(colors[new Random().Next(colors.Count)]); 
        }

        private void StackPanel_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var b = sender as Border;
            b.ContextMenu.Items.Clear();

            MenuItem newQuery = new MenuItem();
            newQuery.Header = "Remove";
            newQuery.Cursor = Cursors.Hand;
            newQuery.Click += NewQuery_Click; 

            b.ContextMenu.Items.Add(newQuery);

            //if (b.ContextMenu.Style == null)
            //    b.ContextMenu.Style = (Style)(((MainWindow)Application.Current.MainWindow)).FindResource("ContextMenuStyle") as Style;

            b.ContextMenu.IsOpen = true;
        }

        private void NewQuery_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).RemoveEvent(MainBorder.Tag.ToString(), EventName.Text);
        }
    }
}
