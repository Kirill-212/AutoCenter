using System;

namespace MicroServiceApp.InfrastructureLayer.Models
{
    public class Order
    {
        public int Id { get; set; }

        public decimal TotalCost { get; set; }

        public DateTime DateOfBuyCar { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }
    }
}
