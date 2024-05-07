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

        public MainWindow(Database db, User user)
        {
            InitializeComponent();
            this.db = db;
            userController = new UserController(db);
            ApplicationUser.LoggedInUser = user;
        }



        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            string transferAmount = TransferAmount.Text;
            string transferID = TransferID.Text;

            try
            {
                bool transferBool = userController.TransferFunds(transferID, transferAmount);
                if (transferBool)
                {
                    MessageBox.Show("Successfully transfered " + transferAmount + "kr. to: " + transferID);
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

            string depositAmmount = DepositAmount.Text;

            try
            {
                bool depositBool = userController.DepositFunds(depositAmmount, ApplicationUser.LoggedInUser.PhoneNumber);
                if (depositBool)
                {
                    MessageBox.Show("Successfully deposited " + depositAmmount + "kr.");
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
            string withdrawAmmount = AmountToWithdraw.Text;
            try
            {
                bool withdrawBool = userController.WithdrawFunds(withdrawAmmount);
                if (withdrawBool)
                {
                    MessageBox.Show("Successfully withdrawn " + withdrawAmmount + "kr.");
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
            FirstAndSurname.Text = ApplicationUser.LoggedInUser.Id.ToString();
        }
    }
}