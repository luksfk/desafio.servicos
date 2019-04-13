using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TesteServicos.DAL.Interfaces;
using TesteServicos.Models;
using TesteServicos.Utils;

namespace TesteServicos.DAL.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ApplicationDbContext context, IUserService userService) : base(context, userService)
        {
        }
    }
}