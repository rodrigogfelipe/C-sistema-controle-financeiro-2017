using Financas.DAO;
using Financas.Entidades;
using Financas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace Financas.Controllers
{  //O controller que cuidará do cadastro e listagem de usuários, o UsuarioController:
    public class UsuarioController : Controller
    {
        private UsuarioDAO usuarioDAO;

        public UsuarioController(UsuarioDAO usuarioDAO)
        {
            this.usuarioDAO = usuarioDAO;
        }

        public ActionResult Form()  //FOrm que mostrará o formulário de cadastro para o usuário:
        {
            return View();
        }

        public ActionResult Index() //action Index do controller liste os usuários do banco de dados:
        {
            return View(usuarioDAO.Lista());
        }

        public ActionResult Adiciona(UsuarioModel model)
        {
            if (ModelState.IsValid)
            {   // grava o usuário no banco e cria
                // a conta de login com o simple membership
                try
                {
                    WebSecurity.CreateUserAndAccount(model.Nome, model.Senha, //No CreateUserAndAccount precisamos passar como argumentos o login e a senha que serão 
                                                                             //utilizados além de um objeto anônimo cujas informações serão adicionadas na tabela Usuarios do Entity Framework:
                        new { Email = model.Email });
                    return RedirectToAction("Index");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("usuario.Invalido", e.Message);
                    return View("Form", model);
                }
            }
            else
            {
                return View("Form", model);
            }
        }
    }
}