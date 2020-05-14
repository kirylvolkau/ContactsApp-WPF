using ContactsApp.Entities;
using SQLite;
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
using System.Windows.Shapes;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for ContactWindow.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {
        Contact contact;
        public ContactWindow(Contact contact)
        {
            InitializeComponent();
            this.contact = contact;
            nameContactTextBox.Text = contact.Name;
            emailContactTextBox.Text = contact.Email;
            phoneContactTextBox.Text = contact.Phone;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            contact.Name = nameContactTextBox.Text;
            contact.Email = emailContactTextBox.Text;
            contact.Phone = phoneContactTextBox.Text;

            using (var connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>(); //nothing will happen if table exists
                connection.Update(contact);
            };

            Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>(); //nothing will happen if table exists
                connection.Delete(contact);
            };

            Close();
        }
    }
}
