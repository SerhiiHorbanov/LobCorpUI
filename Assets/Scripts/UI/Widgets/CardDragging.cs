using EventBuses;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Widgets
{
	[RequireComponent(typeof(LobotomiteCard))]
	[RequireComponent(typeof(Draggable))]
	[RequireComponent(typeof(CanvasGroup))]
	public class CardDragging : MonoBehaviour
	{
		private LobotomiteCard _card;
		private Draggable _draggable;
		private CanvasGroup _canvasGroup;

		private void Start()
		{
			_card = GetComponent<LobotomiteCard>();
			_draggable = GetComponent<Draggable>();
			_canvasGroup = GetComponent<CanvasGroup>();
			
			_draggable._OnBeginDrag.AddListener(OnBeginDrag);
			_draggable._OnDrag.AddListener(OnDrag);
			_draggable._OnEndDrag.AddListener(OnEndDrag);
		}
		
		public void OnBeginDrag(PointerEventData eventData)
		{
			_canvasGroup.blocksRaycasts = false;
			_canvasGroup.interactable = false;
			
			_card.Select();
			
			Canvas rootCanvas = _card.GetComponentInParent<Canvas>().rootCanvas;
			_card.transform.SetParent(rootCanvas.transform, true);
		}
		
		private void OnDrag(PointerEventData eventData)
		{
			_card.transform.position = eventData.position;
		}

		private void OnEndDrag(PointerEventData eventData)
		{
			_canvasGroup.blocksRaycasts = true;
			_canvasGroup.interactable = true;
			
			Canvas rootCanvas = _card.GetComponentInParent<Canvas>().rootCanvas;
			
			if (transform.parent == rootCanvas.transform)
			{
				LobotomiteCardThrownOutEvent payload = new(_card);
				EventBus<LobotomiteCardThrownOutEvent>.Invoke(payload);
			}
		}
	}
}
