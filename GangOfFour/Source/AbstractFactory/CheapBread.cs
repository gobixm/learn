namespace Infotecs.GangOfFour.AbstractFactory
{
    internal sealed class CheapBread : IProduct
    {
        public string Name
        {
            get { return this.GetType().Name; }
        }
    }
}