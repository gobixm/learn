namespace Infotecs.Attika.AttikaGui.DataService
{
    public interface IDataSerializer
    {
        byte[] Serialize(object dto);
        T Deserialize<T>(byte[] data);
    }
}