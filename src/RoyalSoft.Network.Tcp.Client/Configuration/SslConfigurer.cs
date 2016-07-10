#region using

using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

#endregion

namespace RoyalSoft.Network.Tcp.Client.Configuration
{
    public class SslConfigurer
    {
        public SslConfigurer Certificate(X509Certificate certificate)
        {
            return this;
        }

        public SslConfigurer Protocols(SslProtocols protocols)
        {
            return this;
        }

        public SslConfigurer CheckRevocationList(bool checkRevocationList)
        {
            return this;
        }

        public SslConfigurer ClientCertificateRequired(bool required)
        {
            return this;
        }
    }
}
