using System.Net.Security;

namespace RoyalSoft.Network.Tcp.Server.Configuration.Interfaces
{
    public interface IEvents
    {
        /// <summary>
        /// 
        /// </summary>
        event LocalCertificateSelectionCallback OnLocalCertificateSelection;


        /// <summary>
        /// 
        /// </summary>
        event RemoteCertificateValidationCallback OnRemoteCertificateValidation;
    }
}
