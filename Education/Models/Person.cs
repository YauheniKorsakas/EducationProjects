﻿namespace Education.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        private CommonData data;

        public Person(CommonData data) { }
    }
}