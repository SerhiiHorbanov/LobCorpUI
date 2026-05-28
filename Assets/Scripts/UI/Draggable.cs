using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
	public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public UnityEvent<PointerEventData> _OnBeginDrag;
		public UnityEvent<PointerEventData> _OnDrag;
		public UnityEvent<PointerEventData> _OnEndDrag;

		public void OnBeginDrag(PointerEventData eventData)
		{
			_OnBeginDrag.Invoke(eventData);
		}

		public void OnDrag(PointerEventData eventData)
		{
			_OnDrag.Invoke(eventData);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			_OnEndDrag.Invoke(eventData);
		}
	}
}
