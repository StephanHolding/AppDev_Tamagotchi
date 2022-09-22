using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Tamagochi.Service_Locator;
using Tamagotchi.Functionality;
using Xamarin.Forms.Internals;

namespace Tamagochi.Functionality
{
	internal class Creature : Service
	{
		public Resource[] resources;

		public delegate void CreatureEvent();
		public event CreatureEvent OnDialogueUpdated;

		private string currentDialogueToSpeak = "";
		private string CurrentDialogueToSpeak { get { return currentDialogueToSpeak; } set { currentDialogueToSpeak = value; OnDialogueUpdated?.Invoke(); } }
		private Queue<string> messageQueue = new Queue<string>();

		public Creature()
		{
			resources = new Resource[]
			{
				new Resource_Food()
			};
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
