using System;
using EventBuses;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Widgets
{
	[RequireComponent(typeof(Droppable))]
	public class LobotomiteCardSlot : MonoBehaviour
	{
		private LobotomiteCard _currentCard;
		private Draggable _currentDraggable;
		private Hoverable _currentHoverable;

		[SerializeField] private Transform _CardContainer;
		[SerializeField] private TextMeshProUGUI _LobotomiteNameText;

		[SerializeField] private UnityEvent _OnCardAttached;
		[SerializeField] private UnityEvent _OnCardDetachedNotThrownOut;
		[SerializeField] private UnityEvent<PointerEventData> _OnCardHovered;
		[SerializeField] private UnityEvent<PointerEventData> _OnCardStoppedHovering;

		private bool _isBeingReplaced;
		public bool IsEmpty => !_isBeingReplaced && _currentCard == null;
		public Action OnCardChanged;
		
		private void Awake()
		{
			Droppable droppable = GetComponent<Droppable>();
			droppable._OnDrop.AddListener(OnDrop);
			
			_LobotomiteNameText.text = "";
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

			_LobotomiteNameText.text = card.LobotomiteData._Name;
			
			_currentDraggable = card.GetComponent<Draggable>();
			if (_currentDraggable != null)
			{
				_currentDraggable._OnBeginDrag.AddListener(OnCardLifted);
			}
			else
			{
				Debug.LogError($"{card.name} does not have a Draggable component. Lobotomite cards should always have a Draggable component");
			}
			
			_currentHoverable = card.GetComponent<Hoverable>();
			if (_currentHoverable != null)
			{
				_currentHoverable._OnHover.AddListener(_OnCardHovered.Invoke);
				_currentHoverable._OnStoppedHovering.AddListener(_OnCardStoppedHovering.Invoke);
			}
			else
			{
				Debug.LogError($"{card.name} does not have a Draggable component. Lobotomite cards should always have a Draggable component");
			}
			
			_OnCardAttached?.Invoke();
		}

		private void OnCardLifted(PointerEventData _)
		{
			DetachCard(false);
		}

		private void DetachCard(bool thrownOut)
		{
			_currentDraggable?._OnBeginDrag.RemoveListener(OnCardLifted);
			
			_currentHoverable?._OnHover.RemoveListener(_OnCardHovered.Invoke);
			_currentHoverable?._OnStoppedHovering.RemoveListener(_OnCardStoppedHovering.Invoke);
			
			_currentCard = null;
			_LobotomiteNameText.text = "";
			
			OnCardChanged?.Invoke();
			
			if (!thrownOut)
				_OnCardDetachedNotThrownOut?.Invoke();
		}
		
		private void ThrowOutCard()
		{
			LobotomiteCard detachedCard = _currentCard;
			DetachCard(true);

			LobotomiteCardThrownOutEvent payload = new(detachedCard);
			EventBus<LobotomiteCardThrownOutEvent>.Invoke(payload);
		}
	}
}
