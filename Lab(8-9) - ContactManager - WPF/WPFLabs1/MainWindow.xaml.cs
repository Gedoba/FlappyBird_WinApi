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
        List<Contact> contacts = new List<Contact>();
        ObservableCollection<Contact> contactListTemp = new ObservableCollection<Contact>(); //when inotified update it
        public MainWindow()
        {
            InitializeComponent();
            //var contactManager = new ContactManager();
            //var user = contactManager.GetUser("mini", "pw");
            //if (user == null)
            //    return;
            //contacts = user.GetContacts();
            //contactListTemp = new ObservableCollection<Contact>(contacts);
            this.DataContext = contactListTemp;
        }

        public void LoginButton_Click(object sender, RoutedEventArgs e)
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

            contactListTemp.Clear();

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
                contactListTemp.Add(tempContact);
            }
        }

        public void ExportMenuItem_Click(object sender, RoutedEventArgs e)
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
