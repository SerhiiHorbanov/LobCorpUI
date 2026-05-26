using System;

namespace EventBuses
{
	public class EventBus<T>
	{
		public static event Action<T> Event;

		private static void Call(T payload)
		{
			Event?.Invoke(payload);
		}
	}
}
