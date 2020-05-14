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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContactsApp
{
   
    public partial class MainWindow : Window
    {
        List<Contact> contacts;
        public MainWindow()
        {
            InitializeComponent();
            contacts = new List<Contact>();
            ReadDatabase();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();
            ReadDatabase();
        }

        private void ReadDatabase()
        {
            using(var conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Contact>();
                contacts = conn.Table<Contact>().ToList().OrderBy(c => c.Name).ToList();
                //var sorted = from c in contacts
                //               orderby c.Name
                //               select c;
            }
            contactListView.ItemsSource = contacts;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchBox = sender as TextBox;
            var filteredList = (from c in contacts
                               where c.Name.ToLower().Contains(searchBox.Text.ToLower())
                               orderby c.Name
                               select c).ToList();
            contactListView.ItemsSource = filteredList;
        }

        private void contactListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact selected = (Contact)contactListView.SelectedItem;
            if(selected!=null)
            {
                ContactWindow contactDetailWindow = new ContactWindow(selected);
                contactDetailWindow.ShowDialog();
                ReadDatabase();
            }
        }
    }
}
