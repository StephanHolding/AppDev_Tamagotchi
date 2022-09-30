using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Tamagotchi.Service_Locator;
using Tamagotchi.Functionality;
using System.Collections.Specialized;
using System.ComponentModel;
using Xamarin.Forms;

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

		public int CurrentValue { get; set; }
		protected int resourceDecreaseAmountAfterEachTimerEvent = 1;
		public ResourceThreshold[] resourceThresholds;

		protected Creature owner;
		protected ResourceThreshold cachedThreshold;

		protected Resource(Creature owner)
		{
			this.owner = owner;
			Console.WriteLine("CONSTRUCTOR CALLED");
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

			Console.WriteLine("TIME ELAPSED");

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

		public virtual void IncreaseResourceValue(int amount)
		{
			if (CurrentValue + amount <= 100)
				CurrentValue += amount;
			else
				CurrentValue = 100;

			OnResourceChanged?.Invoke(CurrentValue);
		}

		protected void DecreaseResourceValue(int amount)
		{
			if (CurrentValue - amount >= 0)
				CurrentValue -= amount;
			else
				CurrentValue = 0;

			Console.WriteLine("DECREASING VALUE");

			OnResourceChanged?.Invoke(CurrentValue);

			Device.BeginInvokeOnMainThread(() =>
			{
				CheckVoiceLineThreshold();
			});
		}

		private void CheckVoiceLineThreshold()
		{
			ResourceThreshold current = GetCurrentResourceThreshold(out bool succeeded);

			if (!succeeded)
				throw new Exception("No ResourceThreshold found");

			if (cachedThreshold.thresholdPercentage != current.thresholdPercentage)
			{
				cachedThreshold = current;
				owner.Speak(cachedThreshold.dialogueLine);
			}
		}

		private ResourceThreshold GetCurrentResourceThreshold(out bool succeeded)
		{
			for (int i = 0; i < resourceThresholds.Length; i++)
			{
				if (CurrentValue <= resourceThresholds[i].thresholdPercentage)
				{
					if (i + 1 >= resourceThresholds.Length || CurrentValue > resourceThresholds[i + 1].thresholdPercentage)
					{
						succeeded = true;
						return resourceThresholds[i];
					}
				}
			}

			succeeded = false;
			return new ResourceThreshold(0, "");
		}

		protected abstract void SaveData();

	}
}