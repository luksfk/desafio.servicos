using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using TesteServicos.Models;

namespace TesteServicos.DAL
{
    public class ApplicationInitializerContext : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "Administrator" });
            context.SaveChanges();

            base.Seed(context);
            //ALTERADO PARA TRIGGER NA TABELA DE FORNECEDORES
            //NECESSÀRIO RODAR SCRIPT INITDATABASE


            //for (int i = 1; i <= 3; i++)
            //{
            //    var cliente = new Cliente { Nome = $"Cliente {i}", Bairro = "Centro", Cidade = "Jaraguá do Sul", Estado = "Santa Catarina" };
            //    context.Clientes.Add(cliente);
            //}

            //for (int i = 4; i <= 6; i++)
            //{
            //    var cliente = new Cliente { Nome = $"Cliente {i}", Bairro = "Corticeira", Cidade = "Guaramirim", Estado = "Santa Catarina" };
            //    context.Clientes.Add(cliente);
            //}

            //context.SaveChanges();

        }
    }
}