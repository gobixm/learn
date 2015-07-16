using System;

namespace Infotecs.GangOfFour.Proxy
{
    internal class ControlledResource : IResource
    {
        private readonly IResource _resource;
        private readonly string _yourName;

        public ControlledResource(IResource resource, string yourName)
        {
            _resource = resource;
            _yourName = yourName;
        }

        public void Erase()
        {
            if (!_yourName.StartsWith("I"))
            {
                Console.WriteLine("User {0} tryed to eraze resource information. Access denied", _yourName);
            }
            else
            {
                Console.WriteLine("User {0} granted to delete resource information", _yourName);
                _resource.Erase();
            }
        }
    }
}
