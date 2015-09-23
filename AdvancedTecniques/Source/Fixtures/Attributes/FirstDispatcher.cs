namespace Fixtures.Attributes
{
    [Alias("ПервыйОбработчик")]
    [Alias("DefaultDispatcher")]
    public sealed class FirstDispatcher : Dispatcher
    {
        public override void Dispach()
        {
            throw new System.NotImplementedException();
        }
    }
}