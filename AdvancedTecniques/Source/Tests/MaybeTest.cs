using Fixtures.Maybe;
using NUnit.Framework;

namespace Tests
{
    public class MaybeTest
    {
        [Test]
        public void ShouldNotThrowNull()
        {
            const string expected = "well done";
            var hierarchy = new MaybeFixture();
            hierarchy.SpawnChild()
                .SpawnChild()
                .Value = expected;
            Assert.AreEqual(expected, hierarchy.Child.Child.Value);
            hierarchy.Child = null;
            Assert.IsNull(hierarchy.With(x => x.Child)
                .With(x => x.Child)
                .With(x => x.Value));

        }
    }
}