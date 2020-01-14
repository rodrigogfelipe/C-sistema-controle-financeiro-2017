using Financas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financas.DAO
{
    public class UsuarioDAO //ara isso, precisamos inicialmente implementar a classe que será responsável por acessar a tabela de usuários do banco de dados, o UsuarioDAO, que será criado dentro da pasta DAO:
    {
        // código do construtor da classe
        private FinancasContext context;

        public UsuarioDAO(FinancasContext context) //Podemos pedir a instância do contexto do Entity Framework como argumento do construtor do DAO:
        {
            this.context = context; //contexto do Entity Framework, 
        }

        public void Adiciona(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            context.SaveChanges();
        }
        public IList<Usuario> Lista()
        {
            return context.Usuarios.ToList();
        }
    }
}