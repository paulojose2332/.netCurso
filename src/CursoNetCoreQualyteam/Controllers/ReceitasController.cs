using System.Collections.Generic;
using System.Linq;
using CursoNetCoreQualyteam.Dominio;
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
        }

        // GET api/receitas/5
        [HttpGet("{id}")]
        public ActionResult<ReceitaViewModel> Get(int id)
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
            ).Where(receita => receita.Id == id).FirstOrDefault();
        }

        // POST api/receitas
        [HttpPost]
        public ActionResult<ReceitaViewModel> Post([FromBody] ReceitaViewModel receitaPayload)
        {
            var receita = new Receita(receitaPayload.Id, receitaPayload.Title, receitaPayload.Description, receitaPayload.Ingredients, receitaPayload.Preparation, receitaPayload.ImageUrl);
            _context.Receitas.Add(receita);
            _context.SaveChanges();

            var newViewModel = new ReceitaViewModel(){
                Id = receita.Id,
                Title = receita.Titulo,
                Description = receita.Descricao,
                Ingredients = receita.Ingredientes,
                Preparation = receita.Preparacao,
                ImageUrl = receita.UrlDaImagem
            };
            return Ok(newViewModel);
        }

        // DELETE api/receitas/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var deleteReceita =  _context.Receitas.Where(receita => receita.Id == id).FirstOrDefault();

            if(deleteReceita == null)
                return NotFound();

            _context.Receitas.Remove(deleteReceita);
            _context.SaveChanges();

            return NoContent();
        }
        // PUT api/receitas/5
        [HttpPut("{id}")]
        public ActionResult<ReceitaViewModel> Put(int id, [FromBody] ReceitaViewModel receitaPayload) {
            var receita = _context.Receitas.Where(r => r.Id == id).FirstOrDefault();

            if(receita == null)
                return NotFound();

            receita.Titulo = receitaPayload.Title;
            receita.Descricao = receitaPayload.Description;
            receita.Ingredientes = receitaPayload.Ingredients;
            receita.Preparacao = receitaPayload.Preparation;
            receita.UrlDaImagem = receitaPayload.ImageUrl;

            _context.Receitas.Update(receita);
            _context.SaveChanges();

            var updatedViewModel = new ReceitaViewModel(){
                Id = receita.Id,
                Title = receita.Titulo,
                Description = receita.Descricao,
                Ingredients = receita.Ingredientes,
                Preparation = receita.Preparacao,
                ImageUrl = receita.UrlDaImagem
            };
            return Ok(updatedViewModel);
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