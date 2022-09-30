using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Tamagotchi.Service_Locator;
using Tamagotchi.Functionality;
using Xamarin.Forms.Internals;
using System.Timers;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Tamagotchi.Functionality
{
	public class Creature : Service
	{
		public delegate void CreatureDialogueEvent(string dialogue);
		public event CreatureDialogueEvent OnDialogueUpdated;
		public string CurrentDialogueToSpeak { get { return currentDialogueToSpeak; } set { currentDialogueToSpeak = value; OnDialogueUpdated?.Invoke(currentDialogueToSpeak); } }

		private Dictionary<Type, Resource> resources = new Dictionary<Type, Resource>();
		private string currentDialogueToSpeak = "";
		private Queue<string> messageQueue = new Queue<string>();
		private const string RESOURCE_PREF_KEY = "Resource_Data";

		public Creature()
		{
			App.OnAppSleep += App_OnAppSleep;

			resources.Add(typeof(Resource_Drink), new Resource_Drink(this));
			resources.Add(typeof(Resource_Attention), new Resource_Attention(this));
			resources.Add(typeof(Resource_Food), new Resource_Food(this));
			resources.Add(typeof(Resource_Sleep), new Resource_Sleep(this));

			if (Preferences.ContainsKey(RESOURCE_PREF_KEY))
			{
				LoadResourceData();
			}

			ServiceLocator.LocateService<TimeManager>().Init();
		}

		private void App_OnAppSleep()
		{
			SaveResourceData();
		}

		public int GetResourceValue<T>() where T : Resource
		{
			return resources[typeof(T)].CurrentValue;
		}

		public void AssignResourceEvent<T>(Resource.ResourceEvent resourceEvent) where T : Resource
		{
			resources[typeof(T)].AssignResourceListener(resourceEvent);
		}

		public void RemoveResourceEvent<T>(Resource.ResourceEvent resourceEvent) where T : Resource
		{
			resources[typeof(T)].RemoveResourceListener(resourceEvent);
		}

		public void AddToResource<T>(int amount)
		{
			resources[typeof(T)].IncreaseResourceValue(amount);
		}

		public void Speak(params string[] messages)
		{
			foreach (string message in messages)
			{
				if (!string.IsNullOrEmpty(message))
					messageQueue.Enqueue(message);
			}

			if (string.IsNullOrEmpty(CurrentDialogueToSpeak))
			{
				ContinueDialogue();
			}
		}

		public void ContinueDialogue()
		{
			if (messageQueue.Count > 0)
				CurrentDialogueToSpeak = messageQueue.Dequeue();
			else
				CurrentDialogueToSpeak = "";
		}

		public string GetCurrentDisplayingMessage()
		{
			if (CurrentDialogueToSpeak != null)
				return CurrentDialogueToSpeak;
			else
				return "";
		}

		private void SaveResourceData()
		{
			Dictionary<Type, int> toSave = new Dictionary<Type, int>();

			foreach (KeyValuePair<Type, Resource> pair in resources)
			{
				toSave.Add(pair.Key, pair.Value.CurrentValue);
			}

			string JSON = JsonConvert.SerializeObject(toSave);
			Preferences.Set(RESOURCE_PREF_KEY, JSON);
		}

		private void LoadResourceData()
		{
			string JSON = Preferences.Get(RESOURCE_PREF_KEY, "");
			Console.WriteLine(JSON);
			Dictionary<Type, int> loaded = JsonConvert.DeserializeObject<Dictionary<Type, int>>(JSON);

			foreach (KeyValuePair<Type, int> pair in loaded)
			{
				resources[pair.Key].CurrentValue = pair.Value;
			}
		}
	}
}
