using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TrabalhoComp.Models.DTO.Livros;
using TrabalhoComp.Models.DTO.Usuarios;

namespace TrabalhoComp.Models.DTO.UsuariosLivros
{
    public class UsuarioLivroResponse
    {
        public int Id { get; set; }

        public string NomeUsuario { get; set; }
        public string TituloLivro { get; set; }
        public decimal ValorMulta { get; set; }
        public DateTime DataDevolucao { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public LivroResponse Livro { get; set; }
        public UsuarioResponse Usuario { get; set; }


        public static UsuarioLivroResponse CriarCom(UsuarioLivro usuarioLivro)
        {
            var response = new UsuarioLivroResponse();
            response.Id = usuarioLivro.Id;
            response.ValorMulta = usuarioLivro.ValorMulta;
            response.DataDevolucao = usuarioLivro.DataDevolucao;
            response.DataEmprestimo = usuarioLivro.DataEmprestimo;
            response.NomeUsuario = usuarioLivro.Usuario.Nome;
            response.TituloLivro = usuarioLivro.Livro.Titulo;
            response.Livro = LivroResponse.CriarCom(usuarioLivro.Livro);
            response.Usuario = UsuarioResponse.CriarCom(usuarioLivro.Usuario);
           
            return response;
        }
    }
}