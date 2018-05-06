using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;
using EmailContactsExtension;

namespace WPFLabs1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts = new List<Contact>();
        public MainWindow()
        {
            InitializeComponent();
            var contactManager = new ContactManager();
            var user = contactManager.GetUser("mini", "pw");
            if (user == null)
                return;
           contacts = user.GetContacts();
            var contactListTemp = new ObservableCollection<Contact>(contacts);
            this.DataContext = contactListTemp;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            var contactManager = new ContactManager();
            var user = contactManager.GetUser(LoginText.Text, PasswordText.Password);
            if (user == null)
            {
               
                MessageBox.Show("Failed to login", "Error", MessageBoxButton.OK);
                return;
            }
                
            contacts = user.GetContacts();
            var contactListTemp = new ObservableCollection<Contact>(contacts);
            this.DataContext = contactListTemp;

        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ImportMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExportMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var xml = new XElement("Contacts", contacts.Select(
                x => new XElement("contact",
                new XAttribute("Name", x.Name),
                new XAttribute("Surname", x.Surname),
                new XAttribute("Email", x.Email),
                new XAttribute("Phone", x.Phone),
                new XAttribute("Gender", x.Gender)
                )));
            xml.Save("contacts.xml");
        }
    }   

}
