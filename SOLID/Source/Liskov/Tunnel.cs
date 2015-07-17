namespace Infotecs.SOLID.Liskov
{
    /// <summary>
    ///     <exception cref="TunnelNotExistsException"></exception>
    /// </summary>
    internal class Tunnel
    {
        public virtual void Open(string connectionString)
        {
            throw new TunnelNotExistsException();
        }
    }
}