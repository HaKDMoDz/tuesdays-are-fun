using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TuesdaysAreFun.Tests.Cards;
using TuesdaysAreFun.Windows;

namespace TuesdaysAreFun.Commands
{
	[Command("cards")]
	public sealed class CmdCards : CommandInputable
	{
		Random rand = new Random();

		public const int TRIALS = 500;

		public CardPile deck = CardPile.FullDeck;

		public CardRenderWindow RenderWindow
		{ get; private set; }

		public override void Run(params string[] args)
		{
			GrabInput();

			RenderWindow = ViewModel.LaunchCardWindow(this);
		}

		private void PairFinding()
		{
			deck = deck.Shuffle();

			int found = 0;
			for (int trial = 0; trial < TRIALS; trial++)
			{
				Card a = rand.NextCard();
				Card b = rand.NextCard();

				List<Card> list = deck.ToList();

				int id = -1;
				bool reversed = false;
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] == a)
					{
						if (i > 0 && list[i - 1].Rank == b.Rank)
						{
							id = i - 1;
							reversed = true;
							break;
						}
						if (i < list.Count - 1 && list[i + 1].Rank == b.Rank)
						{
							id = i;
							break;
						}
					}
				}

				if (id != -1)
				{
					found++;
					PrintLnF(string.Format("[TRIAL {0:0000}]: ", trial) +
						"&a&*Sequence " + a.Rank.ToCustomString() + " && " +
						b.Rank.ToCustomString() + " found at: &r" + id.ToString() +
						" &9   \t" + (reversed ? b.ToCustomString() + ", " + a.ToCustomString() :
						a.ToCustomString() + ", " + b.ToCustomString()) + "&r");
				}
				else
				{
					PrintLnF(string.Format("[TRIAL {0:0000}]: ", trial) +
						"&c&*Sequence " + a.Rank.ToCustomString() + " && " +
						b.Rank.ToCustomString() + " not found.&r");
				}
			}

			PrintLnF("\r&6Results:");
			double pHat = (double)found / (double)TRIALS;
			PrintLnF("    &5p^ = " + pHat.ToString());
			double sigma = Math.Sqrt((pHat * (1 - pHat)) / (double)TRIALS);
			PrintLnF("    &eo` = " + sigma.ToString());

			double lower = pHat - (1.96 * sigma);
			double upper = pHat + (1.96 * sigma);
			PrintLnF("    &995% Confidence in [" + lower.ToString() + ", " + upper.ToString() + "]");
		}

		public override void ReroutedInput(string cmd)
		{
			RenderWindow.SendCommand(cmd);
		}
	}
}
