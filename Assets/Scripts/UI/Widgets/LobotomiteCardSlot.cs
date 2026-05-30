using System;
using EventBuses;
using TMPro;
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
		[SerializeField] private TextMeshProUGUI _LobotomiteNameText;
		[SerializeField] private Animator _Animator;

		[SerializeField] private string _CloseByItselfTrigger;
		[SerializeField] private string _CloseWithCardTrigger;
		[SerializeField] private string _OpenTrigger;

		private bool _isBeingReplaced;
		public bool IsEmpty => !_isBeingReplaced && _currentCard == null;
		public Action OnCardChanged;
		
		private void Awake()
		{
			Droppable droppable = GetComponent<Droppable>();
			droppable._OnDrop.AddListener(OnDrop);
			_Animator = GetComponent<Animator>();
			
			_LobotomiteNameText.text = "";
		}

		private void OnDrop(PointerEventData eventData)
		{
			GameObject dragged = eventData.pointerDrag;
			
			LobotomiteCard card = dragged.GetComponent<LobotomiteCard>();
			
			if (card)
				AttachCard(card, true);
		}

		public void AttachCard(LobotomiteCard card, bool playAnimation = false)
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
			
			if (playAnimation)
				_Animator.SetTrigger(_CloseWithCardTrigger);
		}

		private void OnCardLifted(PointerEventData _)
		{
			DetachCard(true);
		}

		private void DetachCard(bool playAnimation)
		{
			_currentDraggable?._OnBeginDrag.RemoveListener(OnCardLifted);
			
			_currentCard = null;
			_LobotomiteNameText.text = "";
			
			OnCardChanged?.Invoke();
			
			if (playAnimation)
				_Animator.SetTrigger(_OpenTrigger);
		}
		
		private void ThrowOutCard()
		{
			LobotomiteCard detachedCard = _currentCard;
			DetachCard(false);

			LobotomiteCardThrownOutEvent payload = new(detachedCard);
			EventBus<LobotomiteCardThrownOutEvent>.Invoke(payload);
		}
	}
}
