﻿using System.Windows.Controls;
using System.Windows;

namespace BankApplication.View.UserControls
{
    /// <summary>
    /// Interaction logic for PasswordInputBox.xaml
    /// </summary>
    public partial class PasswordInputBox : UserControl
    {
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
            passwordInput.Clear();
            passwordInput.Focus();
        }

        private void passwordInput_GotFocus(object sender, RoutedEventArgs e)
        {
            tbPlaceholder.Visibility = Visibility.Collapsed;
        }
    }
}
