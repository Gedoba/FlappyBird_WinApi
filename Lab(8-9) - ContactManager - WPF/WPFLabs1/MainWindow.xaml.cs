using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;
using EmailContactsExtension;
using Microsoft.Win32;

namespace WPFLabs1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //List<Contact> contacts = new List<Contact>();
        User currentUser;
        ObservableCollection<Contact> contacts; //when inotified update it
        public MainWindow()
        {
            InitializeComponent();
            contacts = new ObservableCollection<Contact>();
            genderColumn.ItemsSource = new List<Gender>(new Gender[] { Gender.Male, Gender.Female});
            this.DataContext = contacts;
        }

        public void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            var contactManager = new ContactManager();
            var user = contactManager.GetUser(LoginText.Text, PasswordText.Password);
            if (user == null)
            {
               
                MessageBox.Show("Failed to login", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            currentUser = user;
            LowerGridPrelogin.Visibility = Visibility.Collapsed;
            UpperGridPrelogin.Visibility = Visibility.Collapsed;
            LowerGridPostlogin.Visibility = Visibility.Visible;
            UpperGridPostlogin.Visibility = Visibility.Visible;
           
            var contactsTemp = user.GetContacts();
            contacts.Clear();
            contacts = new ObservableCollection<Contact>(contactsTemp);
            this.DataContext = contacts;

        }

        public void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void ImportMenuItem_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            var dlg = new OpenFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML documents (.xml)|*.xml";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                doc.Load(filename);
            }

            contacts.Clear();

            //LINQ didnt work here, dunno why
            XmlNodeList elemList = doc.GetElementsByTagName("contact");
            for (int i = 0; i < elemList.Count; i++)
            {
                var tempContact = new Contact();
                tempContact.Name = elemList[i].Attributes["Name"].Value;
                tempContact.Surname = elemList[i].Attributes["Surname"].Value;
                tempContact.Email = elemList[i].Attributes["Email"].Value;
                tempContact.Phone = elemList[i].Attributes["Phone"].Value;
                Gender g;
                if (elemList[i].Attributes["Gender"].Value == "Female")
                    g = Gender.Female;
                else
                    g = Gender.Male;
                tempContact.Gender = g;
                contacts.Add(tempContact);
            }
        }

        public void ExportMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML documents (.xml)|*.xml";
            Nullable<bool> result = dlg.ShowDialog();


            var xml = new XElement("Contacts", contacts.Select(
                x => new XElement("contact",
                new XAttribute("Name", x.Name),
                new XAttribute("Surname", x.Surname),
                new XAttribute("Email", x.Email),
                new XAttribute("Phone", x.Phone),
                new XAttribute("Gender", x.Gender)
                )));

            if (result == true)
            {
                string filename = dlg.FileName;
                xml.Save(filename);
            }

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            this.Opacity = 0.7;
            var registerWindow = new Register();
            registerWindow.Owner = this;
            registerWindow.ShowDialog();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LowerGridPrelogin.Visibility = Visibility.Visible;
            UpperGridPrelogin.Visibility = Visibility.Visible;
            LowerGridPostlogin.Visibility = Visibility.Collapsed;
            UpperGridPostlogin.Visibility = Visibility.Collapsed;
            ContactDetailsGrid.Visibility = Visibility.Collapsed;
            LoginText.Clear();
            PasswordText.Clear();
            currentUser = null;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            currentUser.SaveContacts(contacts.ToList());
        }

        private void contactListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContactDetailsGrid.Visibility = Visibility.Visible;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            contacts.RemoveAt(contactListView.SelectedIndex);
        }
    }   

}
