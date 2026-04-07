using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingView : MonoBehaviour
{
    SoundManager _soundManager;

    [Header("----- ÄÄÆ÷³ÍÆ® -----")]
    [SerializeField] Slider _bgmBar;
    [SerializeField] Slider _sfxBar;

	private void Start()
	{
        _soundManager = GameManager.Instance.SoundManager;
	}

	public void UpdateVolume(float bgm, float sfx)
    {
        _bgmBar.value = bgm;
        _sfxBar.value = sfx;

        _soundManager.SetBgmVolume(_bgmBar.value);
        _soundManager.SetSfxVolume(_sfxBar.value);
	}
}
