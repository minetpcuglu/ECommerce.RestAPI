using AutoMapper;
using ECommerce.Domain.Entities.Security;
using ECommerce.Shared.DataTransferObjects.Security;

namespace ECommerce.Data.EF.Mappings
{
    public static class ModelMapper
    {
        private static IMapper _mapper;

        public static IMapper Mapper
        {
            get
            {
                if (_mapper != null) return _mapper;

                var config = new MapperConfiguration(cfg =>
                {
                    _ = cfg.CreateMap<User, UserDTO>();
                    _ = cfg.CreateMap<User, UserDTO>().ReverseMap();

                    ////_ = cfg.CreateMap<Sunucu, SunucuDTO>();
                    ////_ = cfg.CreateMap<Sunucu, SunucuDTO>().ReverseMap();

                    ////_ = cfg.CreateMap<SunucuVeriTabani, SunucuVeriTabaniDTO>();
                    ////_ = cfg.CreateMap<SunucuVeriTabani, SunucuVeriTabaniDTO>().ReverseMap();

                    _ = cfg.CreateMap<Role, RoleDTO>();
                    _ = cfg.CreateMap<Role, RoleDTO>().ReverseMap();

                    _ = cfg.CreateMap<RoleAuthorization, RoleAuthorizationDTO>();
                    _ = cfg.CreateMap<RoleAuthorization, RoleAuthorizationDTO>().ReverseMap();

                    _ = cfg.CreateMap<Authorization, AuthorizationDTO>();
                    _ = cfg.CreateMap<Authorization, AuthorizationDTO>().ReverseMap();

                    _ = cfg.CreateMap<UserRole, UserRoleDTO>();
                    _ = cfg.CreateMap<UserRole, UserRoleDTO>().ReverseMap();

                    //_ = cfg.CreateMap<Eposta, EpostaDTO>();
                    //_ = cfg.CreateMap<Eposta, EpostaDTO>().ReverseMap();

                    //_ = cfg.CreateMap<EpostaLog, EpostaLogDTO>();
                    //_ = cfg.CreateMap<EpostaLog, EpostaLogDTO>().ReverseMap();

                });

                _mapper = config.CreateMapper();
                return _mapper;
            }
        }


    }
}
