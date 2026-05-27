using System;

namespace EventBuses
{
	public static class EventBus<T>
	{
		public static event Action<T> Event;

		public static void Invoke(T payload)
		{
			Event?.Invoke(payload);
		}
	}
}
