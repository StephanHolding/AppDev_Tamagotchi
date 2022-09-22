using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi.Functionality
{
	internal class Resource
	{
		public struct ResourceThreshold
		{
			public float thresholdPercentage;
			public string dialogueLine;
		}

		Resource()
		{

		}

		public string resouceName;
		public float currentValue;
		public ResourceThreshold[] resourceThresholds;
	}


}
