using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CaelumEstoque.filtros
{ //Filtros são componentes que conseguem executar lógicas antes e depois do código do controller.
    public class AutorizacaoFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext) //No código do OnActionExecuting, se o usuário estiver logado, queremos deixar a 
          //lógica do controller executar normalmente, senão precisamos redirecionar o usuário para a página de login da aplicação. 
         //Dentro desse método, recebemos um argumento do tipo ActionExecutingContext,dentro desse objeto podemos conseguir informações sobre a requisição que está sendo tratada atualmente.
        {

            object usuario = filterContext.HttpContext.Session["usuarioLogado"]; //Então precisamos inicialmente acessar a sessão do servidor para buscar o usuário 
            //logado. Quando queremos acessar a sessão do servidor a partir do código do filtro, precisamos ler a propriedade HttpContext do filterContext e dentro do HttpContext, podemos acessar a Session do servidor:
            if (usuario == null)
            {
                //No caso em que o usuário não está logado, queremos escrever no Result um resultado fará um redirect no navegador do usuário, porém dentro do filtro 
                //não podemos utilizar o método RedirectToAction da mesma forma que fazíamos no controller, precisamos instanciar diretamente o objeto devolvido pelo 
                //RedirectToAction que é o RedirectToRouteResult.

                filterContext.Result = new RedirectToRouteResult( //Dentro do RedirectToRouteResult, precisamos passar o nome do controller e da action para onde 
                    //queremos enviar o usuário. Essas informações são enviadas dentro de um objeto do tipo RouteValueDictionary e dentro desse dicionário precisamos passar as informações dentro de um objeto anônimo do C#:
                    new RouteValueDictionary(
                    new {controller = "Login", action = "index"}

                    )
              );
            }
        }
    }
}