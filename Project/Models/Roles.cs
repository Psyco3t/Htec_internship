using System.Text.Json.Serialization;
namespace Project.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        [JsonIgnore]
        public string ?RoleName { get; set; }
        [JsonIgnore]
        public ICollection<User>? Users { get; } = new List<User>();
    }
}
