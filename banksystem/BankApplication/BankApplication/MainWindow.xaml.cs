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
        // UserController which handles users input
        private UserController userController;
        // ObservableCollection of Users for listbox property for accountdetails
        public ObservableCollection<User> UserCollection { get; set; }
        // ObservableCollection of TransactionRecords which is connected and updated simultaniously as the loggedin user, ( '=>' ) this is similar to a pointer. IfLoggedinUser.Transactions change, then this changes aswelll
        public ObservableCollection<TransactionRecord> Transactions => ApplicationUser.LoggedInUser.Transactions;

        public MainWindow(Database db, User user)
        {
            InitializeComponent();
            userController = new UserController(db);
            // Set the static class ApplicationUser to the authenticated user which logged in
            ApplicationUser.LoggedInUser = user;
            UserCollection = new ObservableCollection<User>
            {
               ApplicationUser.LoggedInUser
            };
           
            this.DataContext = this;
            this.Loaded += MainWindow_Loaded;
        }



        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            string transferAmount = TransferAmount.TextString;
            string transferID = TransferID.TextString;

            try
            {
                // Method call which will transfer funds between users
                bool transferBool = userController.TransferFunds(transferID, transferAmount);
                // If its done succesfully, validate the user via a Textbox message
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
                // Method call which will deposit funds to users account
                bool depositBool = userController.DepositFunds(depositAmmount, ApplicationUser.LoggedInUser.PhoneNumber);
                // If its done succesfully, validate the user via a Textbox message
                if (depositBool)
                {
                    MessageBox.Show("Successfully deposited " + Int32.Parse(depositAmmount) + "kr.");
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
                // Method call which will withdraw funds from users account
                bool withdrawBool = userController.WithdrawFunds(withdrawAmmount);
                // If its done succesfully, validate the user via a Textbox message
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