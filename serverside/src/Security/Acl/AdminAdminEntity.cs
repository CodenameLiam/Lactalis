
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lactalis.Models;


namespace Lactalis.Security.Acl
{
	public class AdminAdminEntity : IAcl
	{
		public string Group => "Admin";
		public bool IsVisitorAcl => false;

		public bool GetCreate(User user, IEnumerable<IAbstractModel> models, SecurityContext context)
		{
			
			return true;
			
		}

		public Expression<Func<TModel, bool>> GetReadConditions<TModel>(User user, SecurityContext context)
			where TModel : IOwnerAbstractModel, new()
		{
	
			return model => true;
	
		}

		public Expression<Func<TModel, bool>> GetUpdateConditions<TModel>(User user, SecurityContext context)
			where TModel : IOwnerAbstractModel, new()
		{
			return model => true;
		}

		public Expression<Func<TModel, bool>> GetDeleteConditions<TModel>(User user, SecurityContext context)
			where TModel : IOwnerAbstractModel, new()
		{
			return model => true;
		}

		public bool GetUpdate(User user, IEnumerable<IAbstractModel> models, SecurityContext context)
		{
			
			return true;
			
		}

		public bool GetDelete(User user, IEnumerable<IAbstractModel> models, SecurityContext context)
		{
			
			return true;
			
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