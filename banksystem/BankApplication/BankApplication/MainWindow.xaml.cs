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
using BankApplication.View;
using BankApplication.myExceptions;

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
            Loaded += MainWindow_Loaded;
        }



        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            Transaction new_transaction = new Transaction();
            double transferAmount = double.Parse(TransferAmount.Text);
            string transferID = TransferID.Text;
            try
            {
                new_transaction.Transfer(logged_in_user.UserId, transferID, transferAmount);
                MessageBox.Show("Transfer succeeded");

            }
            catch (insufficientFunds insufficientFund)
            {
                MessageBox.Show(insufficientFund.Message);

            }
            catch (invalid_source invalid_source)
            {
                MessageBox.Show(invalid_source.Message);
            }
        }

        private void Deposit_Click(object sender, RoutedEventArgs e)
        {

            string depositAmmount = DepositAmount.Text;
            string ConString = OurSqlConnectionString.ConString;

            // Input is not a double
            if (!double.TryParse(depositAmmount, out double res))
            {
                MessageBox.Show("Enter a number please!");
                return;

            }

            // Deposit is larger than 10 000 and therefore false
            if (!logged_in_user.Deposit(double.Parse(depositAmmount)))
            {
                // Maybe window saying its not possible to deposit this ammount?
                MessageBox.Show("You cant deposit more than 10 000kr at a time!");
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
                        MessageBox.Show("Successfully deposited " + depositAmmount + "kr.");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: 404. Account not found in database");
            }

        }

        private void Withdraw_Click(object sender, RoutedEventArgs e)
        {
            string withdrawAmmount = AmountToWithdraw.Text;
            string ConString = OurSqlConnectionString.ConString;

            // Input is not a double
            if (!double.TryParse(withdrawAmmount, out double res))
            {
                MessageBox.Show("Enter a number please!");
                return;

            }

            // Deposit is larger than 10 000 and therefore false
            if (!logged_in_user.Withdraw(double.Parse(withdrawAmmount)))
            {
                // Maybe window saying its not possible to deposit this ammount?
                MessageBox.Show("You cant withdraw " + withdrawAmmount +"kr, you only have: " + logged_in_user.Balance + "kr!");
                return;
            }

            try
            {
                using (SqlConnection Con = new SqlConnection(ConString))
                {
                    string query = "UPDATE Account SET Balance = Balance - @withdraw WHERE Id = @AccountId";
                    using (SqlCommand command = new SqlCommand(query, Con))
                    {
                        command.Parameters.AddWithValue("@AccountId", logged_in_user.UserId);
                        command.Parameters.AddWithValue("@withdraw", withdrawAmmount);

                        Con.Open();
                        command.ExecuteScalar();
                        MessageBox.Show("Successfully withdrawn " + withdrawAmmount + "kr.");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: 404. Account not found in database");
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Login signUpWindow = new Login();
            signUpWindow.Show();
            this.Close();

        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Change the text of the TextBox
            FirstAndSurname.Text = this.logged_in_user.UserId;
        }
    }
}