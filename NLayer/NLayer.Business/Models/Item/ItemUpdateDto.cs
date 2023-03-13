﻿namespace NLayer.Business.Models.Item
{
    public class ItemUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TotalCount { get; set; }
        public double? Price { get; set; }
    }
}
