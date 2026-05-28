using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
	public class Hoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		public UnityEvent<PointerEventData> _OnHover;
		public UnityEvent<PointerEventData> _OnStoppedHovering;

		public void OnPointerEnter(PointerEventData eventData)
		{
			_OnHover.Invoke(eventData);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			_OnStoppedHovering.Invoke(eventData);
		}
	}
}
