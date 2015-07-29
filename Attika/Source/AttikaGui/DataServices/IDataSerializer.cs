namespace Infotecs.Attika.AttikaGui.DataServices
{
    public interface IDataSerializer
    {
        byte[] Serialize(object dto);
        T Deserialize<T>(byte[] data);
    }
}