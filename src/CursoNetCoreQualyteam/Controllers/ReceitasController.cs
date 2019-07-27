using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CursoNetCoreQualyteam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitasController : ControllerBase
    {
        // GET api/receitas
        [HttpGet]
        public ActionResult<IEnumerable<ReceitaViewModel>> Get()
        {
            return new ReceitaViewModel[] {
                new ReceitaViewModel(){
                    Id = 1,
                    Title = "Feijão com Arroz",
                    Description = "Um belo prato de feijão com arroz que alimenta todo bom brasileiro",
                    Ingredients = "Feijão, Arroz",
                    Preparation = "Mistureo feijão com o arroz e pronto.",
                    ImageUrl = "fakeurl.com/feijaocomarroz"
                },new ReceitaViewModel(){
                    Id = 2,
                    Title = "Feijão com Arroz",
                    Description = "Um belo prato de feijão com arroz que alimenta todo bom brasileiro",
                    Ingredients = "Feijão, Arroz",
                    Preparation = "Mistureo feijão com o arroz e pronto.",
                    ImageUrl = "fakeurl.com/feijaocomarroz"
                }
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }


    public class ReceitaViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Preparation { get; set; }
        public string ImageUrl { get; set; }
    }
}