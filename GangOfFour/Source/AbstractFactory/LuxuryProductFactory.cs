namespace Infotecs.GangOfFour.AbstractFactory
{
    internal sealed class LuxuryProductFactory : ProductFactory
    {
        public override IProduct CreateBread()
        {
            return new LuxuryBread();
        }

        public override IProduct CreateMilk()
        {
            return new LuxuryMilk();
        }
    }
}