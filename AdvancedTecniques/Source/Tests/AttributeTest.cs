using Fixtures.Attributes;
using NUnit.Framework;

namespace Tests
{
    internal class AttributeTest
    {
        [Test]
        public void ShoudRespectAttributes()
        {
            var pool = new DispatcherPool();
            Assert.AreEqual(typeof(FirstDispatcher), pool[typeof(FirstDispatcher).Name].GetType());
            Assert.AreEqual(typeof(FirstDispatcher), pool["ПервыйОбработчик"].GetType());
            Assert.AreEqual(typeof(FirstDispatcher), pool["DefaultDispatcher"].GetType());
            Assert.IsNull(pool["nonexisting"]);
        }
    }
}