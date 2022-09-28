using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tamagotchi.Functionality;
using Tamagotchi.Service_Locator;

namespace Tamagotchi
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AttentionPage : ContentPage, IShowDialogue
	{
		private Creature creatureInstance;

		public AttentionPage()
		{
			InitializeComponent();

			creatureInstance = ServiceLocator.LocateService<Creature>();

			creatureInstance.OnDialogueUpdated += ShowDialogue;
			creatureInstance.AssignResourceEvent<Resource_Attention>(UpdateAttentionMeter);

			ShowDialogue(creatureInstance.CurrentDialogueToSpeak);
			UpdateAttentionMeter(creatureInstance.GetResourceValue<Resource_Attention>());
		}

		~AttentionPage()
		{
			creatureInstance.OnDialogueUpdated -= ShowDialogue;
			creatureInstance.RemoveResourceEvent<Resource_Attention>(UpdateAttentionMeter);
		}

		private void UpdateAttentionMeter(int attentionValue)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				AttentionValue.Text = CommonFunctionality.ConvertToPercentageText(attentionValue);
				AttentionValue.BackgroundColor = CommonFunctionality.CalculateStatusColor(attentionValue, 50, 15);
			});
		}

		private void Play(object sender, EventArgs e)
		{
			creatureInstance.AddToResource<Resource_Attention>(CommonFunctionality.RESOURCE_ADD_AMOUNT);
			creatureInstance.Speak("Thank you for spending time with me");
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