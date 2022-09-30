using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Tamagotchi;
using Tamagotchi.Service_Locator;
using Xamarin.Essentials;

namespace Tamagotchi.Functionality
{
	internal class TimeManager : Service
	{
		public delegate void TimeElapsedEvent(double timeElapsedInMiliseconds);
		public event TimeElapsedEvent OnTimeElapsed;

		public const double TIMER_INTERVAL = 120000.0;
		private const string TIMER_PREF_KEY = "TimerKey";
		private DateTime timeAtAppSleep = DateTime.UtcNow;

		public TimeManager()
		{
			App.OnAppSleep += OnSleep;
		}

		public void Init()
		{
			if (Preferences.ContainsKey(TIMER_PREF_KEY))
			{
				timeAtAppSleep = Preferences.Get(TIMER_PREF_KEY, DateTime.UtcNow);
				double timeSinceLastSession = (DateTime.UtcNow - timeAtAppSleep).TotalMilliseconds;
				TimeElapsed(timeSinceLastSession);
			}

			StartTimer();
		}

		private void OnSleep()
		{
			Preferences.Set(TIMER_PREF_KEY, DateTime.UtcNow);
		}

		private void StartTimer()
		{
			Timer timer = new Timer
			{
				Interval = TIMER_INTERVAL,
				AutoReset = true,
				Enabled = true
			};
			timer.Elapsed += TimeElapsed;

			timer.Start();
		}

		private void TimeElapsed(Object source, ElapsedEventArgs args)
		{
			OnTimeElapsed?.Invoke(TIMER_INTERVAL);
		}

		private void TimeElapsed(double timeElapsedInMiliseconds)
		{
			OnTimeElapsed?.Invoke(timeElapsedInMiliseconds);
		}
	}
}
