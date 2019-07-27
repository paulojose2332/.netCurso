using System;
using FluentAssertions;
using Xunit;

namespace CursoNetCoreQualyteam.Controllers.Tests {
    public class ReceitasControllerTest {
        [Fact]
        public void GetAll_DeveResponderComTodasAsReceitasCadastradas() {
            // insere receitas
            //Arrange
            var controller = new ReceitasController();

            //Act
            var receitas = controller.Get();

            //Assert
            receitas.Value.Should().BeEquivalentTo(new ReceitaViewModel[]{
                new ReceitaViewModel(){
                    Id = 1,
                    Title = "Feijão com Arroz",
                    Description = "Um belo prato de feijão com arroz que alimenta todo bom brasileiro",
                    Ingredients = "Feijão, Arroz",
                    Preparation = "Mistureo feijão com o arroz e pronto.",
                    ImageUrl = "fakeurl.com/feijaocomarroz"
                },
                new ReceitaViewModel(){
                    Id = 2,
                    Title = "Feijão com Arroz",
                    Description = "Um belo prato de feijão com arroz que alimenta todo bom brasileiro",
                    Ingredients = "Feijão, Arroz",
                    Preparation = "Mistureo feijão com o arroz e pronto.",
                    ImageUrl = "fakeurl.com/feijaocomarroz"
                }
            });
        }
    }
}
