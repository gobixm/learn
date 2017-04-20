namespace tests.entities
{
    public class NodeLink : Link
    {
        public virtual Node Node1 { get; set; }

        public virtual Node Node2 { get; set; }

        public override Entity Entity1 { get; set; }
        public override Entity Entity2 { get; set; }
    }
}