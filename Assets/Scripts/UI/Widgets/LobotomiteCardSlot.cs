using System;
using EventBuses;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Widgets
{
	[RequireComponent(typeof(Droppable))]
	public class LobotomiteCardSlot : MonoBehaviour
	{
		private LobotomiteCard _currentCard;
		private Draggable _currentDraggable;

		[SerializeField] private Transform _CardContainer;

		private bool _isBeingReplaced;
		public bool IsEmpty => !_isBeingReplaced && _currentCard == null;
		public Action OnCardChanged;
		
		private void Start()
		{
			Droppable droppable = GetComponent<Droppable>();
			droppable._OnDrop.AddListener(OnDrop);
		}

		private void OnDrop(PointerEventData eventData)
		{
			GameObject dragged = eventData.pointerDrag;
			
			LobotomiteCard card = dragged.GetComponent<LobotomiteCard>();
			
			if (card)
				AttachCard(card);
		}

		public void AttachCard(LobotomiteCard card)
		{
			_isBeingReplaced = true;
			if (_currentCard != null)
				ThrowOutCard();
			_isBeingReplaced = false;
			
			_currentCard = card;
			OnCardChanged?.Invoke();
			card.transform.SetParent(_CardContainer, false);
			card.transform.localPosition = Vector3.zero;
			
			_currentDraggable = card.GetComponent<Draggable>();
			if (_currentDraggable != null)
			{
				_currentDraggable._OnBeginDrag.AddListener(DetachCard);
			}
			else
			{
				Debug.LogError($"{card.name} does not have a Draggable component. Lobotomite cards should always have a Draggable component");
			}
		}

		private void DetachCard(PointerEventData _)
			=> DetachCard();
		
		private void DetachCard()
		{
			_currentDraggable?._OnBeginDrag.RemoveListener(DetachCard);
			
			_currentCard = null;
			OnCardChanged?.Invoke();
		}
		
		private void ThrowOutCard()
		{
			LobotomiteCard detachedCard = _currentCard;
			DetachCard();

			LobotomiteCardThrownOutEvent payload = new(detachedCard);
			EventBus<LobotomiteCardThrownOutEvent>.Invoke(payload);
		}
	}
}
