using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Tamagotchi.Functionality
{
	public static class CommonFunctionality
	{
		public static string playerName = "null";
		public const int RESOURCE_ADD_AMOUNT = 30;

		private static Color StatusGood = Color.Green;
		private static Color StatusBad = Color.Red;
		private static Color StatusWorse = Color.Orange;
		private static Color StatusNeutral = Color.Yellow;

		public static string ConvertToPercentageText(int percentage)
		{
			string toReturn = percentage.ToString() + "%";
			return toReturn;
		}

		public static Color CalculateStatusColor(int value, int desired, int step)
		{
			int dif = Math.Abs(value - desired);

			if (dif <= step)
				return StatusGood;
			else if (dif >= step && dif < step * 2)
				return StatusNeutral;
			else if (dif >= step * 2 && dif < step * 3)
				return StatusWorse;
			else if (dif >= step * 3)
				return StatusBad;
			else
				return Color.Black;
		}
	}
}
