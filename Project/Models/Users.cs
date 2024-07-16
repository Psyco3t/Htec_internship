using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Project.Models
{
 public class User
 {
  [Key]
  public int Id { get; set; }
  [Required]
  [MaxLength(100)]
  public string UserName { get; set; }
  [Required]
  public string Password { get; set; }
  [Required]
  [MaxLength(100)]
  public string Email { get; set; }
  [ForeignKey("RoleId")]
  public int RoleId { get; set; }
  [JsonIgnore]
  public Role? Role { get; set; }
 }

 public class UserDTO
 {
  public int Id { get; set; }
  public string UserName { get; set; }
  public string Email { get; set; }
 }
}
