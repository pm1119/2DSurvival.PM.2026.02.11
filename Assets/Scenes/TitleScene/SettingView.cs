using UnityEngine;
using UnityEngine.UI;

public class SettingView : MonoBehaviour
{
    [Header("----- ÄÄÆ÷³ÍÆ® -----")]
    [SerializeField] Slider _bgmBar;
    [SerializeField] Slider _sfxBar;

    public void UpdateBgmVolume(float volume)
    {
        _bgmBar.value = volume;
    }

    public void UpdateSfxVolume(float volume)
    {
        _sfxBar.value = volume;
    }
}
