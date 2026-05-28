using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
	public class Droppable : MonoBehaviour, IDropHandler
	{
		public UnityEvent<PointerEventData> _OnDrop;

		public void OnDrop(PointerEventData eventData)
		{
			_OnDrop.Invoke(eventData);
		}
	}
}
