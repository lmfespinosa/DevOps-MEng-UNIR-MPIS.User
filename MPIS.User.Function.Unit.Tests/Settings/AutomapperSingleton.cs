#region "Libraries"

using AutoMapper;
using MPIS.User.AutoMapper.Profiles;

#endregion

namespace MPIS.User.Function.Unit.Tests.Settings
{
    public class AutomapperSingleton
    {
        private static IMapper _mapper;

        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    // Auto Mapper Configurations
                    var mappingConfig = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new UserProfile());

                    });

                    IMapper mapper = mappingConfig.CreateMapper();
                    _mapper = mapper;
                }

                return _mapper;
            }
        }
    }
}
