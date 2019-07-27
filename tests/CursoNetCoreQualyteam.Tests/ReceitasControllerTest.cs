using System;
using System.Linq;
using CursoNetCoreQualyteam.Dominio;
using CursoNetCoreQualyteam.Infraestrutura;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CursoNetCoreQualyteam.Controllers.Tests
{
    public class ReceitasControllerTest
    {

        private ReceitasContext CreateTestContext()
        {
            var options = new DbContextOptionsBuilder<ReceitasContext>().UseInMemoryDatabase("database").Options;
            return new ReceitasContext(options);
        }


        [Fact]
        public void GetAll_DeveResponderComTodasAsReceitasCadastradasAsync()
        {
            //Arrange
            // insere receitas
            var arrozComFeijao = new Receita()
            {
                Id = 1,
                Titulo = "Feijão com Arroz",
                Descricao = "Um belo prato de feijão com arroz que alimenta todo bom brasileiro",
                Ingredientes = "Feijão, Arroz",
                Preparacao = "Misture o feijão com o arroz e pronto.",
                UrlDaImagem = "http://media.agoramt.com.br/2016/10/arroz-e-feij%C3%A3o.jpg"
            };
            var batataFrita = new Receita()
            {
                Id = 2,
                Titulo = "Batatas Fritas",
                Descricao = "Uma porção de batata",
                Ingredientes = "Batata, Óleo, Sal",
                Preparacao = "Misture o feijão com o arroz e pronto.",
                UrlDaImagem = "https://panelinha-sitenovo.s3-sa-east-1.amazonaws.com/receita/953607600000-Batata-frita-tradicional.jpg"
            };

            var context = CreateTestContext();
            context.AddRange(arrozComFeijao, batataFrita);
            context.SaveChanges();
            var controller = new ReceitasController(context);

            //Act
            var receitas = controller.Get();

            //Assert
            receitas.Value.Should().BeEquivalentTo(new ReceitaViewModel[]{
                new ReceitaViewModel(){
                    Id = arrozComFeijao.Id,
                    Title = arrozComFeijao.Titulo,
                    Description = arrozComFeijao.Descricao,
                    Ingredients = arrozComFeijao.Ingredientes,
                    Preparation = arrozComFeijao.Preparacao,
                    ImageUrl = arrozComFeijao.UrlDaImagem
                },
                new ReceitaViewModel(){
                    Id = batataFrita.Id,
                    Title = batataFrita.Titulo,
                    Description = batataFrita.Descricao,
                    Ingredients = batataFrita.Ingredientes,
                    Preparation = batataFrita.Preparacao,
                    ImageUrl = batataFrita.UrlDaImagem
                }
            });
        }

        [Fact]
        public void GetOne_DeveRetornarUmaReceita()
        {
            //Arrange
            // insere receitas
            var arrozComFeijao = new Receita()
            {
                Id = 3,
                Titulo = "Feijão com Arroz",
                Descricao = "Um belo prato de feijão com arroz que alimenta todo bom brasileiro",
                Ingredientes = "Feijão, Arroz",
                Preparacao = "Misture o feijão com o arroz e pronto.",
                UrlDaImagem = "http://media.agoramt.com.br/2016/10/arroz-e-feij%C3%A3o.jpg"
            };

            var context = CreateTestContext();
            context.AddRange(arrozComFeijao);
            context.SaveChanges();
            var controller = new ReceitasController(context);

            //Act
            var receitas = controller.Get(arrozComFeijao.Id);

            //Assert
            receitas.Value.Should().BeEquivalentTo(new ReceitaViewModel()
            {
                Id = arrozComFeijao.Id,
                Title = arrozComFeijao.Titulo,
                Description = arrozComFeijao.Descricao,
                Ingredients = arrozComFeijao.Ingredientes,
                Preparation = arrozComFeijao.Preparacao,
                ImageUrl = arrozComFeijao.UrlDaImagem
            });
        }

        [Fact]
        public void GetOne_DeveRetornarNull()
        {
            //Arrange
            var context = CreateTestContext();
            var controller = new ReceitasController(context);

            //Act
            var receitas = controller.Get(-1);

            //Assert
            receitas.Value.Should().BeNull();
        }

        [Fact]
        public void Post_DeveInserirUmaReceita()
        {
            //Arrange
            var brigadeiro = new Receita()
            {
                Id = 4,
                Titulo = "Brigadeiro",
                Descricao = "Um belo briagdeiro",
                Ingredientes = "Chocolate, Leite Condensado",
                Preparacao = "Misture o e leve ao fogo.",
                UrlDaImagem = "https://img.itdg.com.br/tdg/images/recipes/000/000/114/75811/75811_original.jpg?mode=crop&width=710&height=400"
            };

            var context = CreateTestContext();
            var controller = new ReceitasController(context);

            // Act
            controller.Post(new ReceitaViewModel()
            {
                Id = brigadeiro.Id,
                Title = brigadeiro.Titulo,
                Description = brigadeiro.Descricao,
                Ingredients = brigadeiro.Ingredientes,
                Preparation = brigadeiro.Preparacao,
                ImageUrl = brigadeiro.UrlDaImagem
            });

            var receita = controller.Get(brigadeiro.Id);
            var receitaDoBanco = context.Receitas.FirstOrDefault(r => r.Id == brigadeiro.Id);

            //Assert
            receita.Value.Should().BeEquivalentTo(new ReceitaViewModel()
            {
                Id = brigadeiro.Id,
                Title = brigadeiro.Titulo,
                Description = brigadeiro.Descricao,
                Ingredients = brigadeiro.Ingredientes,
                Preparation = brigadeiro.Preparacao,
                ImageUrl = brigadeiro.UrlDaImagem
            });
            receitaDoBanco.Should().NotBeNull();
        }

        [Fact]
        public void Delete_DeveDeletarReceita()
        {
            //criar receita para testar
            var brigadeiro = new Receita() {
                Id = 5,
                Titulo = "Brigadeiro",
                Descricao = "Um belo brigadeiro",
                Ingredientes = "Chocolate, Leite Condensado",
                Preparacao = "Misture o e leve ao fogo.",
                UrlDaImagem = "https://img.itdg.com.br/tdg/images/recipes/000/000/114/75811/75811_original.jpg?mode=crop&width=710&height=400"
            };

            var context = CreateTestContext();
            context.AddRange(brigadeiro);
            context.SaveChanges();
            var controller = new ReceitasController(context);

            // Act
            controller.Delete(brigadeiro.Id);

            // var receita = controller.Get(brigadeiro.Id);
            var receitaDoBanco = context.Receitas.FirstOrDefault(r => r.Id == brigadeiro.Id);

            //Assert
            receitaDoBanco.Should().BeNull();
        }

        [Fact]
        public void Put_DeveAtualizarReceita() {
            //criar receita para testar
            var brigadeiro = new Receita() {
                Id = 6,
                Titulo = "Brigadeiro",
                Descricao = "Um belo briagdeiro",
                Ingredientes = "Chocolate, Leite Condensado",
                Preparacao = "Misture o e leve ao fogo.",
                UrlDaImagem = "https://img.itdg.com.br/tdg/images/recipes/000/000/114/75811/75811_original.jpg?mode=crop&width=710&height=400"
            };

            var context = CreateTestContext();
            context.AddRange(brigadeiro);
            context.SaveChanges();
            var controller = new ReceitasController(context);

            var titulo = brigadeiro.Titulo + "modificado";
            var descricao = brigadeiro.Descricao + " modificado";
            

            controller.Put(brigadeiro.Id, new ReceitaViewModel() {
                Id = brigadeiro.Id,
                Title = "Titulo modificado",
                Description = "Descrição modificada",
                Ingredients = brigadeiro.Ingredientes,
                Preparation = brigadeiro.Preparacao,
                ImageUrl = brigadeiro.UrlDaImagem
            });

            var receitaDoBanco = context.Receitas.FirstOrDefault(r => r.Id == brigadeiro.Id);

            //Assert
            receitaDoBanco.Should().BeEquivalentTo(new Receita(){
                Id = brigadeiro.Id,
                Titulo = "Titulo modificado",
                Descricao = "Descrição modificada",
                Ingredientes = brigadeiro.Ingredientes,
                Preparacao = brigadeiro.Preparacao,
                UrlDaImagem = brigadeiro.UrlDaImagem
            });

            context.Receitas.Remove(receitaDoBanco);
            context.SaveChanges();
        }
        
        [Fact]
        public void Post_DeveRetornarUmaExeception()
        {
            //Arrange
            var brigadeiro = new Receita()
            {
                Id = 4,
                Titulo = "Brigadeiro com nome grande",
                Descricao = "Um belo briagdeiro com nome grande",
                Ingredientes = "Chocolate, Leite Condensado, Nome grande",
                Preparacao = "Misture o e leve a um fogo GRANDE.",
                UrlDaImagem = "fake.com/brigadeiro_grande"
            };

            var context = CreateTestContext();
            var controller = new ReceitasController(context);

            // Act
            Action act = () => controller.Post(new ReceitaViewModel() {
                Id = brigadeiro.Id,
                Title = brigadeiro.Titulo,
                Description = brigadeiro.Descricao,
                Ingredients = brigadeiro.Ingredientes,
                Preparation = brigadeiro.Preparacao,
                ImageUrl = brigadeiro.UrlDaImagem
            });
            act.Should().Throw<Exception>().WithMessage("Passa o titulo direito");
        }
    }
}
