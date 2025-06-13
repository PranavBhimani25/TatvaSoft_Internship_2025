using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Entities.Entities
{
    [Table("UserSkills")] // Specify the table name
    public class UserSkills : BaseEntity // Assuming BaseEntity defines common properties
    {
        [Key]
        public int Id { get; set; }

        public string Skill { get; set; }

        public int UserId { get; set; }
    }
}
