using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteServicos.Models;
using TesteServicos.ViewModels;

namespace TesteServicos.AutoMapper
{
    internal class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Servico, ServicoViewModel>();
            CreateMap<Cliente, ClienteViewModel>();
            CreateMap<TipoServico, TipoServicoViewModel>();
        }
    }
}