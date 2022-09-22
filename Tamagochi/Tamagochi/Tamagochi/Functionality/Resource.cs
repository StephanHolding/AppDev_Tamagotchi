using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Tamagochi.Service_Locator;
using Tamagotchi.Functionality;

namespace Tamagochi.Functionality
{
	public abstract class Resource
	{
		public struct ResourceThreshold
		{
			public ResourceThreshold(float threshold, string line)
			{
				this.thresholdPercentage = threshold;
				this.dialogueLine = line;
			}

			public float thresholdPercentage;
			public string dialogueLine;
		}

		public string resourceDisplayName=  "NULL";
		public float currentValue = 1;
		public float resourceDecreaseAmountAfterEachTimerEvent = 0.1f;
		public ResourceThreshold[] resourceThresholds;

		protected Resource()
		{
			ServiceLocator.LocateService<TimeManager>().OnTimeElapsed += TimeElapsed;
			App.OnAppSleep += OnSleep;
		}

		private void OnSleep()
		{
			SaveData();
		}

		private void TimeElapsed(double amountOfTimeInMilliseconds)
		{
			int eventTriggerAmount = (int)Math.Floor(amountOfTimeInMilliseconds / TimeManager.TIMER_INTERVAL);
			DecreaseResourceValue(resourceDecreaseAmountAfterEachTimerEvent * eventTriggerAmount);
		}

		protected void IncreaseResourceValue(float amount)
		{
			if (currentValue + amount >= 1)
				currentValue += amount;
			else
				currentValue = 1;
		}

		protected void DecreaseResourceValue(float amount)
		{
			if (currentValue - amount >= 0)
				currentValue -= amount;
			else
				currentValue = 0;
		}

		protected abstract void SaveData();

	}


}
