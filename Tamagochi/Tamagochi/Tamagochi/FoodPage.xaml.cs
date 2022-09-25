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
	public partial class FoodPage : ContentPage
	{

		private Creature creatureInstance;

		public FoodPage()
		{
			InitializeComponent();

			creatureInstance = ServiceLocator.LocateService<Creature>();
			creatureInstance.AssignResourceEvent<Resource_Food>(UpdateFoodMeter);
		}

		~FoodPage()
		{
			creatureInstance.RemoveResourceEvent<Resource_Food>(UpdateFoodMeter);
		}

		private void UpdateFoodMeter(float hungerPercentage)
		{

		}

		private void Feed(object sender, EventArgs e)
		{
			creatureInstance.AddToResource<Resource_Food>(Settings.RESOURCE_ADD_AMOUNT);
			creatureInstance.Speak("Thank you for giving me food :)");
		}
	}
}