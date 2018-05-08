using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EmailContactsExtension;

namespace WPFLabs1
{
    /// <summary>
    /// Interaction logic for register.xaml
    /// </summary>
    public partial class Register : Window
    {
                string login;
        string email;
        string password;
        public Register()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public string Login
        {
            get { return login; }
            set
            {
                if (value.Length != 0)
                    login = value;
                else
                    throw new ArgumentException("Login can't be empty");
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                Regex r = new Regex("\\S+[@]\\S+[.]\\S\\S");
                if (r.Match(value).Success)
                    email = value;
                else
                    throw new ArgumentException("E-mail is wrong");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
                MessageBox.Show("You have to fill all fields.", "An error occured!", MessageBoxButton.OK);
            else if (PasswordTextBox.Password != ConfirmPasswordTextBox.Password)
                MessageBox.Show("Passwords don't match!", "An error occured!", MessageBoxButton.OK);
            else
            {
                password = PasswordTextBox.Password;
                var manager = new ContactManager();
                manager.RegisterUser(login, password);
                User user = manager.GetUser(login, password);
                user.Email = email;
                Owner.Opacity = 1;
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Owner.Opacity = 1;
            this.Close();
        }
    }
}
