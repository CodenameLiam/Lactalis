
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lactalis.Security;
using Lactalis.Security.Acl;


namespace APITests.EntityObjects.Models
{
	public class FarmersFarms
	{
		public FarmersFarms() {}

		public Guid Owner { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.FarmerEntity"/>
		public Guid FarmersId { get; set; }
		public FarmerEntity Farmers { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.FarmEntity"/>
		public Guid FarmsId { get; set; }
		public FarmEntity Farms { get; set; }
	}
}