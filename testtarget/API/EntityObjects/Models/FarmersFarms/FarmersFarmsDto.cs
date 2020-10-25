/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */

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