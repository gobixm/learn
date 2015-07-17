namespace Infotecs.SOLID.Liskov
{
    /// <summary>
    ///     <exception cref="SpecificTunnelNotExistsException"></exception>
    /// </summary>
    internal class SpecificTunnel : Tunnel
    {
        public override void Open(string connectionString)
        {
            throw new SpecificTunnelNotExistsException();
        }
    }
}