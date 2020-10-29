
using System.Collections.Generic;
using APITests.Classes;

namespace APITests.EntityObjects
{
	interface IFileContainingEntity
	{
		IEnumerable<FileData> GetFiles();
	}
}