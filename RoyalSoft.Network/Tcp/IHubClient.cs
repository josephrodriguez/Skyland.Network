#region using

using System.Net;
using System.Net.Security;
using RoyalSoft.Network.Enums;

#endregion

namespace RoyalSoft.Network.Tcp
{
    public interface IHubClient
    {
        #region Events

        event RemoteCertificateValidationCallback OnValidateServerCertificate;

        event LocalCertificateSelectionCallback OnValidateLocalCertificate;

        #endregion

        void EnableSsl();
        void EnableCompression(CompressionMethod method);

        void Send(IPEndPoint endpoint, byte[] message);
        void Send(string host, int port, byte[] message);
    }
}
