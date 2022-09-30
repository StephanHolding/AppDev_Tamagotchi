using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Tamagotchi.Service_Locator;
using Tamagotchi.Functionality;
using Xamarin.Forms.Internals;
using System.Timers;

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
		public Creature()
		{
			resources.Add(typeof(Resource_Drink), new Resource_Drink(this));
			resources.Add(typeof(Resource_Attention), new Resource_Attention(this));
			resources.Add(typeof(Resource_Food), new Resource_Food(this));
			resources.Add(typeof(Resource_Sleep), new Resource_Sleep(this));
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
	}
}
