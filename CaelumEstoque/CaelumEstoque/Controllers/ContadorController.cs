using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    public class ContadorController : Controller
    {
        // precisamos mostrar para um usuário quantas vezes ele acessou uma determinada página. Então devemos ter alguma forma de manter um contador que depende do usuário que está acessando a página.


        public ActionResult Index()
        {
            object valorNaSession = Session["contador"]; // podemos recuperar o valor associado a uma chave com Session[nomeDaChave], porém como a sessão guarda o tipo object, precisamos converter o valor devolvido para o tipo correto. 
            int contador = Convert.ToInt32(valorNaSession); //No caso do contador, precisamos converter o valor devolvido para o tipo int, essa conversão pode ser feita através da classe Convert:
            contador++;
            Session["contador"] = contador; ////A variável Session funciona como um dicionário de String para object, logo podemos armazenar diversos nomes com valores diferentes por usuário.
            return View(contador);
        }
    }
}