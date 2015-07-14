namespace Infotecs.GangOfFour.AbstractFactory
{
    internal sealed class LuxuryBread : IProduct
    {
        public string Name
        {
            get { return this.GetType().Name; }
        }
    }
}