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
	public partial class SleepPage : ContentPage, IShowDialogue
	{
		private Creature creatureInstance;

		public SleepPage()
		{
			InitializeComponent();

			creatureInstance = ServiceLocator.LocateService<Creature>();

			creatureInstance.OnDialogueUpdated += ShowDialogue;
			creatureInstance.AssignResourceEvent<Resource_Sleep>(UpdateSleepMeter);

			ShowDialogue(creatureInstance.CurrentDialogueToSpeak);
			UpdateSleepMeter(creatureInstance.GetResourceValue<Resource_Sleep>());
		}

		~SleepPage()
		{
			creatureInstance.OnDialogueUpdated -= ShowDialogue;
			creatureInstance.RemoveResourceEvent<Resource_Sleep>(UpdateSleepMeter);
		}

		private void UpdateSleepMeter(int thirstPercentage)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				SleepValue.Text = CommonFunctionality.ConvertToPercentageText(thirstPercentage);
				SleepValue.BackgroundColor = CommonFunctionality.CalculateStatusColor(thirstPercentage, 100, 25);
			});
		}

		private void Sleep(object sender, EventArgs e)
		{
			creatureInstance.AddToResource<Resource_Sleep>(CommonFunctionality.RESOURCE_ADD_AMOUNT);
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