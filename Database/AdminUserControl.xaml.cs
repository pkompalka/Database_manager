using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Oracle.DataAccess.Client;

namespace Database
{
    /// <summary>
    /// Interaction logic for AdminUserControl.xaml
    /// </summary>
    public partial class AdminUserControl : UserControl
    {
        private OracleConnection new_connection = new OracleConnection();
        private OracleCommand control_manager_dialog;
        private OracleDataAdapter data_adapter;
        private OracleCommandBuilder command_builder;
        private DataSet choose_data;
        private DataView show_data;

        public AdminUserControl()
        {
            Application.Current.MainWindow.Width = 1200;
            Application.Current.MainWindow.Height = 500;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new_connection.Close();
                new_connection.ConnectionString = "User Id=" + "piotr" + ";Password=" + "piotr"
                + ";Data Source=" + "oracle" + ";";
                new_connection.Open();

                myCombo.Items.Clear();
                string sql = sqlBox.Text;

                control_manager_dialog = new OracleCommand(sql, new_connection);
                control_manager_dialog.CommandType = System.Data.CommandType.Text;

                OracleDataReader dr = control_manager_dialog.ExecuteReader();

                data_adapter = new OracleDataAdapter(control_manager_dialog);
                command_builder = new OracleCommandBuilder(data_adapter);
                choose_data = new DataSet();

                data_adapter.Fill(choose_data);
                myGrid.DataContext = choose_data.Tables[0];

                MessageBox.Show("Zawartość bazy:\n-liczba atrybutow:\t" + choose_data.Tables[0].Columns.Count
                + "\n-liczba rekordów:\t" + choose_data.Tables[0].Rows.Count, "Informacja");
            }
            catch(OracleException ex)
            {
                switch(ex.Number)
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                try
                {
                    
                    for(int i = 0; i < choose_data.Tables[0].Columns.Count; i++)
                    {
                        myCombo.Items.Add(choose_data.Tables[0].Columns[i].ToString());
                    }
                }
                catch
                {

                }
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            show_data = new DataView(choose_data.Tables[0], myCombo.Text + " = '"
            + findBox.Text + "' ", myCombo.Text + " Desc", DataViewRowState.CurrentRows);
            myGrid.ItemsSource = show_data;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                show_data = new DataView();
                show_data.Table = choose_data.Tables[0];
                show_data.RowFilter = askBox.Text;
                myGrid.ItemsSource = show_data;
            }
            catch
            {

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

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            myGrid.ItemsSource = choose_data.Tables[0].DefaultView;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            LoginUserControl u = new LoginUserControl();
            this.Content = u;
        }
    }
}
