using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Tamagotchi.Functionality
{
	public class Resource_Drink : Resource
	{
		public Resource_Drink(Creature owner) : base(owner)
		{
			this.resourceThresholds = new ResourceThreshold[]
			{
				new ResourceThreshold(80, "I am getting a little thirsty..."),
				new ResourceThreshold(40, "Please give me something to drink."),
				new ResourceThreshold(15, "I'm so dehydrated, please I need something to drink...")
			};
		}

		protected override void SaveData()
		{
			Preferences.Set(nameof(Resource_Drink), CurrentValue);
		}
	}
}
