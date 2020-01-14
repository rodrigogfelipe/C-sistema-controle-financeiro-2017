using Financas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financas.Models
    //Além dessa busca por usuários, queremos ser capazes de filtrar a movimentação por diversos critérios: valor mínimo e máximo, data inicial e final, tipo e usuario.
{
    public class BuscaMovimentacoesModel
    {
        public decimal? ValorMinimo { get; set; }

        public decimal? ValorMaximo { get; set; }

        public DateTime? DataMinima { get; set; }

        public DateTime? DataMaxima { get; set; }

        public Tipo? Tipo { get; set; }

        public int? UsuarioId { get; set; }

        public IList<Movimentacao> Movimentacoes { get; set; }

        public IList<Usuario> Usuarios { get; set; }
    }
}