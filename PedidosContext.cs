using Microsoft.EntityFrameworkCore;
using Pedidos.Models;

namespace Pedidos
{
    public class PedidosContext : DbContext
    {
        public PedidosContext(DbContextOptions<PedidosContext> options)
            : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }
    }
}
