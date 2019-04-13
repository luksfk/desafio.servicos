using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteServicos.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        IServicoRepository ServicoRepository { get; }
        IClienteRepository ClienteRepository { get;  }

        ITipoServicoRepository TipoServicoRepository { get; }
    }
}