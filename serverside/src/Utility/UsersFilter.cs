
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lactalis.Helpers;
using Lactalis.Models;
using Lactalis.Services;

namespace Lactalis.Utility
{
	public static class UsersFilter
	{
		/// <summary>
		/// A security filter that can be used over a list of all user entities
		/// </summary>
		/// <param name="user"> The user attempting to perform the operation on all users </param>
		/// <param name="groups"> The groups the user belongs to </param>
		/// <param name="operation"> The database operation the user is trying to perform  </param>
		/// <param name="serviceProvider">Service provider to pass to the ACLs</param>
		/// <returns> An expression that can be used for the where condition of a linq query </returns>
		public static Expression<Func<User, bool>> AllUsersFilter(
			User user, 
			IList<string> groups, 
			DATABASE_OPERATION operation,
			IServiceProvider serviceProvider)
		{
			return ExpressionHelper.OrExpressions(
				new List<Expression<Func<User, bool>>>
				{
					UserFilter<AdminEntity>(user, groups, "AdminEntity", operation, serviceProvider),
					UserFilter<FarmerEntity>(user, groups, "FarmerEntity", operation, serviceProvider),
				}
			);
		}

		/// <summary>
		/// A security filter for a single user entity
		/// </summary>
		/// <param name="user"> The user attempting to perform the operation </param>
		/// <param name="groups"> The groups the user belongs to </param>
		/// <param name="discriminator"> The user model discriminator </param>
		/// <param name="operation"> The database operation the user is trying to perform  </param>
		/// <param name="serviceProvider">Service provider to pass to the ACLs</param>
		/// <typeparam name="TModel"> The user model trying to be accessed </typeparam>
		/// <returns> An expression that can be used for the where condition of a linq query </returns>
		private static Expression<Func<User, bool>> UserFilter<TModel>(
			User user, 
			IList<string> groups,
			string discriminator,
			DATABASE_OPERATION operation,
			IServiceProvider serviceProvider)
			where TModel : class, IOwnerAbstractModel, new()
		{
			return ExpressionHelper.AndExpressions(
				new List<Expression<Func<User, bool>>>
				{
					SecurityService.GetAggregatedUserModelAcls<TModel>(user, groups, operation, serviceProvider),
					u => u.Discriminator == discriminator
				}
			);
		}
	}
}