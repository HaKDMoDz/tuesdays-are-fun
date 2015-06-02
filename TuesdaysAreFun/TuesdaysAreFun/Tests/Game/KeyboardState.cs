using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TuesdaysAreFun.Tests.Game
{
	[Obsolete("Malfunctioning. Does not receive input.")]
	public class KeyboardState
	{
		public Dictionary<Key, KeyStates> PrevKeyStates
		{ get; private set; }

		public Dictionary<Key, KeyStates> CurrentKeyStates
		{ get; private set; }

		public KeyboardState(KeyboardDevice device)
		{
			CurrentKeyStates = new Dictionary<Key, KeyStates>();
			foreach (Key k in Enum.GetValues(typeof(Key)))
			{
				if (CurrentKeyStates.ContainsKey(k))
				{
					continue;
				}

				CurrentKeyStates.Add(k, KeyStates.None);
			}

			PrevKeyStates = new Dictionary<Key, KeyStates>();
			foreach (KeyValuePair<Key, KeyStates> kvp in CurrentKeyStates)
			{
				PrevKeyStates.Add(kvp.Key, kvp.Value);
			}

			Update(device);
		}

		public void Update(KeyboardDevice device)
		{
			foreach (KeyValuePair<Key, KeyStates> kvp in CurrentKeyStates)
			{
				PrevKeyStates[kvp.Key] = kvp.Value;
			}

			foreach (Key k in Enum.GetValues(typeof(Key)))
			{
				if (k == Key.None)
				{
					continue;
				}

				CurrentKeyStates[k] = device.GetKeyStates(k);
			}
		}

		public bool IsKeyDown(Key key)
		{
			if (key == Key.None)
			{
				return false;
			}

			return CurrentKeyStates[key] != KeyStates.None;
		}
		public bool WasKeyDown(Key key)
		{
			if (key == Key.None)
			{
				return false;
			}

			return PrevKeyStates[key] != KeyStates.None;
		}
		public bool IsKeyJustDown(Key key)
		{
			if (key == Key.None)
			{
				return false;
			}

			return IsKeyDown(key) && !WasKeyDown(key);
		}

		public bool AreKeysDown(IEnumerable<Key> keys)
		{
			foreach (Key k in keys)
			{
				if (k == Key.None)
				{
					continue;
				}

				if (!IsKeyDown(k))
				{
					return false;
				}
			}

			return true;
		}
		public bool AreKeysDown(params Key[] keys)
		{
			return AreKeysDown(keys);
		}
		public bool AreKeysDown(Array arr)
		{
			return AreKeysDown(arr.OfType<Key>());
		}
	}
}
