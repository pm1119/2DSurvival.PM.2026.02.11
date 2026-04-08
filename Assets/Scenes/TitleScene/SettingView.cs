using UnityEngine;
using UnityEngine.UI;

public class SettingCtrl : MonoBehaviour
{
	SoundManager _soundManager;

	[Header("----- ÄÄÆ÷³ÍÆ® -----")]
	[SerializeField] GameObject _settingPanel;
	[SerializeField] Toggle _bgmToggle;
	[SerializeField] Toggle _sfxToggle;
	[SerializeField] Slider _bgmSlider;
	[SerializeField] Slider _sfxSlider;

	private void Awake()
	{
		_soundManager = GameManager.Instance.SoundManager;

		_bgmToggle.onValueChanged.AddListener(SetBgmOn);
		_sfxToggle.onValueChanged.AddListener(SetSfxOn);
		_bgmSlider.onValueChanged.AddListener(SetBgmVolume);
		_sfxSlider.onValueChanged.AddListener(SetSfxVolume);
	}

	private void Start()
	{
		UpdateViews();
	}

	public void UpdateViews()
	{
		_bgmToggle.SetIsOnWithoutNotify(!_soundManager.IsBgmMuted);
		_sfxToggle.SetIsOnWithoutNotify(!_soundManager.IsSfxMuted);
		_bgmSlider.SetValueWithoutNotify(_soundManager.BgmVolume);
		_sfxSlider.SetValueWithoutNotify(_soundManager.SfxVolume);
	}

	public void OpenSettingPanel()
	{
		_settingPanel.SetActive(true);
	}

	public void SetBgmOn(bool isOn)
	{
		_soundManager.SetBgmMute(!isOn);
	}
	public void SetSfxOn(bool isOn)
	{
		_soundManager.SetSfxMute(!isOn);
	}

	public void SetBgmVolume(float volume)
	{
		_soundManager.SetBgmVolume(volume);
	}
	public void SetSfxVolume(float volume)
	{
		_soundManager.SetSfxVolume(volume);
	}
}
