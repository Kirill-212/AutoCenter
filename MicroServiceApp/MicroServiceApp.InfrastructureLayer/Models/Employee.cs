using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicroServiceApp.InfrastructureLayer.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public DateTime StartWorkDate { get; set; }

        public bool IsActive { get; set; }

        public string Address { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        [JsonIgnore]
        public List<New> News { get; set; }

    }
}
