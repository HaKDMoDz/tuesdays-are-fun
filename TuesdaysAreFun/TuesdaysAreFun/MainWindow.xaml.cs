using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TuesdaysAreFun
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public CommandViewModel ViewModel;

		public MainWindow()
		{
			InitializeComponent();
			ViewModel = new CommandViewModel(this);
		}

		public void AddText(TextFormat text)
		{
			OutputBox.AppendText(text);
		}
		public void AddTextF(string formatted)
		{
			OutputBox.AppendTextF(formatted);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			CommandViewModel.IsLoaded = true;

			InputBox.Focus();
			AddTextF("&7&/Console loaded.&r\r");
		}

		private void GoBtn_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.HandleInput(InputBox.Text);
			InputBox.Text = "";
			InputBox.Focus();
		}
	}
}
