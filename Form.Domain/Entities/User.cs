using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Form.Domain.Entities
{
    [Index(nameof(FirstName), nameof(FirstLastname))]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastname { get; set; }
        public string SecondLastname { get; set; }
        public DateTime BirthDate { get; set; }
        public double Salary { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
