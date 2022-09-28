using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagotchi.Functionality
{
	public static class CommonFunctionality
	{
		public static string playerName = "null";

		public const float RESOURCE_ADD_AMOUNT = 0.3f;

		public static string ConvertToPercentageText(double percentage)
		{
			string toReturn = (percentage * 100).ToString() + "%";
			return toReturn;
		}
	}
}
