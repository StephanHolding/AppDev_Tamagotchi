using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamagochi.Service_Locator;
using Tamagochi.Functionality;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamagochi
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeScreen : ContentPage
	{

		Creature creatureInstance;

		public HomeScreen()
		{
			BindingContext = this;

			InitializeComponent();

			creatureInstance = ServiceLocator.LocateService<Creature>();
			creatureInstance.onDialogueUpdated += UpdateDialogueText;
			
			creatureInstance.Speak(new string[] 
			{
				"Hello",
				"if you're seeing this...",
				"That means the Service Locator and Observer pattern are working!",
				"Promise..." 
			});
		}


		private void Button_Clicked(object sender, EventArgs e)
		{
			//creatureInstance.Speak(textEntry.Text);
		}

		private void DialogueBoxClicked(object sender, EventArgs e)
		{
			creatureInstance.ContinueDialogue();
			HintText.Text = "";
		}

		private void UpdateDialogueText()
		{
			string dialogueMessage = creatureInstance.GetCurrentDisplayingMessage();

			if (string.IsNullOrEmpty(dialogueMessage))
			{
				DialogueText.IsVisible = false;
			}
			else
			{
				DialogueText.Text = dialogueMessage;
				DialogueText.IsVisible = true;
			}
		}

		private void GoToHungerPage(object sender, EventArgs e)
		{
			//goto
		}

		private void GoToAttentionPage(object sender, EventArgs e)
		{
			//goto
		}

		private void GoToThirstPage(object sender, EventArgs e)
		{
			//goto
		}
	}
}