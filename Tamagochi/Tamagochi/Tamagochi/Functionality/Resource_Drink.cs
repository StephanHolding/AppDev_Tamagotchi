using System;
using System.Collections.Generic;
using System.Text;
using static Tamagochi.Functionality.Resource;
using Xamarin.Essentials;
using Tamagochi.Functionality;

namespace Tamagotchi.Functionality
{
	internal class Resource_Drink : Resource
	{
		public Resource_Drink()
		{
			this.resourceDisplayName = "Thirst";
			this.currentValue = Preferences.Get(nameof(Resource_Drink), 1.0f);
			this.resourceDecreaseAmountAfterEachTimerEvent = 0.1f;

			this.resourceThresholds = new ResourceThreshold[]
			{
				new ResourceThreshold(0.8f, "I am getting a little thirsty..."),
				new ResourceThreshold(0.4f, "Please give me something to drink."),
				new ResourceThreshold(0.15f, "I'm so dehydrated, please I need something to drink...")
			};
		}

		protected override void SaveData()
		{
			Preferences.Set(nameof(Resource_Drink), currentValue);
		}
	}
}
