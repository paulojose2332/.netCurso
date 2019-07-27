using System;

namespace CursoNetCoreQualyteam.Dominio {
    public class Receita {
        public const int LimiteDeCaracteresDoTitulo = 20;
        public Receita() {}
        public Receita(int id, string titulo, string descricao, string ingredientes, string preparacao, string urlDaImagem) {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            Ingredientes = ingredientes;
            Preparacao = preparacao;
            UrlDaImagem = urlDaImagem;
        }

        public int Id { get; set; }
        private string _titulo;
        public string Titulo {
            get { return _titulo; }
            set { 
                if(!CaracteresDoTituloEhValido(value)) throw new Exception("Passa o titulo direito");
                _titulo = value;
            }
         }
        public string Descricao { get; set; }
        public string Ingredientes { get; set; }
        public string Preparacao { get; set; }
        public string UrlDaImagem { get; set; }

        public bool CaracteresDoTituloEhValido(string titulo){
            return titulo.Length > 0 && titulo.Length <= LimiteDeCaracteresDoTitulo;
        }
    }
}
