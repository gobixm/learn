using System;

namespace Infotecs.Attika.AttikaSharedDataObjects.Mappings
{
    public interface IMapper
    {
        T Map<T>(object source);
        IMapper Configuration(Action configure);
        void Configure();
    }
}