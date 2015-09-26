using Fixtures.Maybe;
using NUnit.Framework;

namespace Tests
{
    public class MaybeTest
    {
        private const string WellDone = "well done";

        [Test]
        public void ShouldNotThrowNull()
        {
            var hierarchy = BuildFixture();
            BrakeFixture(hierarchy);
            Assert.IsNull(hierarchy.With(x => x.Child)
                .With(x => x.Child)
                .With(x => x.Value));
        }

        [Test]
        public void ShouldReturnDefaultValue()
        {
            var hierarchy = BuildFixture();
            BrakeFixture(hierarchy);
            Assert.AreEqual("default", hierarchy.With(x => x.Child)
                .With(x => x.Child)
                .Return(x=>x.Value, "default"));
        }

        private static void BrakeFixture(MaybeFixture hierarchy)
        {
            hierarchy.Child = null;
        }

        private static MaybeFixture BuildFixture()
        {
            var hierarchy = new MaybeFixture();
            hierarchy.SpawnChild()
                .SpawnChild()
                .Value = WellDone;
            return hierarchy;
        }
    }
}