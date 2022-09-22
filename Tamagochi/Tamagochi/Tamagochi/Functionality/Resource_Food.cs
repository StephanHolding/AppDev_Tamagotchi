using System;
using System.Collections.Generic;
using System.Text;
using Tamagochi.Functionality;
using Xamarin.Essentials;

namespace Tamagotchi.Functionality
{
	internal class Resource_Food : Resource
	{
		public Resource_Food()
		{
			this.resourceDisplayName = "Food";
			this.currentValue = Preferences.Get(nameof(Resource_Food), 1.0f);
			this.resourceDecreaseAmountAfterEachTimerEvent = 0.1f;

			this.resourceThresholds = new ResourceThreshold[]
			{
				new ResourceThreshold(0.8f, "I am getting a little hungry..."),
				new ResourceThreshold(0.4f, "Please give me something to eat."),
				new ResourceThreshold(0.15f, "I'm starving, please I need something to eat...")
			};
		}

		protected override void SaveData()
		{
			Preferences.Set(nameof(Resource_Food), currentValue);
		}
	}
}
