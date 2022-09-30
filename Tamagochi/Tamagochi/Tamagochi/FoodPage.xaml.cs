using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamagotchi.Functionality;
using Tamagotchi.Service_Locator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamagotchi
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoodPage : ContentPage, IShowDialogue
	{

		private Creature creatureInstance;

		public FoodPage()
		{
			InitializeComponent();

			creatureInstance = ServiceLocator.LocateService<Creature>();

			creatureInstance.OnDialogueUpdated += ShowDialogue;
			creatureInstance.AssignResourceEvent<Resource_Food>(UpdateFoodMeter);

			ShowDialogue(creatureInstance.CurrentDialogueToSpeak);
			UpdateFoodMeter(creatureInstance.GetResourceValue<Resource_Food>());
		}

		~FoodPage()
		{
			creatureInstance.OnDialogueUpdated -= ShowDialogue;
			creatureInstance.RemoveResourceEvent<Resource_Food>(UpdateFoodMeter);
		}

		private void UpdateFoodMeter(int hungerPercentage)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				HungerValue.Text = CommonFunctionality.ConvertToPercentageText(hungerPercentage);
				HungerValue.BackgroundColor = CommonFunctionality.CalculateStatusColor(hungerPercentage, 100, 25);
			});
		}

		private void Feed(object sender, EventArgs e)
		{
			creatureInstance.AddToResource<Resource_Food>(CommonFunctionality.RESOURCE_ADD_AMOUNT);
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