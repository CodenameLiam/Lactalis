
using System;

namespace APITests.Classes
{
	public class FileData
	{
		public Guid Id { get; set; }
		public byte[] Data { get; set; }
		public string Filename { get; set; }
	}
}