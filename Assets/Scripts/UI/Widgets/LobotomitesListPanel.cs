using EventBuses;
using UnityEngine;

namespace UI.Widgets
{
    public class LobotomitesListPanel : MonoBehaviour
    {
        [SerializeField] private LobotomiteData[] _Lobotomites;
        [SerializeField] private GameObject _CardsContainerObject;
        [SerializeField] private GameObject _CardPrefab;
    
        private void Start()
        {
            foreach (LobotomiteData lobotomite in _Lobotomites)
            {
                AddCard(lobotomite);
            }

            EventBus<LobotomiteCardThrownOutEvent>.Event += AttachCard;
        }

        private void AttachCard(LobotomiteCardThrownOutEvent payload)
            => AttachCard(payload.Card);

        private void AttachCard(LobotomiteCard card)
        {
            card.transform.SetParent(_CardsContainerObject.transform, false);
        }
        
        private void AddCard(LobotomiteData lobotomite)
        {
            GameObject card = Instantiate(_CardPrefab);
            LobotomiteCard cardComponent = card.GetComponent<LobotomiteCard>();
            cardComponent.Initialize(lobotomite);
            
            AttachCard(cardComponent);
        }
    }
}
