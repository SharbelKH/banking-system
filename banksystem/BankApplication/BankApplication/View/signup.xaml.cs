using System.Diagnostics.Eventing.Reader;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
namespace BankApplication.View
{
    /// <summary>
    /// Interaction logic for signup.xaml
    /// </summary>
    public partial class signup : Window
    {
        public signup()
        {
            InitializeComponent();
        }

        // Get connectionstring from static global variable
        string ConString = OurSqlConnectionString.ConString;

        private void btn_CreateUser_Click(object sender, RoutedEventArgs e)
        {
            if (fullname.TextString == "" || phonenumber.TextString == "" || adress.TextString == "" || passwordinput.passwordString == "")
            {
                MessageBox.Show("Missing information, please check!");
            }
            else
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(ConString))
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("insert into Account(Name, Phonenumber, Address, Password, Balance) values (@Name, @PhoneNumber, @Address, @Password, @Balance)", Con);
                        cmd.Parameters.AddWithValue("@Name", fullname.TextString);
                        cmd.Parameters.AddWithValue("@PhoneNumber", phonenumber.TextString);
                        cmd.Parameters.AddWithValue("@Address", adress.TextString);
                        cmd.Parameters.AddWithValue("@Password", passwordinput.passwordString);
                        cmd.Parameters.AddWithValue("@Balance", 0);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Account Created!");
                        Con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
