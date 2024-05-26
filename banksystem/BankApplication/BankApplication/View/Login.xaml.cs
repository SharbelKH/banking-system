using System.Data;
using System.Windows;
using BankApplication.Controller;
using BankApplication.model;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.NativeInterop;

namespace BankApplication.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private UserController userController;
        private Database db;

        public Login()
        {
            InitializeComponent();
            // Static connectionstring that we set in static class
            string connectionstring = OurSqlConnectionString.ConString;
            // Instanciate a class Database that handles calls to database
            var connectionFactory = new DefaultDbConnectionFactory();
            db = new Database(connectionstring, connectionFactory);
            // Instanciate class which handles userInputs to a controller class
            userController = new UserController(db);
        }

        private void btn_Signup_Click(object sender, RoutedEventArgs e)
        {
            signup signUpWindow = new signup(userController);
            signUpWindow.Show();
            this.Close();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            // UserID is your phoneNumber
            string userID = UsernameIDInput.TextString;
            string password = PasswordLoginInput.passwordString;

            // Validate the users input of username and password
            if (userController.AuthenticateUser(userID, password))
            {
                MessageBox.Show("login successful!");
                // Authentication was successfull and therefore instanciate a user which is logged in
                User user = userController.getUser(userID);
                // Launch mainWindow and send arguments of the database and the user
                MainWindow mainWindow = new MainWindow(db,user);
                mainWindow.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Invalid phone number or password!");
            }
        }
    }
}
