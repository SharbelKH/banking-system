using System.Windows.Controls;
using System.Windows;
using MaterialDesignThemes.Wpf;

namespace BankApplication.View.UserControls
{
    /// <summary>
    /// Interaction logic for PasswordInputBox.xaml
    /// </summary>
    public partial class PasswordInputBox : UserControl
    {
        public bool isPasswordVisible = false; // default
        public PasswordInputBox()
        {
            InitializeComponent();
        }

        public string passwordString
        {
            get { return passwordInput.Password; }
        }

        private string placeholder = string.Empty;

        public string Placeholder
        {
            get { return placeholder; }
            set
            {
               placeholder = value;
               tbPlaceholder.Text = placeholder;
            }
        }

        public void SetPassword(string password)
        {
            passwordInput.Password = password;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (isPasswordVisible)
            {
                passwordInput.Clear();
                visiblePasswordInput.Clear();
                visiblePasswordInput.Focus();
            }
            else
            {
                visiblePasswordInput.Clear();
                passwordInput.Clear();
                passwordInput.Focus();
            }            
        }

        private void passwordInput_GotFocus(object sender, RoutedEventArgs e)
        {
            tbPlaceholder.Visibility = Visibility.Hidden;
        }

        public void show_Password(object sender, RoutedEventArgs e)
        {
            if (!isPasswordVisible)
            {
                visiblePasswordInput.Text = passwordInput.Password;
                visiblePasswordInput.Visibility = Visibility.Visible;
                passwordInput.Visibility = Visibility.Hidden;
                tbPlaceholder.Visibility = Visibility.Hidden;
                visiblePasswordInput.Focus(); 
            }
            else
            {
                passwordInput.Password = visiblePasswordInput.Text;
                passwordInput.Visibility = Visibility.Visible;
                visiblePasswordInput.Visibility = Visibility.Hidden;
                passwordInput.Focus(); 
            }

            isPasswordVisible = !isPasswordVisible; 
        }

        private void passwordInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((string.IsNullOrEmpty(visiblePasswordInput.Text)) & (string.IsNullOrEmpty(passwordInput.Password)))
                tbPlaceholder.Visibility = Visibility.Visible;
        }
    }
}
