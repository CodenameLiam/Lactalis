
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lactalis.Models;


namespace Lactalis.Security.Acl
{
	public class FarmerTechnicalDocumentCategoryEntity : IAcl
	{
		public string Group => "Farmer";
		public bool IsVisitorAcl => false;

		public bool GetCreate(User user, IEnumerable<IAbstractModel> models, SecurityContext context)
		{
			
			return false;
			
		}

		public Expression<Func<TModel, bool>> GetReadConditions<TModel>(User user, SecurityContext context)
			where TModel : IOwnerAbstractModel, new()
		{
	
			return model => true;
	
		}

		public Expression<Func<TModel, bool>> GetUpdateConditions<TModel>(User user, SecurityContext context)
			where TModel : IOwnerAbstractModel, new()
		{
			return model => false;
		}

		public Expression<Func<TModel, bool>> GetDeleteConditions<TModel>(User user, SecurityContext context)
			where TModel : IOwnerAbstractModel, new()
		{
			return model => false;
		}

		public bool GetUpdate(User user, IEnumerable<IAbstractModel> models, SecurityContext context)
		{
			
			return false;
			
		}

		public bool GetDelete(User user, IEnumerable<IAbstractModel> models, SecurityContext context)
		{
			
			return false;
			
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			// Acls have no setable properties so if they are the same type then they will be the same
			return GetType() == obj.GetType();
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode();
		}

	}
}