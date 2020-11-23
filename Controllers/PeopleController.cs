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
                        new Person(3,"Mario","Macaroni","Rzym",1990),
                    };


        // GET api/<controller>
        public IEnumerable<Person> Get()
        {
            return people;
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
        public HttpResponseMessage Post([FromBody] Person person)
        {
            if(person !=null)
            {
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
