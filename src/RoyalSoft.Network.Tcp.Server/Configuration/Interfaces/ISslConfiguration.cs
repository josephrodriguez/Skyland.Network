#region using

using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Interfaces
{
    public interface ISslConfiguration
    {
        bool Enabled { get; set; }
        SslProtocols Protocols { get; set; }
        X509Certificate Certificate { get; set; }
        bool CheckCertificateRevocation { get; set; }
        bool ClientCertificateIsRequired { get; set; }
    }
}
