using Ookii.Dialogs.Wpf;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Scheduler.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            TextBoxSongName.Text = ((MainWindow)System.Windows.Application.Current.MainWindow).songPath;
        }

        private void TextBoxSongName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dialog = new VistaOpenFileDialog { Filter = @"wav files only|*.wav" };
            var showDialog = dialog.ShowDialog(System.Windows.Application.Current.MainWindow);
            if (showDialog != null && (bool)showDialog)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).songPath = dialog.FileName;
                TextBoxSongName.Text = dialog.FileName;
            }
        }

        private void ButtonGit_Click(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = "https://github.com/Jonika3000";
            p.Start();
        }
    }
}
