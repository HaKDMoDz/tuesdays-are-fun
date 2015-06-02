using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TuesdaysAreFun.Tests.Game
{
	public class PartialImageGraphic : IGraphic
	{
		public string Path
		{ get; private set; }

		public BitmapImage RootImage
		{ get; private set; }

		public Int32Rect Region
		{ get; set; }

		public PartialImageGraphic(string path, Int32Rect region)
		{
			Region = region;
		}
		public PartialImageGraphic(string path)
		{
			Path = path;

			RootImage = new BitmapImage();
			RootImage.BeginInit();
			RootImage.UriSource = new Uri("pack://application:,,,/" + path);
			RootImage.CacheOption = BitmapCacheOption.OnLoad;
			RootImage.EndInit();

			Region = new Int32Rect(0, 0, (int)RootImage.Width, (int)RootImage.Height);
		}

		public Image MakeImage()
		{
			CroppedBitmap subImg = new CroppedBitmap(RootImage, Region);

			Image res = new Image();
			res.Source = subImg;

			return res;
		}
	}
}
