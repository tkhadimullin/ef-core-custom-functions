using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.Models
{
    public class Table2
    {
        [Key]
        public int Id { get; set; }
        public bool IsSomething { get; set; }
    }
}
