using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;


namespace Pedidos.Controllers
{
    [ApiController]
    [Route("[controller]")]
	// [Authorize]
    public class PedidosController : ControllerBase
    {
        private static readonly List<Caixa> CaixasDisponiveis = new()
        {
            new Caixa("Caixa 1", 30, 40, 80),
            new Caixa("Caixa 2", 80, 50, 40),
            new Caixa("Caixa 3", 50, 80, 60)
        };

        [HttpPost]
        public ActionResult<Saida> ProcessarPedidos([FromBody] Entrada entrada)
        {
            var resposta = new Saida
            {
                Pedidos = entrada.Pedidos.Select(ProcessarPedido).ToList()
            };

			return new JsonResult(resposta)
			{
				ContentType = "application/json; charset=utf-8"
			};
        }

        private PedidoSaida ProcessarPedido(PedidoEntrada pedido)
        {
            var resultado = new PedidoSaida
            {
                Pedido_id = pedido.Pedido_id,
                Caixas = new List<CaixaSaida>()
            };

            var produtosParaEmpacotar = new List<Produto>(pedido.Produtos);
            var produtosInvalidos = new List<Produto>();

            produtosParaEmpacotar.RemoveAll(p =>
            {
                if (!ProdutoCabeEmAlgumaCaixa(p.Dimensoes))
                {
                    produtosInvalidos.Add(p);
                    return true;
                }
                return false;
            });

            var produtosOrdenados = produtosParaEmpacotar
                .OrderByDescending(p => p.Dimensoes.Altura * p.Dimensoes.Largura * p.Dimensoes.Comprimento)
                .ToList();

            var caixasUsadas = new List<CaixaSaida>();
            foreach (var produto in produtosOrdenados)
            {
                var caixa = EncontrarCaixaParaProduto(produto.Dimensoes);
                if (caixa == null) continue;

                var caixaExistente = caixasUsadas.FirstOrDefault(c => c.Caixa_id == caixa.Id);
                if (caixaExistente == null)
                {
                    caixasUsadas.Add(new CaixaSaida
                    {
                        Caixa_id = caixa.Id,
                        Produtos = new List<string> { produto.Produto_id }
                    });
                }
                else
                {
                    caixaExistente.Produtos.Add(produto.Produto_id);
                }
            }

            if (produtosInvalidos.Any())
            {
                caixasUsadas.Add(new CaixaSaida
                {
                    Produtos = produtosInvalidos.Select(p => p.Produto_id).ToList(),
                    Observacao = "Produto(s) não cabe(m) em nenhuma caixa disponível."
                });
            }

            resultado.Caixas = caixasUsadas;
            return resultado;
        }

        private bool ProdutoCabeEmAlgumaCaixa(Dimensoes dimensoes)
        {
            return CaixasDisponiveis.Any(c =>
                dimensoes.Altura <= c.Altura &&
                dimensoes.Largura <= c.Largura &&
                dimensoes.Comprimento <= c.Comprimento);
        }

        private Caixa EncontrarCaixaParaProduto(Dimensoes dimensoes)
        {
            return CaixasDisponiveis
                .Where(c => c.Altura >= dimensoes.Altura &&
                            c.Largura >= dimensoes.Largura &&
                            c.Comprimento >= dimensoes.Comprimento)
                .OrderBy(c => c.Volume)
                .FirstOrDefault();
        }
    }
}