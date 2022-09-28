using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Tamagotchi.Functionality
{
	internal class Resource_Food : Resource
	{
		public Resource_Food(Creature owner) : base(owner)
		{
			this.resourceThresholds = new ResourceThreshold[]
			{
				new ResourceThreshold(75, "I am getting a little hungry..."),
				new ResourceThreshold(35, "Please give me something to eat."),
				new ResourceThreshold(10, "I'm starving, please I need something to eat...")
			};
		}

		protected override void SaveData()
		{
			Preferences.Set(nameof(Resource_Food), CurrentValue);
		}
	}
}
