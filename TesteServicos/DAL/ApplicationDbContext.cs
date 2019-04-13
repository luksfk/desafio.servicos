using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TesteServicos.Models;
using TesteServicos.ViewModels;

namespace TesteServicos.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {            
            Database.SetInitializer<ApplicationDbContext>(new ApplicationInitializerContext());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        public System.Data.Entity.DbSet<TesteServicos.Models.Servico> Servicoes { get; set; }

        public System.Data.Entity.DbSet<TesteServicos.Models.TipoServico> TipoServicoes { get; set; }

        public System.Data.Entity.DbSet<TesteServicos.ViewModels.ClienteViewModel> ClienteViewModels { get; set; }
    }
}