
using System.IO;
 

namespace Lactalis.Configuration
{
	/// <summary>
	/// Configuration for the file system storage provider
	/// </summary>
	public class FileSystemStorageProviderConfiguration
	{
		/// <summary>
		/// The root folder to store files in
		/// </summary>
		public string RootFolder { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "___data");
		
	}

}