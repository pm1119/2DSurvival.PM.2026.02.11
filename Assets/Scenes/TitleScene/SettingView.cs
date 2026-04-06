using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingView : MonoBehaviour
{
    [Header("----- ÄÄÆ÷³ÍÆ® -----")]
    [SerializeField] Slider _bgmBar;
    [SerializeField] Slider _sfxBar;

    public event UnityAction<Slider> OnBgmVolume;

    public event UnityAction<Slider> OnSfxVolume;

	public void UpdateBgmVolume()
    {
        OnBgmVolume?.Invoke(_bgmBar);
    }

    public void UpdateSfxVolume()
    {
        OnSfxVolume?.Invoke(_sfxBar);
    }
}
