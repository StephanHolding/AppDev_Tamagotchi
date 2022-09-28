using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Tamagotchi.Service_Locator;
using Tamagotchi.Functionality;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Tamagotchi.Functionality
{
	public abstract class Resource
	{
		public struct ResourceThreshold
		{
			public ResourceThreshold(int threshold, string line)
			{
				this.thresholdPercentage = threshold;
				this.dialogueLine = line;
			}

			public int thresholdPercentage;
			public string dialogueLine;
		}

		public delegate void ResourceEvent(int _currentValue);
		private event ResourceEvent OnResourceChanged;

		public int CurrentValue { get; set; } = 100;
		protected int resourceDecreaseAmountAfterEachTimerEvent = 1;
		public ResourceThreshold[] resourceThresholds;

		private Creature owner;

		protected Resource(Creature owner)
		{
			this.owner = owner;
			ServiceLocator.LocateService<TimeManager>().OnTimeElapsed += TimeElapsed;
			App.OnAppSleep += OnSleep;
		}

		public void AssignResourceListener(ResourceEvent function)
		{
			OnResourceChanged += function;
		}

		public void RemoveResourceListener(ResourceEvent function)
		{
			OnResourceChanged -= function;
		}

		private void OnSleep()
		{
			SaveData();
		}

		private void TimeElapsed(double amountOfTimeInMilliseconds)
		{
			if (amountOfTimeInMilliseconds != TimeManager.TIMER_INTERVAL)
			{
				int eventTriggerAmount = (int)Math.Floor(amountOfTimeInMilliseconds / TimeManager.TIMER_INTERVAL);
				DecreaseResourceValue(resourceDecreaseAmountAfterEachTimerEvent * eventTriggerAmount);
			}
			else
			{
				DecreaseResourceValue(resourceDecreaseAmountAfterEachTimerEvent);
			}
		}

		public void IncreaseResourceValue(int amount)
		{
			if (CurrentValue + amount <= 1)
				CurrentValue += amount;
			else
				CurrentValue = 1;

			OnResourceChanged?.Invoke(CurrentValue);
		}

		protected void DecreaseResourceValue(int amount)
		{
			if (CurrentValue - amount >= 0)
				CurrentValue -= amount;
			else
				CurrentValue = 0;

			OnResourceChanged?.Invoke(CurrentValue);
			CheckVoiceLineThreshold();
		}

		private void CheckVoiceLineThreshold()
		{
			for (int i = 0; i < resourceThresholds.Length; i++)
			{
				if (CurrentValue <= resourceThresholds[i].thresholdPercentage)
				{
					if (i + 1 >= resourceThresholds.Length || CurrentValue > resourceThresholds[i + 1].thresholdPercentage)
					{
						owner.Speak(resourceThresholds[i].dialogueLine);
						return;
					}
				}
			}
		}

		protected abstract void SaveData();

	}
}