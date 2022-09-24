using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamagotchi.Service_Locator;
using Tamagotchi.Functionality;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamagotchi
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeScreen : ContentPage
	{

		private Creature creatureInstance;

		public HomeScreen()
		{
			BindingContext = this;

			InitializeComponent();

			creatureInstance = ServiceLocator.LocateService<Creature>();
			creatureInstance.OnDialogueUpdated += UpdateDialogueUIText;
			creatureInstance.AssignResourceEvent<Resource_Food>(UpdateHungerMeter);
			//creatureInstance.AssignResourceEvent<Resource_Drink>(UpdateThirstMeter);
			//creatureInstance.AssignResourceEvent<Resource_Attention>(UpdateAttentionMeter);

			creatureInstance.Speak(new string[] 
			{
				"Hello",
				"if you're seeing this...",
				"That means the Service Locator and Observer pattern are working!",
				"Promise..." 
			});
		}

		private void DialogueBoxClicked(object sender, EventArgs e)
		{
			creatureInstance.ContinueDialogue();
			HintText.Text = "";
		}

		private void UpdateDialogueUIText()
		{
			string dialogueMessage = creatureInstance.GetCurrentDisplayingMessage();
			DialogueText.Text = dialogueMessage;
		}

		private void UpdateHungerMeter(float hungerPercentage)
		{
			string percentage = (hungerPercentage * 100).ToString() + "%";
			Console.WriteLine("HUNGER UPDATED: " + percentage);
			HungerValue.Text = percentage;
		}

		private void UpdateThirstMeter(float thirstPercentage)
		{

		}

		private void UpdateAttentionMeter(float attentionPercentage)
		{

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
	}
}