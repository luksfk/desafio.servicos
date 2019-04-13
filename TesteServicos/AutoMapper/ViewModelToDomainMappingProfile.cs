using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteServicos.Models;
using TesteServicos.ViewModels;

namespace TesteServicos.AutoMapper
{
    internal class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ServicoViewModel, Servico>();
            CreateMap<ClienteViewModel, Cliente>();
            CreateMap<TipoServicoViewModel, TipoServico>();
        }
    }
}