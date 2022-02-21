using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ApiCatalogo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutosController(AppDbContext contexto)
        {
            _context = contexto;
        }

        // api/produtos
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            //AsNoTracking desabilita o gerenciamento do estado das entidades
            //so deve ser usado em consultas sem alteração
            //return _context.Produtos.AsNoTracking().ToList();
            return _context.Produtos.ToList();
        }

        // api/produtos/1
        [HttpGet("{id}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            //AsNoTracking desabilita o gerenciamento do estado das entidades
            //so deve ser usado em consultas sem alteração
            //var produto = _context.Produtos.AsNoTracking()
            //    .FirstOrDefault(p => p.ProdutoId == id);
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto == null)
            {
                return NotFound();
            }
            return produto;
        }

        //  api/produtos
        [HttpPost]
        public ActionResult Post([FromBody]Produto produto)
        {
            //a validação do ModelState é feito automaticamente
            //quando aplicamos o atributo [ApiController]
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produto);
        }

        // api/produtos/1
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            //a validação do ModelState é feito automaticamente
            //quando aplicamos o atributo [ApiController]
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        //  api/produtos/1
        [HttpDelete("{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            // Usar o método Find é uma opção para localizar 
            // o produto pelo id (não suporta AsNoTracking)
            //var produto = _context.Produtos.Find(id);
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return produto;
        }
    }
}