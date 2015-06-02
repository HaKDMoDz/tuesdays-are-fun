using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace TuesdaysAreFun
{
	public static class Util
	{
		static Random Rand = new Random();

		static ConsoleColor color = ConsoleColor.White;
		static bool bold = false;
		static bool italic = false;
		static bool underline = false;

		public const char ESCAPE_CHAR = '&';
		public const char ESCAPE_RESET = 'r';

		public const char ESCAPE_BOLD = '*';
		public const char ESCAPE_ITALIC = '/';
		public const char ESCAPE_UNDERLINE = '_';

		public static readonly char[] ESCAPE_SEQ = new char[] {  
			ESCAPE_RESET, ESCAPE_BOLD, ESCAPE_ITALIC, ESCAPE_UNDERLINE,
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'a', 'A', 'b', 'B', 'c', 'C', 'd', 'D', 'e', 'E', 'f', 'F' };

		public static void AppendText(this RichTextBox box, TextFormat text)
		{
			TextRange tr = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd);
			tr.Text = text.Text;

			if (text.Color != null)
			{
				color = text.Color.Value;
			}

			tr.ApplyPropertyValue(TextElement.ForegroundProperty,
				new SolidColorBrush(color.ToBrushColor()));

			if (text.Bold != null)
			{
				bold = text.Bold.Value;
			}
			tr.ApplyPropertyValue(TextElement.FontWeightProperty, bold ? FontWeights.Bold : FontWeights.Normal);
			
			if (text.Italic != null)
			{
				italic = text.Italic.Value;
			}
			tr.ApplyPropertyValue(TextElement.FontStyleProperty, italic ? FontStyles.Italic : FontStyles.Normal);
			
			if (text.Underline != null)
			{
				underline = text.Underline.Value;
			}
			tr.ApplyPropertyValue(Inline.TextDecorationsProperty, underline ? TextDecorations.Underline : 
				new TextDecorationCollection());

			foreach (TextFormat tf in text.Substrings)
			{
				box.AppendText(tf);
			}
		}
		public static void AppendTextF(this RichTextBox box, string formatted)
		{
			//color = ConsoleColor.White;
			//bold = false;
			//italic = false;
			//underline = false;

			bool escape = false;
			string text = "";
			for (int i = 0; i < formatted.Length; i++)
			{
				char c = formatted[i];
				if (c == ESCAPE_CHAR)
				{
					escape = true;
					continue;
				}

				if (escape)
				{
					escape = false;

					if (c == ESCAPE_CHAR)
					{
						text += c;
						continue;
					}
					
					if (ESCAPE_SEQ.Contains(c))
					{
						box.AppendText(new TextFormat(text, color, bold, italic, underline));
						text = "";
					}

					if (c == ESCAPE_RESET)
					{
						color = ConsoleColor.White;
						bold = false;
						italic = false;
						underline = false;
					}
					else if (c == ESCAPE_BOLD)
					{
						bold = true;
					}
					else if (c == ESCAPE_ITALIC)
					{
						italic = true;
					}
					else if (c == ESCAPE_UNDERLINE)
					{
						underline = true;
					}
					#region colors
					else if (c == '0')
					{
						color = ConsoleColor.Black;
					}
					else if (c == '1')
					{
						color = ConsoleColor.DarkBlue;
					}
					else if (c == '2')
					{
						color = ConsoleColor.DarkGreen;
					}
					else if (c == '3')
					{
						color = ConsoleColor.DarkCyan;
					}
					else if (c == '4')
					{
						color = ConsoleColor.DarkRed;
					}
					else if (c == '5')
					{
						color = ConsoleColor.DarkMagenta;
					}
					else if (c == '6')
					{
						color = ConsoleColor.DarkYellow;
					}
					else if (c == '7')
					{
						color = ConsoleColor.Gray;
					}
					else if (c == '8')
					{
						color = ConsoleColor.DarkGray;
					}
					else if (c == '9')
					{
						color = ConsoleColor.Blue;
					}
					else if (c == 'a' || c == 'A')
					{
						color = ConsoleColor.Green;
					}
					else if (c == 'b' || c == 'B')
					{
						color = ConsoleColor.Cyan;
					}
					else if (c == 'c' || c == 'C')
					{
						color = ConsoleColor.Red;
					}
					else if (c == 'd' || c == 'D')
					{
						color = ConsoleColor.Magenta;
					}
					else if (c == 'e' || c == 'E')
					{
						color = ConsoleColor.Yellow;
					}
					else if (c == 'f' || c == 'F')
					{
						color = ConsoleColor.White;
					}
					#endregion
					else
					{
						text += ESCAPE_CHAR.ToString() + c.ToString();
					}
				}
				else
				{
					text += c;
				}
			}

			box.AppendText(new TextFormat(text, color, bold, italic, underline));
		}

		public static void SetFormat(this TextRange tr, ConsoleColor _color = ConsoleColor.White,
			bool _bold = false, bool _italic = false, bool _underline = false)
		{
			tr.ApplyPropertyValue(TextElement.ForegroundProperty,
				new SolidColorBrush(_color.ToBrushColor()));
			tr.ApplyPropertyValue(TextElement.FontWeightProperty, _bold ? FontWeights.Bold : FontWeights.Normal);
			tr.ApplyPropertyValue(TextElement.FontStyleProperty, _italic ? FontStyles.Italic : FontStyles.Normal);
			tr.ApplyPropertyValue(Inline.TextDecorationsProperty, _underline ? TextDecorations.Underline : 
				new TextDecorationCollection());
		}

		public static string JoinString(this IEnumerable<string> strings, char separator)
		{
			string res = "";
			foreach (string s in strings)
			{
				res += s + separator;
			}
			return res.TrimEnd(separator);
		}

		public static Color ToBrushColor(this ConsoleColor con)
		{
			switch (con)
			{
			case ConsoleColor.Black:
				return Colors.Black;
			case ConsoleColor.Blue:
				return Colors.DodgerBlue;
			case ConsoleColor.Cyan:
				return Colors.Cyan;
			case ConsoleColor.DarkBlue:
				return Colors.DarkBlue;
			case ConsoleColor.DarkCyan:
				return Colors.DarkCyan;
			case ConsoleColor.DarkGray:
				return Colors.DarkGray;
			case ConsoleColor.DarkGreen:
				return Colors.DarkGreen;
			case ConsoleColor.DarkMagenta:
				return Colors.DarkMagenta;
			case ConsoleColor.DarkRed:
				return Colors.DarkRed;
			case ConsoleColor.DarkYellow:
				return Colors.Orange;
			case ConsoleColor.Gray:
				return Colors.Gray;
			case ConsoleColor.Green:
				return Colors.Lime;
			case ConsoleColor.Magenta:
				return Colors.Magenta;
			case ConsoleColor.Red:
				return Colors.Salmon;
			case ConsoleColor.White:
				return Colors.White;
			case ConsoleColor.Yellow:
				return Colors.Yellow;
			default:
				throw new ArgumentOutOfRangeException("Not a valid ConsoleColor");
			}
		}

		public static Type[] GetTypesWithAttribute<T>(Assembly assembly)
			where T : Attribute
		{
			Type attributeType = typeof(T);

			return assembly.GetTypes().ToList().FindAll((t) =>
				Attribute.IsDefined(t, attributeType)).ToArray();
		}

		public static List<T> GetAllPossibleValues<T>()
			where T : struct
		{
			if (!typeof(T).IsEnum)
			{
				throw new ArgumentException("Type argument must be enum. " +
					typeof(T).ToString() + " is not.");
			}

			List<T> res = new List<T>();
			foreach (FieldInfo fi in typeof(T).GetFields())
			{
				object obj = fi.GetValue(null);
				if (obj is T)
				{
					res.Add((T)obj);
				}
			}

			return res;
		}

		public static bool StartsWithParams(this string str, params string[] sequences)
		{
			foreach (string s in sequences)
			{
				if (str.StartsWith(s))
				{
					return true;
				}
			}

			return false;
		}

		public static string AllButFirstCompiled(string[] arr)
		{
			string text = "";
			for (int i = 1; i < arr.Length; i++)
			{
				text += arr[i] + " ";
			}

			text = text.TrimEnd(' ');

			return text;
		}

		public static char RandomLetter(bool includeUppercase)
		{
			int low = (int)'a';
			int high = (int)'z' + 1;

			char res = (char)Rand.Next(low, high);

			if (includeUppercase && Rand.Next(2) == 0)
			{
				string s = res.ToString();
				s = s.ToUpper();
				res = s[0];
			}

			return res;
		}

		public static K GetFirstKeyOf<K, V>(this Dictionary<K, V> dict, V value)
		{
			foreach (KeyValuePair<K, V> kvp in dict)
			{
				if (kvp.Value as object == null)
				{
					continue;
				}

				if (kvp.Value.Equals(value))
				{
					return kvp.Key;
				}
			}

			return default(K);
		}
	}
}
