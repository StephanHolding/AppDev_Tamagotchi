using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagotchi.Functionality
{
	internal interface IShowDialogue
	{
		void ShowDialogue(string message);
		void DialogueBoxClicked(object sender, EventArgs e);
	}
}
