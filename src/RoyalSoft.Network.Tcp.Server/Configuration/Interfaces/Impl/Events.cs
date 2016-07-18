#region using

using System.Net.Security;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Interfaces.Impl
{
    internal class Events : IEvents
    {
        public event LocalCertificateSelectionCallback OnLocalCertificateSelection;
        public event RemoteCertificateValidationCallback OnRemoteCertificateValidation;
    }
}
