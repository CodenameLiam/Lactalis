
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Lactalis.Helpers
{
    public class GraphQLHelper
    {
		public static List<IDictionary<string, Object>> getDataFromGraphQLResult(object graphQLData, List<string> columns = null)
		{
			var resultList = new List<IDictionary<string, Object>>();
			var graphData = (graphQLData as Dictionary<string, object>).First();
			if (graphData.Equals(new KeyValuePair<string, object>()))
			{
				return null;
			}

            var entities = graphData.Value;
			foreach (var e in entities as List<object>)
			{
				var destEntity = new ExpandoObject() as IDictionary<string, Object>;
				var eDic = (e as Dictionary<string, object>);

				foreach(var field in eDic)
				{
					object oValue;
					string strValue;

					if (columns.Count > 0 && columns.Contains(field.Key))
					{
						eDic.TryGetValue(field.Key, out oValue);
						strValue = oValue == null ? "" : oValue.ToString();
						destEntity.Add(field.Key, strValue);
					}
				}
				resultList.Add(destEntity);
			}

			return resultList;
		}



	}

}
