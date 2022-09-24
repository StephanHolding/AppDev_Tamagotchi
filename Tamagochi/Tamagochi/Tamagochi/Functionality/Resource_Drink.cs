using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Tamagotchi.Functionality;

namespace Tamagotchi.Functionality
{
	internal class Resource_Drink : Resource
	{
		public Resource_Drink(Creature owner) : base(owner)
		{
			//this.CurrentValue = Preferences.Get(nameof(Resource_Drink), 1.0f);
			this.resourceDecreaseAmountAfterEachTimerEvent = 0.001f;

			this.resourceThresholds = new ResourceThreshold[]
			{
				new ResourceThreshold(0.8f, "I am getting a little thirsty..."),
				new ResourceThreshold(0.4f, "Please give me something to drink."),
				new ResourceThreshold(0.15f, "I'm so dehydrated, please I need something to drink...")
			};
		}

		protected override void SaveData()
		{
			Preferences.Set(nameof(Resource_Drink), CurrentValue);
		}
	}
}
