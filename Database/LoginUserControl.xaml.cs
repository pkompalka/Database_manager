using System.Windows;
using System.Windows.Controls;

namespace Database
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        public LoginUserControl()
        {
            Application.Current.MainWindow.Width = 400;
            Application.Current.MainWindow.Height = 300;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (logBox.Text == "admin" && passBox.Password == "admin")
            {
                AdminUserControl u1 = new AdminUserControl();
                this.Content = u1;
            }
            else if (logBox.Text == "zawodnik1" && passBox.Password == "zawodnik1")
            {
                PlayerUserControl u2 = new PlayerUserControl(1);
                this.Content = u2;
            }
            else if (logBox.Text == "zawodnik2" && passBox.Password == "zawodnik2")
            {
                PlayerUserControl u3 = new PlayerUserControl(2);
                this.Content = u3;
            }
            else
            {
                MessageBox.Show("Błędne dane");
                logBox.Clear();
                passBox.Clear();
            }
        }
    }
}
