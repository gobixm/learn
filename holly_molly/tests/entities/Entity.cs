using System;

namespace tests.entities
{
    public abstract class Entity
    {
        public virtual Guid Id { get; set; } = Guid.NewGuid();
    }
}