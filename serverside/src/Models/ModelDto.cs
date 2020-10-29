
using System;

namespace Lactalis.Models
{
	/// <summary>
	/// A DTO for outputting data to the API
	/// </summary>
	public abstract class ModelDto<T>
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		public abstract T ToModel();
		public abstract ModelDto<T> LoadModelData(T model);
	}
}