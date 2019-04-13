using System.Web.Mvc;
using TesteServicos.DAL;
using TesteServicos.DAL.Interfaces;
using TesteServicos.DAL.Repositories;
using TesteServicos.Utils;
using Unity;
using Unity.Mvc5;

namespace TesteServicos
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IClienteRepository, ClienteRepository>();
            container.RegisterType<IServicoRepository, ServicoRepository>();
            container.RegisterType<ITipoServicoRepository, TipoServicoRepository>();            

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}