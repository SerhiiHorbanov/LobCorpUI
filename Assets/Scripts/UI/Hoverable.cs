using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
	public class Hoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		public UnityEvent _OnHover;
		public UnityEvent _OnStoppedHovering;

		public void OnPointerEnter(PointerEventData eventData)
		{
			_OnHover.Invoke();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			_OnStoppedHovering.Invoke();
		}
	}
}
