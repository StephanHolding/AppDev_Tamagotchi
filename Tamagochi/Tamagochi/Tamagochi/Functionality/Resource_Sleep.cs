using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Tamagotchi.Functionality
{
	public class Resource_Sleep : Resource
	{
		public Resource_Sleep(Creature owner) : base(owner)
		{
			this.resourceThresholds = new ResourceThreshold[]
			{
				new ResourceThreshold(100, "Wide awake... eyes opened..."),
				new ResourceThreshold(95, null),
				new ResourceThreshold(50, "I'm getting kind of sleepy"),
				new ResourceThreshold(35, "*yawn*"),
				new ResourceThreshold(5, "I can't keep my eyes open")
			};

			CurrentValue = 90;
		}

		public override void IncreaseResourceValue(int amount)
		{
			base.IncreaseResourceValue(amount);
			owner.Speak("ZZZZZZZZZZZZZZZ....");
		}
	}
}
