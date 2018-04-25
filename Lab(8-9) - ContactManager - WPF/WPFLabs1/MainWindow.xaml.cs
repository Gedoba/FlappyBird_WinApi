using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EmailContactsExtension;

namespace WPFLabs1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            var contactManager = new ContactManager();
            var user = contactManager.GetUser(LoginText.Text, PasswordText.Text);
            if (user == null)
                return;
            var contacts = user.GetContacts();
            foreach(var v in contacts)
            {
                ListBoxItem itm = new ListBoxItem();
                itm.Content = v.Name;
                contactList.Items.Add(itm);

            }

        }
    }
}
