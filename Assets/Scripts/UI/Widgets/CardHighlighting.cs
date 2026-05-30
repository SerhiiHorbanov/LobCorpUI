using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Widgets
{
    [RequireComponent(typeof(Hoverable))]
    [RequireComponent(typeof(AudioSource))]
    public class CardHighlighting : MonoBehaviour
    {
        private Hoverable _hoverable;
        private AudioSource _audioSource;

        [SerializeField] private Image _Outline;
        [SerializeField] private Color _DefaultColor;
        [SerializeField] private Color _HighlightedColor;
    
        private void Awake()
        {
            _hoverable = GetComponent<Hoverable>();
            _audioSource = GetComponent<AudioSource>();
        
            _hoverable._OnHover.AddListener(OnHover);
            _hoverable._OnStoppedHovering.AddListener(OnStoppedHovering);
        }

        private void OnDestroy()
        {
            _hoverable?._OnHover.RemoveListener(OnHover);
            _hoverable?._OnStoppedHovering.RemoveListener(OnStoppedHovering);
        }
    
        private void OnHover(PointerEventData _)
        {
            _Outline.color = _HighlightedColor;
            _audioSource.Play();
        }
    
        private void OnStoppedHovering(PointerEventData _)
        {
            _Outline.color = _DefaultColor;
        }
    }
}
