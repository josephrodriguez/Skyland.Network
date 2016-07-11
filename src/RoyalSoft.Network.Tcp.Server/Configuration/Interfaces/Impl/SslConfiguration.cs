#region using

using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Interfaces.Impl
{
    internal class SslConfiguration : ISslConfiguration
    {
        public bool Enabled { get; set; }
        public SslProtocols Protocols { get; set; }
        public X509Certificate Certificate { get; set; }
        public bool CheckCertificateRevocation { get; set; }
        public bool ClientCertificateIsRequired { get; set; }

        public override string ToString()
        {
            return $"{Protocols}";
        }
    }
}
