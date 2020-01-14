using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers

{   // LoginController. Dentro dessa classe, a action Index será a responsável por mostrar o formulário de login para o usuário:
    public class LoginController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Autentica(String login, string senha) //Os dados desse formulário serão enviados para a action Autentica do LoginController:
        {
            UsuariosDAO dao = new UsuariosDAO(); //Nessa action, precisamos verificar se as informações que foram enviadas pelo formulário realmente existem dentro do
            //banco de dados, para isso utilizaremos o método Busca do UsuariosDAO../

            Usuario usuario = dao.Busca(login, senha); //Esse método busca um usuário dado o login e a senha. Se as informações estiverem corretas, o método devolve 
            //o Usuario do banco de dados, senão ele devolve a referência nula:
            if (usuario!= null)
            {
                Session["usuarioLogado"] = usuario; //Se o usuario tiver uma referência válida, vamos armazená-lo na sessão do servidor e depois redirecionar para a página inicial da aplicação (a lista de produtos),
                return RedirectToAction("index", "Produto");

            }
            else
            {
                return RedirectToAction("index"); //senão vamos enviar o usuário de volta para a página de login da aplicação.
            }



        }
    }
}