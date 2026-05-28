using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Widgets
{
	[RequireComponent(typeof(LobotomiteCard))]
	[RequireComponent(typeof(Draggable))]
	public class CardDragging : MonoBehaviour
	{
		[SerializeField] private LobotomiteCard _Card;
		[SerializeField] private Draggable _Draggable;

		private void Start()
		{
			_Card = GetComponent<LobotomiteCard>();
			_Draggable = GetComponent<Draggable>();
			
			_Draggable._OnBeginDrag.AddListener(OnBeginDrag);
			_Draggable._OnDrag.AddListener(OnDrag);
		}
		
		public void OnBeginDrag(PointerEventData eventData)
		{
			_Card.Select();
		}
		
		private void OnDrag(PointerEventData eventData)
		{
			_Card.transform.position = eventData.position;
		}
	}
}
