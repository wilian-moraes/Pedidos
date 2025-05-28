using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pedidos.Models
{
    public record Dimensoes(
        int Altura,
        int Largura,
        int Comprimento);

    public class Pedido
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
    }

    public class Produto
    {
        public string Produto_id { get; set; }

        public Dimensoes Dimensoes { get; set; }
    }

    public class PedidoEntrada
    {
        public int Pedido_id { get; set; }

        public List<Produto> Produtos { get; set; }
    }

    public class Entrada
    {
        public List<PedidoEntrada> Pedidos { get; set; }
    }

    public class CaixaSaida
    {
        public string Caixa_id { get; set; }

        public List<string> Produtos { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Observacao { get; set; }
    }

    public class PedidoSaida
    {
        public int Pedido_id { get; set; }

        public List<CaixaSaida> Caixas { get; set; }
    }

    public class Saida
    {
        public List<PedidoSaida> Pedidos { get; set; }
    }

    public class Caixa
    {
        public string Id { get; }
        public int Altura { get; }
        public int Largura { get; }
        public int Comprimento { get; }
        public int Volume => Altura * Largura * Comprimento;

        public Caixa(string id, int altura, int largura, int comprimento)
        {
            Id = id;
            Altura = altura;
            Largura = largura;
            Comprimento = comprimento;
        }
    }

    public class GrupoCaixa
    {
        public Dimensoes Dimensoes { get; set; }
        public Caixa Caixa { get; set; }
        public List<string> Produtos { get; set; }
    }
}