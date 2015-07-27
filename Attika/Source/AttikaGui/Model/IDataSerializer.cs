namespace Infotecs.Attika.AttikaGui.Model
{
    public interface IDataSerializer
    {
        byte[] Serialize(object dto);
        T Deserialize<T>(byte[] data);
    }
}