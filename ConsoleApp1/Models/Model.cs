using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp1.Models
{
    public partial class Model
    {
        public int Id { get; set; }

        public byte[] Encrypted { get; set; }

        public byte[] Encrypted2 { get; set; }

        [NotMapped]
        public string Decrypted { get; set; }

        [NotMapped]
        public string Decrypted2 { get; set; }

        public Table2 Table2 { get; set; }
    }

    public class EncryptedAttribute : Attribute
    {
        private string _key { get; set; }
        public EncryptedAttribute(string key)
        {
            _key = key;
        }
    }
}
