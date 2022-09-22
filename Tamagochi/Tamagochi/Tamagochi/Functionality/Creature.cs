using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Tamagochi.Service_Locator;
using Xamarin.Forms.Internals;

namespace Tamagochi.Functionality
{
	internal class Creature : Service
	{
		public Resource[] resources = null;

		public delegate void CreatureEvent();
		public event CreatureEvent onDialogueUpdated;

		private string currentDisplayingMessage = "";
		private string CurrentDisplayingMessage { get { return currentDisplayingMessage; } set { currentDisplayingMessage = value; onDialogueUpdated?.Invoke(); } }
		private Queue<string> messageQueue = new Queue<string>();

		//public string CurrentDisplayingMessage { get { return currentDisplayingMessage; } set { currentDisplayingMessage = value; OnPropertyChanged(nameof(CurrentDisplayingMessage)); } }
		//public event PropertyChangedEventHandler PropertyChanged;



		public void Speak(string message)
		{
			messageQueue.Enqueue(message);

			if (string.IsNullOrEmpty(CurrentDisplayingMessage))
			{
				ContinueDialogue();
			}
		}

		public void Speak(string[] messages)
		{
			foreach (string message in messages)
			{
				messageQueue.Enqueue(message);
			}

			//messages.ForEach(x => messageQueue.Enqueue(x));

			if (string.IsNullOrEmpty(CurrentDisplayingMessage))
			{
				ContinueDialogue();
			}
		}

		public void ContinueDialogue()
		{
			if (messageQueue.Count > 0)
				CurrentDisplayingMessage = messageQueue.Dequeue();
			else
				CurrentDisplayingMessage = "";
		}

		public string GetCurrentDisplayingMessage()
		{
			if (CurrentDisplayingMessage != null)
				return CurrentDisplayingMessage;
			else
				return "";
		}

		//protected void OnPropertyChanged(string propertyName)
		//{
		//	PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		//}
	}
}
