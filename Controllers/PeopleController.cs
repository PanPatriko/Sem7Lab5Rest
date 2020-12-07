using Lab5Rest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lab5Rest.Controllers
{
    public class PeopleController : ApiController
    {
        private static List<Person> people { get; set; } = new List<Person>()
                    {
                        new Person(1,"Patryk","Panasiuk","Biała Podlaska",1997),
                        new Person(2,"Johnny","Mexico","Nowy Meksyk",1985),
                        new Person(3,"Mario","Bros","Rzym",1990),
                    };


        // GET api/<controller>
        public IEnumerable<Person> Get()
        {
            return people;
        }

        [Route("api/people/find/{name?}/{surname?}/{city?}/{year?}/{lowercase?}/{contains?}")]
        public IEnumerable<Person> Get(string name = null, string surname = null, string city = null, int? year = null, 
                                       bool lowercase = false, bool contains = false)
        {

            var item = people.AsEnumerable();

            if (year != null)
            {
                item = item.Where(x => x.Year == year);
            }

            if (!string.IsNullOrEmpty(name))
            {
                if (contains)
                {
                    item = item.Where(x => (lowercase ? x.Name.ToLower().Contains(name.ToLower()) : x.Name.Contains(name)));
                }
                else
                {
                    item = item.Where(x => (lowercase ? x.Name.ToLower() : x.Name)
                    == (lowercase ? name.ToLower() : name));
                }
                //if (lowercase)
               // {
                //    item = item.Where(x => x.Name.Contains());
               // }
                //else
                //{
                //    item = item.Where(x => x.Name == name);
                //}
            }

            if (!string.IsNullOrEmpty(surname))
            {
                if (contains)
                {
                    item = item.Where(x => (lowercase ? x.Surname.ToLower().Contains(surname.ToLower()) : x.Surname.Contains(surname)));
                }
                else
                {
                    item = item.Where(x => (lowercase ? x.Surname.ToLower() : x.Surname)
                    == (lowercase ? surname.ToLower() : surname));
                }
            }

            if (!string.IsNullOrEmpty(city))
            {
                if (contains)
                {
                    item = item.Where(x => (lowercase ? x.City.ToLower().Contains(city.ToLower()) : x.City.Contains(city)));
                }
                else
                {
                    item = item.Where(x => (lowercase ? x.City.ToLower() : x.City)
                    == (lowercase ? city.ToLower() : city));
                }
            }

            return item;
        }
        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            var item = people.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Person person, bool city = false)
        {
            if(person !=null)
            {
                if(city)
                {
                    var item = people.FirstOrDefault(x => x.City == person.City);
                    if(item == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,"With city parameter, there must be a person from the same city in the base");
                    }
                }
                var maxId = 0;
                if (people.Count > 0)
                {
                    maxId = people.Max(x => x.Id);
                }
                person.Id = maxId + 1;
                people.Add(person);
                return Request.CreateResponse(HttpStatusCode.Created, person);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody] Person person)
        {
            if(person != null)
            {
                var item = people.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    item.Name = person.Name;
                    item.Surname = person.Surname;
                    item.City = person.City;
                    item.Year = person.Year;

                    return Request.CreateResponse(HttpStatusCode.OK, item);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            var item = people.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                people.Remove(item);
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
