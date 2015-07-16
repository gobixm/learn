using System;

namespace Infotecs.GangOfFour.FactoryMethod
{
    internal interface ICreator
    {
        IProduct CreateProduct(string productName);
    }
}
