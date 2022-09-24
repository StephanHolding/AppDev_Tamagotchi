using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagotchi.Service_Locator
{
	public abstract class Service
	{
		public Service()
		{
			ProvideMyselfToServiceLocator();
		}

		protected void ProvideMyselfToServiceLocator()
		{
			if (!ServiceLocator.ProvideService(this))
			{
				throw new Exception("The Service could not be provided");
			}
		}
	}
}
