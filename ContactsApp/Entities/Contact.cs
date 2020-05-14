using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Entities
{
    [Table("Contacts")]
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        [Unique]
        public string Email { get; set; }
        [Column("PhoneNumber"), Unique]
        public string Phone { get; set; }

        [Ignore]
        public string NotUsedProperty { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Email} - {Phone}";
        }
    }
}
