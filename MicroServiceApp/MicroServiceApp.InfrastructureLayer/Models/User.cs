using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicroServiceApp.InfrastructureLayer.Models
{
    public enum Status
    {
        CREATED,
        ACTIVE
    }

    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Surname { get; set; }

        public DateTime DBay { get; set; }

        public Status Status { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int RoleId { get; set; }

        public Employee Employee { get; set; }

        public Role Role { get; set; }

        public List<ClientCar> ClientsCars { get; set; }
    }
}
