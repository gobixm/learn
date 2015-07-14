namespace Infotecs.GangOfFour.FactoryMethod
{
    internal class Creator<T> : ICreator where T:IProduct, new()
    {

        public IProduct CreateProduct(string productName)
        {
            return new T()
            {
                Name = typeof(T).Name+" "+productName
            };
        }
    }
}