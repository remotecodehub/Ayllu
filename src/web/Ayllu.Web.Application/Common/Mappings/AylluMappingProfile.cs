using AutoMapper; 
using Ayllu.Web.Application.Identity.Dtos;
using Ayllu.Web.Application.Identity.Responses; 

namespace Ayllu.Web.Application.Common.Mappings;

public class AylluMappingProfile : Profile
{
    public AylluMappingProfile()
    { 
        CreateMap<UserDto, ApplicationUserResponse>();
    }
}
