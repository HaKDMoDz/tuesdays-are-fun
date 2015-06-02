using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TuesdaysAreFun.Tests.Game
{
	public class MouseState
	{
		public Vector2 Position
		{ get; private set; }

		public Dictionary<MouseButton, MouseButtonState> PrevButtonStates
		{ get; private set; }

		public Dictionary<MouseButton, MouseButtonState> ButtonStates
		{ get; private set; }

		public MouseState(MouseDevice device, IInputElement relativeTo)
		{
			ButtonStates = new Dictionary<MouseButton, MouseButtonState>();
			ButtonStates.Add(MouseButton.Left, MouseButtonState.Released);
			ButtonStates.Add(MouseButton.Right, MouseButtonState.Released);
			ButtonStates.Add(MouseButton.Middle, MouseButtonState.Released);
			ButtonStates.Add(MouseButton.XButton1, MouseButtonState.Released);
			ButtonStates.Add(MouseButton.XButton2, MouseButtonState.Released);

			PrevButtonStates = new Dictionary<MouseButton, MouseButtonState>();
			foreach (KeyValuePair<MouseButton, MouseButtonState> kvp in ButtonStates)
			{
				PrevButtonStates.Add(kvp.Key, kvp.Value);
			}

			Update(device, relativeTo);
		}

		public void Update(MouseDevice device, IInputElement relativeTo)
		{
			Position = new Vector2(device.GetPosition(relativeTo));

			foreach (KeyValuePair<MouseButton, MouseButtonState> kvp in ButtonStates)
			{
				PrevButtonStates[kvp.Key] = kvp.Value;
			}

			ButtonStates[MouseButton.Left] = device.LeftButton;
			ButtonStates[MouseButton.Right] = device.RightButton;
			ButtonStates[MouseButton.Middle] = device.MiddleButton;
			ButtonStates[MouseButton.XButton1] = device.XButton1;
			ButtonStates[MouseButton.XButton2] = device.XButton2;
		}

		public bool IsButtonDown(MouseButton button)
		{
			return ButtonStates[button] == MouseButtonState.Pressed;
		}
		public bool WasButtonDown(MouseButton button)
		{
			return PrevButtonStates[button] == MouseButtonState.Pressed;
		}
		public bool IsButtonJustDown(MouseButton button)
		{
			return IsButtonDown(button) && !WasButtonDown(button);
		}

		// shortcuts for left-click

		public bool IsClicking()
		{
			return IsButtonDown(MouseButton.Left);
		}
		public bool WasClicking()
		{
			return WasButtonDown(MouseButton.Left);
		}
		public bool IsClickingStart()
		{
			return IsButtonJustDown(MouseButton.Left);
		}
	}
}
