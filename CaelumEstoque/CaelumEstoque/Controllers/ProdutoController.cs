using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using CaelumEstoque.filtros;

namespace CaelumEstoque.Controllers
{
    [AutorizacaoFilter]
    public class ProdutoController : Controller
    {
        [Route("produtos", Name ="ListaProdutos")] //Vamos fazer com que a listagem de produtos seja servida pela URL /produtos e que a visualização por 
        //produtos/{produtoId}.essa anotação é utilizada sobre actions dos controllers e recebe como argumento a nova url do método../
         public ActionResult Index()

        {
                ProdutosDAO dao = new ProdutosDAO(); //Logo, precisamos instanciar um DAO que saiba acessar os produtos no banco, o ProdutosDAO....
                IList<Produto> produtos = dao.Lista(); //Lista que retorna um IList<Produto>. Utilizaremos esse método para recuperar a lista de produtos do banco de dados...
                ViewBag.Produtos = produtos; //Toda informação que colocamos dentro da ViewBag pode ser acessada pela camada de visualização, pois ela também tem acesso
                //ao mesmo objeto ViewBag que utilizamos no controller. Com a ViewBag, podemos enviar qualquer número de objetos para a camada de visualização....
                return View(produtos);
        }
        

        public ActionResult Form() // Form redireciona ao Index Form../
        {
            CategoriasDAO categoriasDAO = new CategoriasDAO();
            IList<CategoriaDoProduto> categorias = categoriasDAO.Lista();
            ViewBag.Categorias = categorias;
            ViewBag.Produto = new Produto();
            return View();
        }

        [HttpPost] //Da mesma forma que podemos utilizar o HttpPostAttribute para aceitar apenas requisições do tipo post
        public ActionResult Adiciona(Produto produto) // Metado adiciona  será responsável por receber as informações que foram enviadas pelo formulário Produto produto...
        {
            int idDaInformatica = 1;
            if(produto.CategoriaId.Equals(idDaInformatica) && produto.Preco < 100)
            {
                ModelState.AddModelError("produto.InformaticaComPrecoInvalido", "Produtos da categoria informática devem ter preço maior do que 100"); //o método AddModelError do ModelState.
                //Esse método recebe como argumento duas strings, a primeira indica o nome do erro de validação e a segunda a mensagem de erro que deve ser exibida.
                //O nome do erro pode ser utilizado na view para se recuperar a mensagem de erro através do método ValidationMessage do HtmlHelper.
            }

            //O ModelState guarda todas as regras de validação que foram violadas. Para verificar se todas as regras de validação foram obedecidas utiliza-se o 
            //atributo IsValid do ModelState.
            if (ModelState.IsValid)
            { // O Modelo foi validado corretamente, então pode ser gravado no banco de dados.

                ProdutosDAO dao = new ProdutosDAO();
                dao.Adiciona(produto); //utilizaremos o DAO para adicionar o produto no banco de dados:
                return RedirectToAction("Index", "Produto"); //O RedirectToAction redirecionará o usuário para o método Index do controller atual (ProdutoController).
            }
            else  // O Modelo não foi validado corretamente.
            {
                // produto inválido então vamos guardá-lo na ViewBag
                ViewBag.Produto = produto;
                // envia o usuário para o formulário de cadastro
                CategoriasDAO categoriasDAO = new CategoriasDAO();
                ViewBag.Categorias = categoriasDAO.Lista();
                return View("Form");
            }
        }

        [Route("produtos/{id}", Name="VisualizaProduto")] //Para capturarmos uma variável da url, precisamos colocar o nome da variável que queremos capturar dentro de chaves, {id} para capturar uma variável chamada id.
        public ActionResult Visualiza(int id) //recebe um id, busca no banco o produto com Id igual ao recebido e envia essa informação para ser exibida na view.
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto = dao.BuscaPorId(id);
            ViewBag.Produto = produto;
            return View();
        }

        public ActionResult DecrementaQtd(int id) //vamos desenvolver uma nova action chamada DecrementaQtd que recebe o id do produto que será decrementado:
            //Dentro desse método, precisamos buscar o produto do banco de dados, decrementar sua quantidade, atualizar as informações e por fim redirecionar o
            //usuário para a página de listagem de produtos para que ele veja as informações atualizadas:
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto = dao.BuscaPorId(id);
            produto.Quantidade--;
            dao.Atualiza(produto);
            return Json(produto); //Para devolvermos o Json do produto do servidor, utilizamos mais um método herdado da classe Controller chamado Json passando qual é o objeto que queremos devolver como resposta:

        }
      
    }
}