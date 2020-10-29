
 

namespace Lactalis.Configuration
{
	/// <summary>
	/// Configuration for the S3 storage provider
	/// </summary>
	public class S3StorageProviderConfiguration
	{
		/// <summary>
		/// The S3 access key
		/// </summary>
		public string AccessKey { get; set; }

		/// <summary>
		/// The S3 secret key
		/// </summary>
		public string SecretKey { get; set; }

		/// <summary>
		/// The id of the S3 AWS region to get the files from.
		/// The list of all regions is listed here: https://docs.aws.amazon.com/AmazonRDS/latest/UserGuide/Concepts.RegionsAndAvailabilityZones.html
		/// </summary>
		/// <example>ap-southeast-2</example>
		public string RegionEndpoint { get; set; }

		/// <summary>
		/// The name of the bucket to store files in
		/// </summary>
		public string BucketName { get; set; }

	}

}