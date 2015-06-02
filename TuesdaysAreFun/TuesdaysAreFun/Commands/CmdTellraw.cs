using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace TuesdaysAreFun.Commands
{
	[Command("tellraw")]
	public class CmdTellraw : CommandBase
	{
		public override void Run(params string[] args)
		{
			string json = "";
			for (int i = 1; i < args.Length; i++)
			{
				json += args[i];
			}

			JsonSerializerSettings set = new JsonSerializerSettings();
			set.DefaultValueHandling = DefaultValueHandling.Ignore;
			set.Formatting = Formatting.None;
			set.NullValueHandling = NullValueHandling.Ignore;
			set.StringEscapeHandling = StringEscapeHandling.EscapeHtml;

			TextFormat text = JsonConvert.DeserializeObject<TextFormat>(json, set);
			PrintLn(text);
		}
	}
}
