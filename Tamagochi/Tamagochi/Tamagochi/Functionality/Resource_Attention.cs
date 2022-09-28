using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Tamagotchi.Functionality
{
	internal class Resource_Attention : Resource
	{
		public Resource_Attention(Creature owner) : base(owner)
		{
			this.resourceThresholds = new ResourceThreshold[]
			{
				new ResourceThreshold(0.8f, "I am getting a little thirsty..."),
				new ResourceThreshold(0.4f, "Please give me something to drink."),
				new ResourceThreshold(0.15f, "I'm so dehydrated, please I need something to drink...")
			};
		}

		protected override void SaveData()
		{
			Preferences.Set(nameof(Resource_Attention), CurrentValue);
		}
	}
}
