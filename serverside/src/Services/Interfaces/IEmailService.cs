

using System.Threading.Tasks;

namespace Lactalis.Services.Interfaces
{
	public interface IEmailService
	{
		/// <summary>
		/// Sending an email
		/// </summary>
		/// <param name="emailToSend">The parameters object for calling SendEmail function</param>
		/// <returns>an sending email result true, or false</returns>
		Task<bool> SendEmail(EmailEntity emailToSend);
	}
}