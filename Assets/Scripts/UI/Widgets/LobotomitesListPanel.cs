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
        }

        private void AddCard(LobotomiteData lobotomite)
        {
            GameObject card = Instantiate(_CardPrefab, _CardsContainerObject.transform);
            LobotomiteCard cardComponent = card.GetComponent<LobotomiteCard>();
            
            cardComponent.Initialize(lobotomite);
        }
    }
}
