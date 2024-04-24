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
        private bool isPasswordVisible = false;
        public PasswordInputBox()
        {
            InitializeComponent();
        }

        public string passwordString
        {
            get { return passwordInput.Password; }
        }

        private string placeholder;

        public string Placeholder
        {
            get { return placeholder; }
            set
            {
               placeholder = value;
               tbPlaceholder.Text = placeholder;
            }
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
            tbPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void show_Password(object sender, RoutedEventArgs e)
        {
            if (!isPasswordVisible)
            {
                visiblePasswordInput.Text = passwordInput.Password;
                visiblePasswordInput.Visibility = Visibility.Visible;
                passwordInput.Visibility = Visibility.Hidden;
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
