
using System.IO;
using Lactalis.Models;
using Microsoft.AspNetCore.Http;


namespace Lactalis.Services.Files
{
	/// <summary>
	/// The options for the GetAsync storage provider function
	/// </summary>
	public class StorageGetOptions
	{
		/// <summary>
		/// The container the file is in
		/// </summary>
		public string Container { get; set; }

		/// <summary>
		/// The name of the file
		/// </summary>
		public string FileName { get; set; }

	}

	/// <summary>
	/// The options for the ListAsync storage provider function
	/// </summary>
	public class StorageListOptions
	{
		/// <summary>
		/// The container to list the files in
		/// </summary>
		public string Container { get; set; }

	}

	/// <summary>
	/// The options for the ExistsAsync storage provider function
	/// </summary>
	public class StorageExistsOptions
	{
		/// <summary>
		/// The container the file is in
		/// </summary>
		public string Container { get; set; }

		/// <summary>
		/// The name of the file
		/// </summary>
		public string FileName { get; set; }

	}

	/// <summary>
	/// The options for the PutAsync storage provider function
	/// </summary>
	public class StoragePutOptions
	{
		/// <summary>
		/// The container to save the file to
		/// </summary>
		public string Container { get; set; }

		/// <summary>
		/// The name to save the file as
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// The stream of data to save
		/// </summary>
		public Stream Content { get; set; }

		/// <summary>
		/// Optional. If the file already exists should it be overwritten
		/// </summary>
		public bool Overwrite { get; set; } = true;

		/// <summary>
		/// Optional. The content type of the file
		/// </summary>
		public string ContentType { get; set; } = null;

		/// <summary>
		/// If putting the object into a container that does not exist should it be created automatically
		/// </summary>
		/// <remarks>
		/// This option only has meaning when the underlying storage provider implements physical containers. In the
		/// case of a key value store provider with only namespaced containers the container might be 'created'
		/// regardless of this option.
		/// </remarks>
		public bool CreateContainerIfNotExists { get; set; } = true;

	}

	/// <summary>
	/// The options for the DeleteAsync storage provider function
	/// </summary>
	public class StorageDeleteOptions
	{
		/// <summary>
		/// The container the file is in
		/// </summary>
		public string Container { get; set; }

		/// <summary>
		/// The name of the file
		/// </summary>
		public string FileName { get; set; }

	}

	/// <summary>
	/// The options for the ContainerExistsAsync storage provider function
	/// </summary>
	public class StorageContainerExistsOptions
	{
		/// <summary>
		/// The name of the container to check
		/// </summary>
		public string Container { get; set; }

	}

	/// <summary>
	/// The options for the CreateContainerAsync storage provider function
	/// </summary>
	public class StorageCreateContainerOptions
	{
		/// <summary>
		/// The container to create
		/// </summary>
		public string Container { get; set; }

	}

	/// <summary>
	/// The options for the DeleteContainerAsync storage provider function
	/// </summary>
	public class StorageDeleteContainerOptions
	{
		/// <summary>
		/// The name of the container to delete
		/// </summary>
		public string Container { get; set; }

	}

	/// <summary>
	/// The options for the OnFetch storage provider function
	/// </summary>
	public class StorageOnFetchOptions
	{
		/// <summary>
		/// The file to fetch
		/// </summary>
		public UploadFile File { get; set; }

		/// <summary>
		/// Should the file have download headers
		/// </summary>
		public bool Download { get; set; }

		/// <summary>
		/// The HttpContext of the request
		/// </summary>
		public HttpContext HttpContext { get; set; }

	}

}