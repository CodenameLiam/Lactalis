
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;



namespace Lactalis.Services.CertificateProvider
{
	public class CertificateSetting
	{
		/// <summary>
		/// The name of the certificate file
		/// </summary>
		public string CertFileName { get; set; }
		/// <summary>
		/// The password for the private key of the certificate file
		/// </summary>
		public string PrivateKeyPWD { get; set; }
		/// <summary>
		/// The JwtBearer Authority
		/// </summary>
		public string JwtBearerAuthority { get; set; }
		/// <summary>
		/// The JwtBearer Audience
		/// </summary>
		public string JwtBearerAudience { get; set; }

	}

	public abstract class BaseCertificateProvider: ICertificateProvider
	{

		public BaseCertificateProvider(CertificateSetting certSetting)
		{
			CertificateSetting = certSetting;
		}

		ed CertificateSetting CertificateSetting { get; }

		public abstract X509Certificate2 ReadX509SigningCert();

	}
}
