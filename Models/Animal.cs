using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab5Rest.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Age { get; set; }

        public Animal(int id,string name, string type, int age)
        {
            Id = id;
            Name = name;
            Type = type;
            Age = age;
        }
    }
}