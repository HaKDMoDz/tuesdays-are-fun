using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using TuesdaysAreFun.Commands;
using TuesdaysAreFun.Tests;
using TuesdaysAreFun.Tests.Cards;
using TuesdaysAreFun.Tests.Game;

namespace TuesdaysAreFun.Windows
{
	/// <summary>
	/// Interaction logic for CardRenderWindow.xaml
	/// </summary>
	public partial class CardRenderWindow : Window
	{
		DispatcherTimer renderTick;
		Stopwatch stopwatch;

		public CmdCards Command
		{ get; private set; }

		public CardGame Game
		{ get; private set; }

		public CardRenderWindow(CmdCards launcherCommand, CommandViewModel vm)
		{
			Command = launcherCommand;
			Game = new CardGame(this);

			InitializeComponent();

			stopwatch = new Stopwatch();

			renderTick = new DispatcherTimer();
			renderTick.Interval = TimeSpan.FromMilliseconds(10);
			renderTick.Tick += MainLoop;
		}

		public void BeginRender()
		{
			RenderCanvas.Children.Clear();
		}

		public void FinishRender()
		{
			UpdateLayout();
		}

		public void RenderImage(IGraphic img, Vector2 pos)
		{
			Image ui = img.MakeImage();
			RenderCanvas.Children.Add(ui);
			Canvas.SetLeft(ui, pos.X);
			Canvas.SetTop(ui, pos.Y);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Command.PrintLnF("&*&aCard Render Window Loaded.");

			stopwatch.Start();
			renderTick.Start();

			Game.Mouse = new MouseState(Mouse.PrimaryDevice, RenderCanvas);
			Game.Keyboard = new KeyboardState(Keyboard.PrimaryDevice);

			Game.Init();
		}

		private void MainLoop(object sender, EventArgs e)
		{
			TimeSpan dt = stopwatch.Elapsed;
			stopwatch.Restart();

			GetInput();

			Game.Update(dt);

			Game.Render();
		}

		private void GetInput()
		{
			MouseDevice mouse = Mouse.PrimaryDevice;
			KeyboardDevice keyboard = Keyboard.PrimaryDevice;

			Game.Mouse.Update(mouse, RenderCanvas);
			Game.Keyboard.Update(keyboard);

			Game.GetInput();
		}

		public void SendCommand(string cmd)
		{
			if (cmd == "" || cmd == null)
			{
				return;
			}

			Game.SendCommand(cmd.Split(' '));
		}
	}
}
