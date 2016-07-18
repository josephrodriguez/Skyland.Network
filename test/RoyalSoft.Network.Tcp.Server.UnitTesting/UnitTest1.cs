#region using

using System;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoyalSoft.Network.Core.Compression;
using RoyalSoft.Network.Core.Thread;

#endregion

namespace RoyalSoft.Network.Tcp.Server.UnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var hub = Configuration.Configure
                .WithEndpoint(IPAddress.Parse("127.0.0.1"), 5770)
                .Compression(
                    c =>
                        c.UseMethod(CompressionMethod.Gzip))
                .Ssl(
                    s =>
                        s
                            .Certificate(new X509Certificate())
                            .CheckCertificateRevocation()
                            .ClientCertificateRequired()
                            .Policy(EncryptionPolicy.RequireEncryption))
                .Options(
                    o =>
                        o.AllowedConnections(2))
                .Create();

            hub.Start();
            Task.Delay(60000).Wait();
            hub.Stop();
        }

        //[TestMethod]
        //public void Worker()
        //{
        //    var worker = new Worker(Method);
        //    worker.Start();
        //    worker.Cancel();
        //}
    }
}
