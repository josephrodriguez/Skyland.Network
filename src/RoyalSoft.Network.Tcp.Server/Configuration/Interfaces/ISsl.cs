﻿#region using

using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Interfaces
{
    public interface ISsl
    {
        bool Enabled { get; }
        SslProtocols Protocols { get; }
        X509Certificate Certificate { get; }
        bool CheckCertificateRevocation { get; }
        bool ClientCertificateIsRequired { get; }
        EncryptionPolicy EncryptionPolicy { get; }
    }
}