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
	public partial class DrinkPage : ContentPage
	{

		private Creature creatureInstance;

		public DrinkPage()
		{
			InitializeComponent();

			creatureInstance = ServiceLocator.LocateService<Creature>();
			creatureInstance.AssignResourceEvent<Resource_Drink>(UpdateThirstMeter);
		}

		~DrinkPage()
		{
			creatureInstance.RemoveResourceEvent<Resource_Drink>(UpdateThirstMeter);
		}

		private void UpdateThirstMeter(float thirstPercentage)
		{

		}

		private void GiveDrink(object sender, EventArgs e)
		{
			creatureInstance.AddToResource<Resource_Drink>(Settings.RESOURCE_ADD_AMOUNT);
			creatureInstance.Speak("Thank you for giving me something to drink");
		}
	}
}