using System.Data;
using System.Windows;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.NativeInterop;

namespace BankApplication.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        // Get connectionstring from static global variable
        string ConString = OurSqlConnectionString.ConString;
        private void btn_Signup_Click(object sender, RoutedEventArgs e)
        {
            signup signUpWindow = new signup();
            signUpWindow.Show();
            this.Close();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            string userID = UsernameIDInput.TextString;
            string password = PasswordLoginInput.passwordString;

            if (AuthenticateUser(userID, password))
            {
                MessageBox.Show("login successful!");
                //open main window connected with ID
            }
            else
            {
                MessageBox.Show("Invalid user ID or password!");
            }
        }
        
        private bool AuthenticateUser(string userID, string password)
        {
            using (SqlConnection Con = new SqlConnection(ConString))
            {
                string query = "SELECT COUNT(*) FROM Account WHERE ID = @Id AND Password = @Password";
                SqlCommand command = new SqlCommand(query, Con);
                command.Parameters.AddWithValue("@Id", userID);
                command.Parameters.AddWithValue("@Password", password);
                Con.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
