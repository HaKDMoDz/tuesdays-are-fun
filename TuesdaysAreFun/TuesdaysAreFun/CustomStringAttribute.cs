using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TuesdaysAreFun
{
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class CustomStringAttribute : Attribute
	{
		public string CustomString
		{ get; set; }
		
		public CustomStringAttribute(string customString)
		{
			CustomString = customString;
		}
	}

	public static class EnumExtensions
	{
		public static string ToCustomString<T>(this T obj)
			where T : struct
		{
			Type type = obj.GetType();

			if (!type.IsEnum)
			{
				return obj.ToString();
			}

			//Tries to find a DescriptionAttribute for a potential friendly name
			//for the enum
			MemberInfo[] memberInfo = type.GetMember(obj.ToString());
			if (memberInfo != null && memberInfo.Length > 0)
			{
				object[] attrs = memberInfo[0].GetCustomAttributes(typeof(CustomStringAttribute), false);

				if (attrs != null && attrs.Length > 0)
				{
					//Pull out the description value
					return ((CustomStringAttribute)attrs[0]).CustomString;
				}
			}
			//If we have no description attribute, just return the ToString of the enum
			return obj.ToString();
		}
	}
}
