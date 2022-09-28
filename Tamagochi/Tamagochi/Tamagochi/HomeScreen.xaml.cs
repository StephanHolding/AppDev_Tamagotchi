using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamagotchi.Service_Locator;
using Tamagotchi.Functionality;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Timers;

namespace Tamagotchi
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeScreen : ContentPage, IShowDialogue
	{

		public Creature creatureInstance;

		public HomeScreen()
		{
			BindingContext = this;

			creatureInstance = ServiceLocator.LocateService<Creature>();

			InitializeComponent();

			creatureInstance.OnDialogueUpdated += ShowDialogue;
			creatureInstance.AssignResourceEvent<Resource_Food>(UpdateHungerMeter);
			creatureInstance.AssignResourceEvent<Resource_Drink>(UpdateThirstMeter);
			creatureInstance.AssignResourceEvent<Resource_Attention>(UpdateAttentionMeter);

			UpdateHungerMeter(creatureInstance.GetResourceValue<Resource_Food>());
			UpdateThirstMeter(creatureInstance.GetResourceValue<Resource_Drink>());
			UpdateAttentionMeter(creatureInstance.GetResourceValue<Resource_Attention>());

			creatureInstance.Speak(new string[]
			{
				"Hello",
				"if you're seeing this...",
				"That means the Service Locator and Observer pattern are working!",
				"Promise..."
			});
		}

		public void DialogueBoxClicked(object sender, EventArgs e)
		{
			creatureInstance.ContinueDialogue();
			HintText.Text = "";
		}

		private void UpdateHungerMeter(int hungerPercentage)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				HungerValue.Text = CommonFunctionality.ConvertToPercentageText(hungerPercentage);
				HungerValue.BackgroundColor = CommonFunctionality.CalculateStatusColor(hungerPercentage, 100, 25);
			});
		}

		private void UpdateThirstMeter(int thirstPercentage)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				ThirstValue.Text = CommonFunctionality.ConvertToPercentageText(thirstPercentage);
				ThirstValue.BackgroundColor = CommonFunctionality.CalculateStatusColor(thirstPercentage, 100, 25);
			});
		}

		private void UpdateAttentionMeter(int attentionPercentage)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				AttentionValue.Text = CommonFunctionality.ConvertToPercentageText(attentionPercentage);
				AttentionValue.BackgroundColor = CommonFunctionality.CalculateStatusColor(attentionPercentage, 50, 15);
			});
		}

		private void GoToHungerPage(object sender, EventArgs e)
		{
			Navigation.PushAsync(new FoodPage());
		}

		private void GoToAttentionPage(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AttentionPage());
		}

		private void GoToThirstPage(object sender, EventArgs e)
		{
			Navigation.PushAsync(new DrinkPage());
		}

		public void ShowDialogue(string message)
		{
			DialogueText.Text = message;
		}
	}
}