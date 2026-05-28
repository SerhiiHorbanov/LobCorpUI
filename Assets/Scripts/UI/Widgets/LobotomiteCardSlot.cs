using EventBuses;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Widgets
{
	[RequireComponent(typeof(Droppable))]
	public class LobotomiteCardSlot : MonoBehaviour
	{
		private LobotomiteCard _currentCard;

		[SerializeField] private Transform _CardContainer;

		// ReSharper disable once InconsistentNaming
		public GameObject TESTING_cardPrefab;
		
		private void Start()
		{
			Droppable droppable = GetComponent<Droppable>();
			droppable._OnDrop.AddListener(OnDrop);
			//GameObject card = Instantiate(TESTING_cardPrefab);
			//AttachCard(card.GetComponent<LobotomiteCard>());
		}

		private void OnDrop(PointerEventData eventData)
		{
			GameObject dragged = eventData.pointerDrag;
			
			LobotomiteCard card = dragged.GetComponent<LobotomiteCard>();
			
			if (card)
				AttachCard(card);
		}

		private void AttachCard(LobotomiteCard card)
		{
			if (_currentCard != null)
				DetachCard();
			
			_currentCard = card;
			card.transform.SetParent(_CardContainer, false);
			card.transform.localPosition = Vector3.zero;
		}
		
		private void DetachCard()
		{
			LobotomiteCardThrownOutEvent payload = new(_currentCard);
			EventBus<LobotomiteCardThrownOutEvent>.Invoke(payload);
			
			_currentCard = null;
		}
	}
}
