
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Lactalis.Services.CertificateProvider
{
    interface ICertificateProvider
    {
        X509Certificate2 ReadX509SigningCert();
    }
}
