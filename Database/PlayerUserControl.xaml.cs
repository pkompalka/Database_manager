using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Database
{
    /// <summary>
    /// Interaction logic for PlayerUserControl.xaml
    /// </summary>
    public partial class PlayerUserControl : UserControl
    {
        private OracleConnection new_connection = new OracleConnection();
        private OracleCommand control_manager_dialog;
        private OracleDataAdapter data_adapter;
        private OracleCommandBuilder command_builder;
        private DataSet choose_data;
        private DataView show_data;

        public PlayerUserControl(int tmp)
        {
            Application.Current.MainWindow.Width = 1000;
            Application.Current.MainWindow.Height = 150;
            InitializeComponent();

            try
            {
                new_connection.Close();
                new_connection.ConnectionString = "User Id=" + "piotr" + ";Password=" + "piotr"
                + ";Data Source=" + "oracle" + ";";
                new_connection.Open();

                string sql = "SELECT * FROM ZAWODNICY";

                control_manager_dialog = new OracleCommand(sql, new_connection);
                control_manager_dialog.CommandType = System.Data.CommandType.Text;

                OracleDataReader dr = control_manager_dialog.ExecuteReader();

                data_adapter = new OracleDataAdapter(control_manager_dialog);
                command_builder = new OracleCommandBuilder(data_adapter);
                choose_data = new DataSet();

                data_adapter.Fill(choose_data);
                myGrid.DataContext = choose_data.Tables[0];
                try
                {
                    show_data = new DataView();
                    show_data.Table = choose_data.Tables[0];
                    show_data.RowFilter = "ID_ZAWODNIKA=" + tmp;
                    myGrid.ItemsSource = show_data;
                }
                catch
                {

                }

            }
            catch (OracleException ex)
            {
                switch (ex.Number)
                {
                    case 1:
                        MessageBox.Show("Error attempting to insert duplicate data");
                        break;
                    case 2:
                        MessageBox.Show("Database unavailable");
                        break;
                    case 3:
                        MessageBox.Show("Another error" + ex.Message.ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                data_adapter.Update(choose_data.Tables[0]);
            }
            catch
            {

            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            LoginUserControl u = new LoginUserControl();
            this.Content = u;
        }
    }
}
