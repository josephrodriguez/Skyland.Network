#region using

using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Internal.Configuration
{
    internal class Ssl : ISslConfiguration
    {
        public bool Enabled { get; set; }
        public SslProtocols Protocols { get; set; }
        public X509Certificate Certificate { get; set; }
        public bool CheckCertificateRevocation { get; set; }
        public bool ClientCertificateIsRequired { get; set; }
        public EncryptionPolicy EncryptionPolicy { get; set; }

        public override string ToString()
        {
            return $"{Protocols}";
        }
    }
}
