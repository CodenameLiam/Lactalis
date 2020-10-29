
 

namespace Lactalis.Configuration
{
	public enum StorageProviders
	{
		FILE_SYSTEM,
		S3,
	}

	/// <summary>
	/// Configuration for the file storage
	/// </summary>
	public class StorageProviderConfiguration
	{
		/// <summary>
		/// The provider to use for file storage
		/// </summary>
		public StorageProviders Provider { get; set; } = StorageProviders.FILE_SYSTEM;

	}

}