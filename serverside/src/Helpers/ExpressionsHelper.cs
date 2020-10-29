
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lactalis.Services;

namespace Lactalis.Helpers
{
	public static class ExpressionHelper
	{
		/// <summary>
		/// Aggregates a list of expressions using an OR conjunction
		/// </summary>
		/// <param name="expressions"> The expressions to aggregate </param>
		/// <typeparam name="TModel"> Model returned by expressions </typeparam>
		/// <returns> An expression that can be used for the where condition of a linq query </returns>
		public static Expression<Func<TModel, bool>> OrExpressions<TModel>(
			IEnumerable<Expression<Func<TModel, bool>>> expressions)
		{
			Expression<Func<TModel, bool>> baseRule = _ => false;
			var filter = Expression.OrElse(baseRule.Body, baseRule.Body);

			filter = expressions.Aggregate(filter, (current, expression) =>
				Expression.OrElse(current, expression.Body));

			var param = Expression.Parameter(typeof(TModel), "model");
			var replacer = new ParameterReplacer(param);

			return Expression.Lambda<Func<TModel, bool>>(replacer.Visit(filter), param);
		}

		/// <summary>
		/// Aggregates a list of expressions using an AND conjunction
		/// </summary>
		/// <param name="expressions"> The expressions to aggregate </param>
		/// <typeparam name="TModel"> Model returned by expressions </typeparam>
		/// <returns> An expression that can be used for the where condition of a linq query </returns>
		public static Expression<Func<TModel, bool>> AndExpressions<TModel>(
			IEnumerable<Expression<Func<TModel, bool>>> expressions)
		{
			Expression<Func<TModel, bool>> baseRule = _ => true;
			var filter = Expression.AndAlso(baseRule.Body, baseRule.Body);

			filter = expressions.Aggregate(filter, (current, expression) =>
				Expression.AndAlso(current, expression.Body));

			var param = Expression.Parameter(typeof(TModel), "model");
			var replacer = new ParameterReplacer(param);

			return Expression.Lambda<Func<TModel, bool>>(replacer.Visit(filter), param);
		}
	}
}