using System.Collections.Generic;
using EventBuses;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Widgets
{
    public class LobotomitesListPanel : MonoBehaviour
    {
        [FormerlySerializedAs("_Lobotomites")] [SerializeField] private LobotomiteData[] _InitialLobotomites;
        [SerializeField] private GameObject _CardsContainerObject;
        [SerializeField] private GameObject _CardSlotPrefab;
        [SerializeField] private GameObject _CardPrefab;
        
        private readonly List<LobotomiteCardSlot> _slots = new();
        
        private void Start()
        {
            AddEmptySlot();
            
            foreach (LobotomiteData lobotomite in _InitialLobotomites)
            {
                AddCard(lobotomite);
            }

            EventBus<LobotomiteCardThrownOutEvent>.Event += AttachCard;
        }

        private void AttachCard(LobotomiteCardThrownOutEvent payload)
            => AttachCard(payload.Card);

        private void AttachCard(LobotomiteCard card)
        { 
            EnsureSingleLastEmptySlot();
            AddEmptySlot();
            AddEmptySlot();
            _slots[^1].AttachCard(card);
            EnsureSingleLastEmptySlot();
        }
        
        private void AddCard(LobotomiteData lobotomite)
        {
            GameObject card = Instantiate(_CardPrefab);
            LobotomiteCard cardComponent = card.GetComponent<LobotomiteCard>();
            cardComponent.Initialize(lobotomite);
            
            AttachCard(cardComponent);
        }

        private void AddEmptySlot()
        {
            GameObject slot = Instantiate(_CardSlotPrefab, _CardsContainerObject.transform);
            LobotomiteCardSlot lobotomiteCardSlot = slot.GetComponent<LobotomiteCardSlot>();
            lobotomiteCardSlot.OnCardChanged += EnsureSingleLastEmptySlot;

            if (lobotomiteCardSlot == null)
            {
                Debug.LogError("LobotomiteCardSlot component not found on slot prefab");
                return;
            }
            
            _slots.Add(lobotomiteCardSlot);
        }

        private void EnsureSingleLastEmptySlot()
        {
            for (int i = 0; i < _slots.Count - 1; i++)
            {
                if (_slots[i].IsEmpty)
                {
                    Destroy(_slots[i].gameObject);
                    _slots.RemoveAt(i);
                    i--;
                }
            }
            
            if (!_slots[^1].IsEmpty)
                AddEmptySlot();
        }
    }
}
