using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Financas.Controllers
{   // LoginController. Dentro dessa classe, a action Index será a responsável por mostrar o formulário de login para o usuário:
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Autentica(String login, String senha) //Agora no método Autentica, utilizaremos o método Login da classe WebSecurity passando o login 
            //e a senha do usuário. Esse método devolve o valor true caso o login seja bem sucedido e false, caso contrário:
        {
            if (WebSecurity.Login(login, senha)) //Para sabermos se o usuário está logado, podemos utilizar a propriedade IsAuthenticated da classe WebSecurity.
            {
                // Usuário está logado
                return RedirectToAction("Index", "Movimentacao");
            }
            else
            {
                // Usuário ainda não está logado
                return View("Index");
            }
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index");
        }
    }
}