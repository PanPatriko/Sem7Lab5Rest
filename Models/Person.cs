using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab5Rest.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public int Year { get; set; }

        public Person(int id,string name, string surname, string city, int year)
        {
            Id = id;
            Name = name;
            Surname = surname;
            City = city;
            Year = year;
        }
    }
}