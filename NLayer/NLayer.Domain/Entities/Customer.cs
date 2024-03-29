﻿using NLayer.Domain.Base;

namespace NLayer.Domain.Entities
{
    public class Customer : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
