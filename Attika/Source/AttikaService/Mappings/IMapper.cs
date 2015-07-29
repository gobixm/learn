using System;

namespace Infotecs.Attika.AttikaService.Mappings
{
    public interface IMapper
    {
        T Map<T>(object source);
        IMapper Configuration(Action configure);
        IMapper Configure();
    }
}