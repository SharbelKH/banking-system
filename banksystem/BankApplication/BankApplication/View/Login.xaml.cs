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
            string connectionstring = OurSqlConnectionString.ConString;
            var connectionFactory = new DefaultDbConnectionFactory();
            db = new Database(connectionstring, connectionFactory);
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

            if (userController.AuthenticateUser(userID, password))
            {
                MessageBox.Show("login successful!");
                User user = userController.getUser(userID);
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
