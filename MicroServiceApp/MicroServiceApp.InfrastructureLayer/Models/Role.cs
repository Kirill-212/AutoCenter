using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicroServiceApp.InfrastructureLayer.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        [JsonIgnore]
        public List<User> Users { get; set; }
    }
}
