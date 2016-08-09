#region using

using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;
using RoyalSoft.Network.Tcp.Server.Internal.Configuration;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Builders
{
    public class SslBuilder : Builder<ISslConfiguration>
    {
        private X509Certificate _certificate;
        private SslProtocols _protocols;
        private EncryptionPolicy _encryptionPolicy;
        private bool _checkCertificateRevocation, _clientCertificateIsRequired, _enabled;

        public SslBuilder Certificate(X509Certificate certificate)
        {
            if(certificate == null)
                throw new ArgumentNullException(nameof(certificate));

            _enabled = true;
            _certificate = certificate;
            return this;
        }

        public SslBuilder Protocols(SslProtocols protocols)
        {
            _enabled = true;

            _protocols = protocols;
            return this;
        }

        public SslBuilder CheckCertificateRevocation()
        {
            _enabled = true;
            _checkCertificateRevocation = true;
            return this;
        }

        public SslBuilder ClientCertificateRequired()
        {
            _enabled = true;
            _clientCertificateIsRequired = true;
            return this;
        }

        public SslBuilder Policy(EncryptionPolicy policy)
        {
            _enabled = true;
            _encryptionPolicy = policy;
            return this;
        }

        internal override ISslConfiguration Build()
        {
            return 
                new Ssl
                {
                    Enabled = _enabled,
                    Certificate = _certificate,
                    Protocols = _protocols == SslProtocols.None ? SslProtocols.Default : _protocols,
                    EncryptionPolicy = _encryptionPolicy,
                    CheckCertificateRevocation = _checkCertificateRevocation,
                    ClientCertificateIsRequired = _clientCertificateIsRequired
                };
        }
    }
}
