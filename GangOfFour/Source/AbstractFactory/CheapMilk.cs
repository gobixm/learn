namespace Infotecs.GangOfFour.AbstractFactory
{
    internal sealed class CheapMilk : IProduct
    {
        public string Name
        {
            get { return this.GetType().Name; }
        }
    }
}
