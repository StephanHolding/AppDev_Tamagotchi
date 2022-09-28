﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamagotchi.Functionality;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tamagotchi.Service_Locator;

namespace Tamagotchi
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DrinkPage : ContentPage, IShowDialogue
	{
		private Creature creatureInstance;

		public DrinkPage()
		{
			InitializeComponent();

			creatureInstance = ServiceLocator.LocateService<Creature>();

			creatureInstance.OnDialogueUpdated += ShowDialogue;
			creatureInstance.AssignResourceEvent<Resource_Drink>(UpdateThirstMeter);

			ShowDialogue(creatureInstance.CurrentDialogueToSpeak);
			UpdateThirstMeter(creatureInstance.GetResourceValue<Resource_Attention>());
		}

		~DrinkPage()
		{
			creatureInstance.OnDialogueUpdated -= ShowDialogue;
			creatureInstance.RemoveResourceEvent<Resource_Drink>(UpdateThirstMeter);
		}

		private void UpdateThirstMeter(double thirstPercentage)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				ThirstValue.Text = CommonFunctionality.ConvertToPercentageText(thirstPercentage);
			});
		}

		private void GiveDrink(object sender, EventArgs e)
		{
			creatureInstance.AddToResource<Resource_Drink>(CommonFunctionality.RESOURCE_ADD_AMOUNT);
			creatureInstance.Speak("Thank you for giving me something to drink");
		}

		public void ShowDialogue(string message)
		{
			DialogueText.Text = message;
		}

		public void DialogueBoxClicked(object sender, EventArgs e)
		{
			creatureInstance.ContinueDialogue();
		}
	}
}