using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuesdaysAreFun
{
	public class TextFormat
	{
		public string Text
		{ get; set; }

		public ConsoleColor? Color
		{ get; set; }

		public bool? Bold
		{ get; set; }

		public bool? Italic
		{ get; set; }

		public bool? Underline
		{ get; set; }

		public List<TextFormat> Substrings
		{ get; private set; }

		public TextFormat(string text, ConsoleColor? color = null, 
			bool? bold = null, bool? italic = null, bool? underline = null)
		{
			Text = text;
			Color = color;
			Bold = bold;
			Italic = italic;
			Underline = underline;
			Substrings = new List<TextFormat>();
		}

		public void AddSubstring(TextFormat substring)
		{
			Substrings.Add(substring);
		}

		public static TextFormat Orange()
		{
			TextFormat res = new TextFormat("Only one word here is ");
			res.Italic = true;
			TextFormat or = new TextFormat("orange");
			or.Italic = false;
			or.Bold = true;
			or.Color = ConsoleColor.DarkYellow;
			res.AddSubstring(or);
			TextFormat col = new TextFormat("-colored.");
			col.Italic = true;
			col.Bold = false;
			col.Color = ConsoleColor.White;
			res.AddSubstring(col);
			return res;
		}
	}
}
