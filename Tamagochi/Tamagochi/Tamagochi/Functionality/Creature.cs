using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Tamagotchi.Service_Locator;
using Tamagotchi.Functionality;
using Xamarin.Forms.Internals;

namespace Tamagotchi.Functionality
{
	public class Creature : Service
	{

		public delegate void CreatureEvent();
		public event CreatureEvent OnDialogueUpdated;

		private Dictionary<Type, Resource> resources = new Dictionary<Type, Resource>();
		private string currentDialogueToSpeak = "";
		private string CurrentDialogueToSpeak { get { return currentDialogueToSpeak; } set { currentDialogueToSpeak = value; OnDialogueUpdated?.Invoke(); } }
		private Queue<string> messageQueue = new Queue<string>();

		public Creature()
		{
			resources.Add(typeof(Resource_Food), new Resource_Food(this));
			resources.Add(typeof(Resource_Drink), new Resource_Drink(this));
		}

		//public float GetResourceValue<T>() where T : Resource
		//{
		//	return resources[typeof(T)].currentValue;
		//}

		public void AssignResourceEvent<T>(Resource.ResourceEvent resourceEvent) where T : Resource
		{
			resources[typeof(T)].AssignResourceListener(resourceEvent);
		}

		public void RemoveResourceEvent<T>(Resource.ResourceEvent resourceEvent) where T : Resource
		{
			resources[typeof(T)].RemoveResourceListener(resourceEvent);
		}

		public void Speak(params string[] messages)
		{
			foreach (string message in messages)
			{
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
