using AutoMapper;
using Microsoft.Owin;
using Owin;
using TesteServicos.AutoMapper;

[assembly: OwinStartupAttribute(typeof(TesteServicos.Startup))]
namespace TesteServicos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });
        }
    }
}
