using Financas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financas.Models
{
    public class MovimentacoesPorUsuarioModel
    {
        public int? UsuarioId { get; set; }
        public IList<Movimentacao> Movimentacoes { get; set; }
        public IList<Usuario> Usuarios { get; set; } //No formulário, queremos utilizar um combo box para escolhermos o usuário para o relatório, então adicionaremos uma nova propriedade na view model que guardará a lista de usuários:

    }
}