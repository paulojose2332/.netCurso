using System;

namespace CursoNetCoreQualyteam.Dominio {
    public class Receita {
        public const int LimiteDeCaracteresDoTitulo = 15;
        public Receita() {}
        public Receita(int id, string titulo, string descricao, string ingredientes, string preparacao, string urlDaImagem) {
            if(!CaracteresDoTituloEhValido(titulo))
                throw new Exception("Passa o titulo direito");

            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            Ingredientes = ingredientes;
            Preparacao = preparacao;
            UrlDaImagem = urlDaImagem;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Ingredientes { get; set; }
        public string Preparacao { get; set; }
        public string UrlDaImagem { get; set; }

        public bool CaracteresDoTituloEhValido(string titulo){
            return titulo.Length > 0 && titulo.Length <= LimiteDeCaracteresDoTitulo;
        }
    }
}
