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
using BankApplication.model;
using BankApplication.View;
using BankApplication.myExceptions;
using BankApplication.Controller;
using System.Collections.ObjectModel;

namespace BankApplication
{



    //  Transaction
    // Deposit
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database db;
        private UserController userController;
        public ObservableCollection<User> UserCollection { get; set; }
        public ObservableCollection<TransactionRecord> transactions;

        public MainWindow(Database db, User user)
        {
            InitializeComponent();
            this.db = db;
            userController = new UserController(db);
            ApplicationUser.LoggedInUser = user;
            UserCollection = new ObservableCollection<User>
            {
               ApplicationUser.LoggedInUser
            };
            transactions = ApplicationUser.LoggedInUser.transactions;
            this.DataContext = this;
            this.Loaded += MainWindow_Loaded;
        }



        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            string transferAmount = TransferAmount.TextString;
            string transferID = TransferID.TextString;

            try
            {
                bool transferBool = userController.TransferFunds(transferID, transferAmount);
                if (transferBool)
                {
                    MessageBox.Show("Successfully transfered " + Int32.Parse(transferAmount) + "kr. to: " + transferID);
                }
                else
                {
                    MessageBox.Show("Failed to deposit funds. Please try again.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Deposit_Click(object sender, RoutedEventArgs e)
        {

            string depositAmmount = DepositAmount.TextString;

            try
            {
                bool depositBool = userController.DepositFunds(depositAmmount, ApplicationUser.LoggedInUser.PhoneNumber);
                if (depositBool)
                {
                    MessageBox.Show("Successfully deposited " + Int32.Parse(depositAmmount) + "kr.");

                    foreach (TransactionRecord TR in ApplicationUser.LoggedInUser.transactions)
                    {
                        MessageBox.Show(TR.Insert_Transaction_Into_TransactionDb_String());
                    }
                }
                else
                {
                    MessageBox.Show("Failed to deposit funds. Please try again.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Withdraw_Click(object sender, RoutedEventArgs e)
        {
            string withdrawAmmount = AmountToWithdraw.TextString;
            try
            {
                bool withdrawBool = userController.WithdrawFunds(withdrawAmmount);
                if (withdrawBool)
                {
                    MessageBox.Show("Successfully withdrawn " + Int32.Parse(withdrawAmmount) + "kr.");
                }
                else
                {
                    MessageBox.Show("Failed to withdraw funds. Please try again.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            FirstAndSurname.Content = ApplicationUser.LoggedInUser.Id.ToString();
        }


    }
}