using System;
using System.Web;
using TesteServicos.DAL.Interfaces;
using TesteServicos.DAL.Repositories;
using TesteServicos.Models;
using TesteServicos.Utils;

namespace TesteServicos.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext context;
        private ServicoRepository servicoRepository;
        private ClienteRepository clienteRepository;
        private TipoServicoRepository tipoServicoRepository;
        private IUserService userService;

        public UnitOfWork(IUserService userService)
        {
            this.userService = userService;
            context = new ApplicationDbContext();
        }

        public ITipoServicoRepository TipoServicoRepository
        {
            get
            {
                if (this.tipoServicoRepository == null)
                {
                    this.tipoServicoRepository = new TipoServicoRepository(context, userService);
                }
                return tipoServicoRepository;
            }
        }

        public IClienteRepository ClienteRepository
        {
            get
            {
                if (this.clienteRepository == null)
                {
                    this.clienteRepository = new ClienteRepository(context, userService);
                }
                return clienteRepository;
            }
        }

        public IServicoRepository ServicoRepository
        {
            get
            {
                if (this.servicoRepository == null)
                {
                    this.servicoRepository = new ServicoRepository(context, userService);
                }
                return servicoRepository;
            }
        }
        

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}