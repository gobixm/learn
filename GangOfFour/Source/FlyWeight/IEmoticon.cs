namespace Infotecs.GangOfFour.FlyWeight
{
    internal interface IEmoticon
    {
        void Draw(int width, int height);
        void Load(string fileName);
    }
}