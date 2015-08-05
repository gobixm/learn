using System;

namespace Infotecs.Attika.AttikaGui.DataServices
{
    public interface IDataSerializer
    {
        T Deserialize<T>(byte[] data);
        byte[] Serialize(object dto);
    }
}
