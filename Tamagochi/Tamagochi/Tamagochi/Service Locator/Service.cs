using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi.Service_Locator
{
	internal abstract class Service
	{
		public Service()
		{
			if (!ServiceLocator.ProvideService(this))
			{
				throw new Exception("Kan niet he");
			}
		}
	}
}
