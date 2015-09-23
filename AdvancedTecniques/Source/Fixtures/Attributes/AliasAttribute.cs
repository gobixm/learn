using System;

namespace Fixtures.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class AliasAttribute : Attribute
    {
        private string _alias;

        public AliasAttribute(string @alias)
        {
            Alias = alias;
        }

        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }
    }
}