﻿using System;
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
				new ResourceThreshold(95, "Can you leave me alone for a while"),
				new ResourceThreshold(60, "It's fun to spend time with you :)"),
				new ResourceThreshold(40, "I'm starting to feel a little lonely now"),
				new ResourceThreshold(15, "I'm so lonely...")
			};
		}

		protected override void SaveData()
		{
			Preferences.Set(nameof(Resource_Attention), CurrentValue);
		}
	}
}