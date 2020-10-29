

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lactalis.Security;
using Lactalis.Security.Acl;
using ServersideFarmersFarms = Lactalis.Models.FarmersFarms;

namespace APITests.EntityObjects.Models
{
	public class FarmersFarmsDto
	{
		//public Guid Owner { get; set; }
		public Guid FarmersId { get; set; }
		public Guid FarmsId { get; set; }

		public FarmersFarmsDto(FarmersFarms model)
		{
			//Owner = model.Owner;
			FarmersId = model.FarmersId;
			FarmsId = model.FarmsId;
		}

		public FarmersFarmsDto(ServersideFarmersFarms model)
		{
			//Owner = model.Owner;
			FarmersId = model.FarmersId;
			FarmsId = model.FarmsId;
		}

		public ServersideFarmersFarms GetServersideFarmersFarms ()
		{
			return new ServersideFarmersFarms()
			{
				//Owner = Owner,
				FarmersId = FarmersId,
				FarmsId = FarmsId,
			};
		}

		public FarmersFarms GetTesttargetFarmersFarms ()
		{
			return new FarmersFarms()
			{
				//Owner = Owner,
				FarmersId = FarmersId,
				FarmsId = FarmsId,
			};
		}

		public static ICollection<ServersideFarmersFarms> Convert(ICollection<FarmersFarms> collection)
		{
			var newCollection = new List<ServersideFarmersFarms>();


			foreach (var item in collection)
			{
				newCollection.Add(new FarmersFarmsDto(item).GetServersideFarmersFarms());
			}
			return newCollection;
		}

		public static ICollection<FarmersFarms> Convert(ICollection<ServersideFarmersFarms> collection)
		{
			var newCollection = new List<FarmersFarms>();

			foreach (var item in collection)
			{
				newCollection.Add(new FarmersFarmsDto(item).GetTesttargetFarmersFarms());
			}
			return newCollection;
		}
	}
}