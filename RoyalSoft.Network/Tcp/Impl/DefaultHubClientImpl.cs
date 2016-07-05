#region using

using System;
using System.Net;
using System.Net.Security;
using RoyalSoft.Network.Enums;

#endregion

namespace RoyalSoft.Network.Tcp.Impl
{
    internal class DeafultHubClientImpl : IHubClient
    {
        public event RemoteCertificateValidationCallback OnValidateServerCertificate;
        public event LocalCertificateSelectionCallback OnValidateLocalCertificate;

        public void EnableSsl()
        {
            throw new NotImplementedException();
        }

        public void EnableCompression(CompressionMethod method)
        {
            throw new NotImplementedException();
        }

        public void Send(IPEndPoint endpoint, byte[] message)
        {
            throw new NotImplementedException();
        }

        public void Send(string host, int port, byte[] message)
        {
            throw new NotImplementedException();
        }
    }
}
