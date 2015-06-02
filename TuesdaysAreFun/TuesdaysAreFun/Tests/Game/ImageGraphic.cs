using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TuesdaysAreFun.Tests.Game
{
	public class ImageGraphic : IGraphic
	{
		public string Path
		{ get; private set; }

		public Image Rendered
		{ get; private set; }

		public ImageGraphic(string path)
		{
			Path = path;
			Rendered = new Image();
			Rendered.Source = new BitmapImage(new Uri("pack://application:,,,/" + Path));
		}

		public Image MakeImage()
		{
			return Rendered;
		}
	}
}
