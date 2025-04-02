using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Models
{
    public class Apage
    {
        [Required]
        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Primary Key (Auto-incrementing)

        [Required]
        [MaxLength(100)]
        public string? Username { get; set; }  // Ensure it's mapped correctly

        [Required]
        [MaxLength(100)]
        public string? Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

       
    }
}
