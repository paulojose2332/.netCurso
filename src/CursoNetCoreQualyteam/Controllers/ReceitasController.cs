using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursoNetCoreQualyteam.Infraestrutura;
using Microsoft.AspNetCore.Mvc;

namespace CursoNetCoreQualyteam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitasController : ControllerBase
    {
        private readonly ReceitasContext _context;

        public ReceitasController(ReceitasContext context) {
            _context = context;
        }

        // GET api/receitas
        [HttpGet]
        public ActionResult<IEnumerable<ReceitaViewModel>> Get()
        {
            return _context.Receitas.Select(receita => 
                new ReceitaViewModel(){
                    Id = receita.Id,
                    Title = receita.Titulo,
                    Description = receita.Descricao,
                    Ingredients = receita.Ingredientes,
                    Preparation = receita.Preparacao,
                    ImageUrl = receita.UrlDaImagem
                }
            ).ToArray();

            // return new ReceitaViewModel[] {
            //     new ReceitaViewModel(){
            //         Id = 1,
            //         Title = "Feijão com Arroz",
            //         Description = "Um belo prato de feijão com arroz que alimenta todo bom brasileiro",
            //         Ingredients = "Feijão, Arroz",
            //         Preparation = "Mistureo feijão com o arroz e pronto.",
            //         ImageUrl = "https://bugg.com.br/wordpress/wp-content/uploads/2016/11/Arroz-com-feij%C3%A3o.jpg"
            //     },new ReceitaViewModel(){
            //         Id = 2,
            //         Title = "Feijão com Arroz",
            //         Description = "Um belo prato de feijão com arroz que alimenta todo bom brasileiro",
            //         Ingredients = "Feijão, Arroz",
            //         Preparation = "Mistureo feijão com o arroz e pronto.",
            //         ImageUrl = "https://www.falamart.com.br/wp-content/uploads/2019/07/prato-arroz-e-feij%C3%A3o-1-1068x707.jpg"
            //     }
            // };
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