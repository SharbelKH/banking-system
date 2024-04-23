using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BankApplication.logic;


namespace BankApplication
{



    //  Transaction
    // Deposit
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User logged_in_user;
        public MainWindow(string accountId)
        {
            this.logged_in_user = new User(accountId);
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Transfer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Deposit_Click(object sender, RoutedEventArgs e)
        {
            
            string depositAmmount = DepositAmount.Text;
            string ConString = OurSqlConnectionString.ConString;
            
            // Deposit is larger than 10 000 and therefore false
            if (!logged_in_user.Deposit(double.Parse(depositAmmount)))
            {
                // Maybe window saying its not possible to deposit this ammount?
                return;
            }

            try
            {
                using (SqlConnection Con = new SqlConnection(ConString))
                {
                    string query = "UPDATE Account SET Balance = Balance + @new_balance WHERE Id = @AccountId";
                    using (SqlCommand command = new SqlCommand(query, Con))
                    {
                        command.Parameters.AddWithValue("@AccountId", logged_in_user.UserId);
                        command.Parameters.AddWithValue("@new_balance", depositAmmount);

                        Con.Open();
                        command.ExecuteScalar();

                    }
                }
            }
            catch (Exception ex)
            {
                // Some exception window maybe?
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}