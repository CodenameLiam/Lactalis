
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lactalis.Models;
using Microsoft.EntityFrameworkCore;

namespace Lactalis.Utility
{
	public static class ReflectionCache
	{
		public static MethodInfo ILikeMethod = typeof(NpgsqlDbFunctionsExtensions)
			.GetMethod("ILike", new [] {typeof(DbFunctions), typeof(string), typeof(string)});

		private static ConcurrentDictionary<Type, List<PropertyInfo>> FileAttributeCache { get; } =
			new ConcurrentDictionary<Type, List<PropertyInfo>>();

		/// <summary>
		/// Gets all the file attributes for this type. The values for this are cached in a static map for fast
		/// repeated lookups.
		/// </summary>
		/// <param name="entityType">The type to get the file attributes from</param>
		/// <returns>A list of property info representing the file attributes</returns>
		public static List<PropertyInfo> GetFileAttributes(Type entityType)
		{
			if (FileAttributeCache.TryGetValue(entityType, out var properties))
			{
				return properties;
			}

			var attributeInfos = entityType.GetProperties()
				.Where(p => p.GetCustomAttributes<FileReference>().Any())
				.ToList();
			FileAttributeCache.TryAdd(entityType, attributeInfos);

			return attributeInfos;
		}
	}
}
