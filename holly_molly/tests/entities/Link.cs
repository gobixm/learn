using System;

namespace tests.entities
{
    public abstract class Link
    {
        public virtual Guid Id { get; set; } = Guid.NewGuid();

        public abstract Entity Entity1 { get; set; }

        public abstract Entity Entity2 { get; set; }
    }
}