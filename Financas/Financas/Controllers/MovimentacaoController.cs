using Financas.DAO;
using Financas.Entidades;
using Financas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financas.Controllers
{
    [Authorize] // Filter Authorize permite somente usuarios logados possam acessar o Controller movimentacao
    public class MovimentacaoController : Controller
    {
        // implementação da classe
        private MovimentacaoDAO movimentacaoDAO; //precisamos implementar o controller que cuidará do cadastro e lista de movimentações:
        private UsuarioDAO usuarioDAO;

        //Mas no formulário de cadastro de movimentações, queremos escolher qual é o usuário dessa movimentação utilizando um combo box, mas para isso, precisaremos 
        //da lista de usuários na camada de visualização, portanto o construtor do MovimentacaoController precisa receber também um objeto do tipo UsuarioDAO além do MovimentacaoDAO:
        public MovimentacaoController(MovimentacaoDAO movimentacaoDAO, UsuarioDAO usuarioDAO)
        {
            this.movimentacaoDAO = movimentacaoDAO;
            this.usuarioDAO = usuarioDAO;
        }

        public ActionResult Index() //para listar as informações no método Index
        {
            return View(movimentacaoDAO.Lista());
        }

        public ActionResult Form() //o método Form que novamente mostrará o formulário de cadastro para o usuário:
        {
            ViewBag.Usuarios = usuarioDAO.Lista();
            return View();
        }

        public ActionResult Adiciona(Movimentacao movimentacao) //método Adiciona no MovimentacaoController:
        {
            if (ModelState.IsValid)
            {
                movimentacaoDAO.Adiciona(movimentacao);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Usuarios = usuarioDAO.Lista();
                return View("Form", movimentacao);
            }
        }

        public ActionResult MovimentacoesPorUsuario(MovimentacoesPorUsuarioModel model) //Então dentro do MovimentacaoController vamos adicionar um novo método chamado MovimentacoesPorUsuario que deve receber qual é o usuário que queremos utilizar no filtro:
        {
            model.Usuarios = usuarioDAO.Lista(); //vamos criar relatórios com os dados gravados no banco de dados, o primeiro relatório que queremos criar no projeto é uma lista de movimentações por usuário
            model.Movimentacoes = movimentacaoDAO.BuscaPorUsuario(model.UsuarioId);
            return View(model);
        }

        public ActionResult Busca(BuscaMovimentacoesModel model)
        {
            model.Usuarios = usuarioDAO.Lista();
            model.Movimentacoes = movimentacaoDAO.Busca(model.ValorMinimo, model.ValorMaximo,
                                    model.DataMinima, model.DataMaxima,
                                    model.Tipo, model.UsuarioId);
            return View(model);
        }
    }
}