using System;
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
			UpdateThirstMeter(creatureInstance.GetResourceValue<Resource_Drink>());
		}

		~DrinkPage()
		{
			creatureInstance.OnDialogueUpdated -= ShowDialogue;
			creatureInstance.RemoveResourceEvent<Resource_Drink>(UpdateThirstMeter);
		}

		private void UpdateThirstMeter(int thirstPercentage)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				ThirstValue.Text = CommonFunctionality.ConvertToPercentageText(thirstPercentage);
				ThirstValue.BackgroundColor = CommonFunctionality.CalculateStatusColor(thirstPercentage, 100, 25);
			});
		}

		private async void GiveDrink(object sender, EventArgs e)
		{
			creatureInstance.AddToResource<Resource_Drink>(CommonFunctionality.RESOURCE_ADD_AMOUNT);

			await Image.TranslateTo(0, -30, 250, Easing.SinIn);
			await Image.TranslateTo(0, 0, 250, Easing.SinIn);
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