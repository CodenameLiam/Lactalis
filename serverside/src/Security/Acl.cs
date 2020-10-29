
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lactalis.Models;


namespace Lactalis.Security
{
	/// <summary>
	/// Defines the methods and data that must be provided to an ACL rule for runtime security
	/// </summary>
	public interface IAcl
	{
		string Group { get; }
		bool IsVisitorAcl { get; }

		Expression<Func<TModel, bool>> GetReadConditions<TModel>(User user, SecurityContext context)
			where TModel : IOwnerAbstractModel, new();
		Expression<Func<TModel, bool>> GetUpdateConditions<TModel>(User user, SecurityContext context)
			where TModel : IOwnerAbstractModel, new();
		Expression<Func<TModel, bool>> GetDeleteConditions<TModel>(User user, SecurityContext context)
			where TModel : IOwnerAbstractModel, new();
		bool GetCreate(User user, IEnumerable<IAbstractModel> models, SecurityContext context);
		bool GetUpdate(User user, IEnumerable<IAbstractModel> models, SecurityContext context);
		bool GetDelete(User user, IEnumerable<IAbstractModel> models, SecurityContext context);

	}
}