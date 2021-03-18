using MahApps.Metro.Controls;

namespace Database
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            LoginUserControl u = new LoginUserControl();
            this.Content = u;
        }
    }
}
