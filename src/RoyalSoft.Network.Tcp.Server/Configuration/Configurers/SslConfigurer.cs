#region using

using System;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using RoyalSoft.Network.Core.Configuration;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Configurers
{
    public class SslConfigurer : BaseConfigurer<ISslConfiguration>
    {
        public SslConfigurer(ISslConfiguration component)
            : base(component)
        {}

        public SslConfigurer Certificate(X509Certificate certificate)
        {
            if(certificate == null)
                throw new ArgumentNullException(nameof(certificate));

            Component.Certificate = certificate;
            return this;
        }

        public SslConfigurer Protocols(SslProtocols protocols)
        {
            Component.Protocols = protocols;
            return this;
        }

        public SslConfigurer CheckCertificateRevocation()
        {
            Component.CheckCertificateRevocation = true;
            return this;
        }

        public SslConfigurer ClientCertificateRequired()
        {
            Component.ClientCertificateIsRequired = true;
            return this;
        }
    }
}
