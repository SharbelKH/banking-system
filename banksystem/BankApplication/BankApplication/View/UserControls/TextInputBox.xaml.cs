using System.Windows;
using System.Windows.Controls;

namespace BankApplication.View.UserControls
{
    /// <summary>
    /// Interaction logic for TextInputBox.xaml
    /// </summary>
    public partial class TextInputBox : UserControl
    {
        public TextInputBox()
        {
            InitializeComponent();
        }

        public string TextString
        {
            get { return txtInput.Text; }
        }

        private string placeholder;

        public string Placeholder
        {
            get { return placeholder; }
            set 
            { 
                placeholder = value; 
                txtInput.Text = placeholder;
                //maybe there is a smarter way, ex onPropertyChanged()?
            }
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtInput.Text))
                tbPlaceholder.Visibility = Visibility.Visible;
            else    
                tbPlaceholder.Visibility = Visibility.Hidden;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Clear();
            txtInput.Focus();
        }
    }
}
