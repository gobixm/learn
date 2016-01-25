using System.Collections.Generic;

namespace HyberBench.Orm
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}