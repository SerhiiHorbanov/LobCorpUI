using EventBuses;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
	[RequireComponent(typeof(Button))]
	[RequireComponent(typeof(Hoverable))]
	public class LobotomiteCard : MonoBehaviour
	{
		private LobotomiteData _data;
		
		[SerializeField] private LobotomiteVisuals _LobotomiteVisuals;
		[SerializeField] private VirtueDisplay _FortitudeDisplay;
		[SerializeField] private VirtueDisplay _PrudenceDisplay;
		[SerializeField] private VirtueDisplay _TemperanceDisplay;
		[SerializeField] private VirtueDisplay _JusticeDisplay;

		private bool _isSelected;
		private Button _button;
		private Hoverable _hoverable;

		public LobotomiteData LobotomiteData => _data;
		
		public void Initialize(LobotomiteData data)
		{
			if (_data != null)
			{
				Debug.LogError($"{gameObject.name} already initialized with {_data.name}. Tried to initialize again with {data.name}. Lobotomite cards should only be initialized once");
				return;
			}
			
			_data = data;
			
			_FortitudeDisplay.SetValue(_data._Fortitude);
			_PrudenceDisplay.SetValue(_data._Prudence);
			_TemperanceDisplay.SetValue(_data._Temperance);
			_JusticeDisplay.SetValue(_data._Justice);
			
			_LobotomiteVisuals.SetLobotomite(_data._Visuals);
			
			_button = GetComponent<Button>();
			_button.onClick.AddListener(Select);
			
			_hoverable = GetComponent<Hoverable>();
			_hoverable._OnHover.AddListener(OnHovered);
			_hoverable._OnStoppedHovering.AddListener(OnStoppedHovering);
		}

		public void Select()
		{
			HardLobotomiteSelectionEventPayload payload = new(_data);
			EventBus<HardLobotomiteSelectionEventPayload>.Invoke(payload);

			EventBus<HardLobotomiteSelectionEventPayload>.Event += Deselect;
			
			_button.onClick.RemoveListener(Select);
			_button.onClick.AddListener(DeselectAndNotify);
		}

		private void DeselectAndNotify()
		{
			HardLobotomiteSelectionEventPayload payload = new(null);
			EventBus<HardLobotomiteSelectionEventPayload>.Event -= Deselect;
			EventBus<HardLobotomiteSelectionEventPayload>.Invoke(payload);
			Deselect();
		}
		
		private void Deselect(HardLobotomiteSelectionEventPayload _)
			=> Deselect();
		
		public void Deselect()
		{
			EventBus<HardLobotomiteSelectionEventPayload>.Event -= Deselect;
			_button.onClick.AddListener(Select);
			_button.onClick.RemoveListener(DeselectAndNotify);
		}

		public void OnHovered(UnityEngine.EventSystems.PointerEventData _)
			=> EventBus<SoftLobotomiteSelectionEventPayload>.Invoke(new(_data));
		
		private void OnStoppedHovering(UnityEngine.EventSystems.PointerEventData _)
			=> EventBus<SoftLobotomiteDeselectionEventPayload>.Invoke(new(_data));
	}
}
