using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaLearning.Models
{
    [Table("User")]
    [Index(nameof(Username), IsUnique = true)]
    public class EfUser
    {
        public int Id { get; set; }

        [StringLength(450)]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string ClassGrade { get; set; }

        public EfUser(string username, string password, string name, string classGrade)
        {
            Username = username;
            Password = password;
            Name = name;
            ClassGrade = classGrade;
        }
    }
}
