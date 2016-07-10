using System.Net.Security;

namespace RoyalSoft.Network.Tcp.Client.Configuration
{
    public interface IHubClientEvents
    {
        /// <summary>
        /// When SSL is enabled this event will be raised to validate remote certificate
        /// </summary>
        event RemoteCertificateValidationCallback OnRemoteCertificateValidation;

        /// <summary>
        /// When SSL is enabled this event will be raised to validate local certificate
        /// </summary>
        event LocalCertificateSelectionCallback OnLocalCertificateValidation;
    }
}
