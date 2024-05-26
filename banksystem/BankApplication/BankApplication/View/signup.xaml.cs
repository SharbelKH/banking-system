using System.Diagnostics.Eventing.Reader;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using BankApplication;
using BankApplication.Controller;
using BankApplication.model;

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
        // Validate the users input before creating a user
        private void btn_CreateUser_Click(object sender, RoutedEventArgs e)
        {
            if (fullname.TextString == "" || phonenumber.TextString == "" || adress.TextString == "" || passwordinput.passwordString == "" 
                || Age_DOB.SelectedDate == null || Age_DOB.SelectedDate.Value == DateTime.MinValue)
            {
                MessageBox.Show("Missing information, please check!");
            }
            else if (phonenumber.TextString.Length < 5)
            {
                {
                    MessageBox.Show("Invalid phonenumber, must be larger than 5 Characters");
                }
            } 
            
            else if (User.IsUserOldEnough(Age_DOB.SelectedDate.Value))
            {
                string fullName = fullname.TextString;
                string phoneNumber = phonenumber.TextString;
                string address = adress.TextString;
                string password = passwordinput.passwordString;
                DateTime dateOfBirth = Age_DOB.SelectedDate.Value;

                // Method to create the user and put the new user into the database
                bool userCreated = userController.CreateUser(fullName, phoneNumber, address, password, dateOfBirth);

                // If sucessfull then validate the user via UI, otherwise tell user something went wrong 
                if (userCreated)
                {
                    MessageBox.Show("User created successfully!");
                    // Sucessfully created user, send them back to login screen
                    Login loginWindow = new Login();
                    loginWindow.Show();
                    // Close the signup window
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create user. Phonenumber already in use! Please try again.");
                }
            }
            else
            {
                MessageBox.Show("Users have to be at least 18 years old to sign up!");
            }
        }
    }
}
