#region using

using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;
using RoyalSoft.Network.Tcp.Server.Internal.Configuration;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Configurers
{
    public class SslConfigurer : BaseConfigurer<ISsl>
    {
        private X509Certificate _certificate;
        private SslProtocols _protocols;
        private EncryptionPolicy _encryptionPolicy;
        private bool _checkCertificateRevocation, _clientCertificateIsRequired;

        public SslConfigurer Certificate(X509Certificate certificate)
        {
            if(certificate == null)
                throw new ArgumentNullException(nameof(certificate));

            _certificate = certificate;
            return this;
        }

        public SslConfigurer Protocols(SslProtocols protocols)
        {
            _protocols = protocols;
            return this;
        }

        public SslConfigurer CheckCertificateRevocation()
        {
            _checkCertificateRevocation = true;
            return this;
        }

        public SslConfigurer ClientCertificateRequired()
        {
            _clientCertificateIsRequired = true;
            return this;
        }

        public SslConfigurer Policy(EncryptionPolicy policy)
        {
            _encryptionPolicy = policy;
            return this;
        }

        internal override ISsl Build()
        {
            return 
                new Ssl
                {
                    Enabled = true,
                    Certificate = _certificate,
                    Protocols = _protocols == SslProtocols.None ? SslProtocols.Default : _protocols,
                    EncryptionPolicy = _encryptionPolicy,
                    CheckCertificateRevocation = _checkCertificateRevocation,
                    ClientCertificateIsRequired = _clientCertificateIsRequired
                };
        }
    }
}
