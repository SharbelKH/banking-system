using System.Diagnostics.Eventing.Reader;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using BankApplication;
using BankApplication.Controller;

namespace BankApplication.View
{
    /// <summary>
    /// Interaction logic for signup.xaml
    /// </summary>
    public partial class signup : Window
    {
        private UserController userController;

        public signup(UserController userController)
        {
            InitializeComponent();
            this.userController = userController;
        }

        private void btn_CreateUser_Click(object sender, RoutedEventArgs e)
        {
            if (fullname.TextString == "" || phonenumber.TextString == "" || adress.TextString == "" || passwordinput.passwordString == "")
            {
                MessageBox.Show("Missing information, please check!");
            }

            else
            {
                string fullName = fullname.TextString;
                string phoneNumber = phonenumber.TextString;
                string address = adress.TextString;
                string password = passwordinput.passwordString;

                // Assuming you have access to the userController instance
                // and it's properly initialized, you can use it here
                bool userCreated = userController.CreateUser(fullName, phoneNumber, address, password);

                if (userCreated)
                {
                    MessageBox.Show("User created successfully!");
                    
                    // Optionally, you might want to open the login window again
                    Login loginWindow = new Login();
                    loginWindow.Show();
                    // Close the signup window
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create user. Please try again.");
                }
            }
        }

    }
}
