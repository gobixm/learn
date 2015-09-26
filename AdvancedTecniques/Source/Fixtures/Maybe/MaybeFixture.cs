namespace Fixtures.Maybe
{
    public class MaybeFixture
    {
        public MaybeFixture Child { get; set; }
        public string Value { get; set; }

        public MaybeFixture SpawnChild()
        {
            return Child = new MaybeFixture();
        }
    }
}