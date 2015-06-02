using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuesdaysAreFun
{
	/// <summary>
	/// Apply this attribute to register the class as a command
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class CommandAttribute : Attribute
	{
		public string CommandName
		{
			get 
			{ 
				return _name; 
			}
		}
		readonly string _name;

		public CommandAttribute(string name) : base()
		{
			_name = name.ToLower();
		}
	}
}
