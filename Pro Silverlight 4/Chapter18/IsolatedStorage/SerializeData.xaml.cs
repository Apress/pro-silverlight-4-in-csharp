using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.IO;

namespace IsolatedStorage
{
    public partial class SerializeData : UserControl
    {
        public SerializeData()
        {
            InitializeComponent();
        }

        // requires System.Xml.Serialization.dll refrence
        private XmlSerializer serializer = new XmlSerializer(typeof(Person));

        private Person currentPerson;
        private void lstPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstPeople.SelectedItem == null) return;

            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (FileStream stream = store.OpenFile(lstPeople.SelectedItem.ToString(), FileMode.Open))
                {
                    currentPerson = (Person)serializer.Deserialize(stream);
                    txtFirstName.Text = currentPerson.FirstName;
                    txtLastName.Text = currentPerson.LastName;
                    dpDateOfBirth.SelectedDate = currentPerson.DateOfBirth;
                }
            }
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {                           
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (currentPerson != null)
                {
                    store.DeleteFile(currentPerson.FirstName + currentPerson.LastName + ".person");
                }                

                Person person = new Person(txtFirstName.Text, txtLastName.Text, dpDateOfBirth.SelectedDate);
                using (FileStream stream = store.CreateFile(person.FirstName + person.LastName + ".person"))
                {
                    serializer.Serialize(stream, person);
                }
                lstPeople.ItemsSource = store.GetFileNames("*.person");

                currentPerson = null;
                txtFirstName.Text = "";
                txtLastName.Text = "";
            }            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string[] people = store.GetFileNames("*.person");
                lstPeople.ItemsSource = people;
            }
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (lstPeople.SelectedItem == null) return;

            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                store.DeleteFile(lstPeople.SelectedItem.ToString());
                lstPeople.ItemsSource = store.GetFileNames("*.person");

                currentPerson = null;
                txtFirstName.Text = "";
                txtLastName.Text = "";
            }
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public Person(string firstName, string lastName, DateTime? dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        // Required for serialization support.
        public Person() { }
    }
}
