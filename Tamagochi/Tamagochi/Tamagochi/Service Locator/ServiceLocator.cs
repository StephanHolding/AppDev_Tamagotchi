using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi.Service_Locator
{
	internal static class ServiceLocator
	{
		private static Dictionary<Type, Service> services = new Dictionary<Type, Service>();

		public static T LocateService<T>() where T : Service
		{
			if (services.ContainsKey(typeof(T)))
			{
				return services[typeof(T)] as T;
			}
			else
			{
				return null;
			}
		}

		public static bool ProvideService(Service newService)
		{
			if (!services.ContainsKey(newService.GetType()))
			{
				services.Add(newService.GetType(), newService);
				return true;
			}

			return false;
		}
	}
}
