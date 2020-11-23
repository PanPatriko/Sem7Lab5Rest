using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lab5Rest.Models;
namespace Lab5Rest.Controllers
{
    public class AnimalsController : ApiController
    {
        private static List<Animal> animals { get; set; } = new List<Animal>()                   
                    {
                        new Animal(1,"Riko","Dog",5),
                        new Animal(2,"Rambo","Dog",3),
                        new Animal(3,"Felix","Cat",7),
                    };


        // GET api/<controller>
        public IEnumerable<Animal> Get()
        {
            return animals;
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            var item = animals.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Animal animal)
        {
            if(animal != null)
            {
                var maxId = 0;
                if (animals.Count > 0)
                {
                    maxId = animals.Max(x => x.Id);
                }
                animal.Id = maxId + 1;
                animals.Add(animal);
                return Request.CreateResponse(HttpStatusCode.Created, animal);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody] Animal animal)
        {
            if (animal != null)
            {
                var item = animals.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    item.Name = animal.Name;
                    item.Type = animal.Type;
                    item.Age = animal.Age;

                    return Request.CreateResponse(HttpStatusCode.OK, item);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            var item = animals.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                animals.Remove(item);
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
