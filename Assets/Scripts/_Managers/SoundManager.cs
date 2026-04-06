using UnityEngine;
using UnityEngine.Events;

public enum Bgm
{
    Title,
    Stage0
}

public enum Sfx
{
    Coin,
    Hit
}

public class SoundManager : MonoBehaviour
{
    //PlayerPrefs 키
    const string _bgmVolumeKey = "BgmVolume";
    const string _sfxVolumeKey = "SfxVolume";
    const string _bgmMuteKey = "BgmMute";
    const string _sfxMuteKey = "SfxMute";

    [Header("----- 컴포넌트 -----")]
    [SerializeField] AudioSource _bgmSource;        //배경음 소스
    [SerializeField] AudioSource _sfxSource;        //효과음 소스

    [Header("----- 리소스 -----")]
    [SerializeField] AudioClip[] _bgmClips;          //배경음 클립 배열
    [SerializeField] AudioClip[] _sfxClips;          //효과음 클립 배열

    [Header("----- 런타임 데이터 -----")]
    [SerializeField] float _bgmVolume = 1;
    [SerializeField] float _sfxVolume = 1;

    [SerializeField] bool _isBgmMuted = false;
    [SerializeField] bool _isSfxMuted = false;

    public float BgmVolume => _bgmVolume;

    public float SfxVolume => _sfxVolume;

    public bool IsBgmMuted => _isBgmMuted;

    public bool IsSfxMuted => _isSfxMuted;

	private void Awake()
	{
		LoadSettings();
        //_settingView.Initialize(BgmVolume, SfxVolume);
	}

	/// <summary>
	/// 저장된 볼륨 및 음소거 설정을 불러오는 함수
	/// </summary>
	void LoadSettings()
    {
        _bgmVolume = PlayerPrefs.GetFloat(_bgmVolumeKey, 1f);
        _sfxVolume = PlayerPrefs.GetFloat(_sfxVolumeKey, 1f);

        _isBgmMuted = PlayerPrefs.GetInt(_bgmMuteKey, 0) == 1;
        _isSfxMuted = PlayerPrefs.GetInt(_sfxMuteKey, 0) == 1;

        _bgmSource.volume = _bgmVolume;
        _bgmSource.mute = _isBgmMuted;

        _sfxSource.volume = _sfxVolume;
        _sfxSource.mute = _isSfxMuted;
    }

    /// <summary>
    /// 지정한 배경음악을 재생하는 함수
    /// </summary>
    /// <param name="bgm"></param>
    public void PlayBgm(Bgm bgm)
    {
        int index = (int)bgm;     
        if (index < 0 || index >= _bgmClips.Length)
        {
			Debug.LogWarning($"존재하지 않는 배경음악입니다. Type: {bgm}");
			return;
		}

        _bgmSource.clip = _bgmClips[index];
        _bgmSource.Play();
    }

    /// <summary>
    /// 현재 재생 중인 배경음악을 정지
    /// </summary>
    public void StopBgm()
    {
        _bgmSource.Stop();
    }

	/// <summary>
	/// 현재 재생 중인 배경음악을 일시 정지
	/// </summary>
	public void PauseBgm()
    {
        _bgmSource.Pause();
    }

    /// <summary>
    /// 일시정지된 배경음악을 재개하는 함수
    /// </summary>
    public void UnPauseBgm()
    {
        _bgmSource.UnPause();
    }

	/// <summary>
	/// 지정한 효과음을 재생하는 함수
	/// </summary>
	/// <param name="sfx"></param>
	public void PlaySfx(Sfx sfx)
    {
        int index = (int)sfx;
        if (index < 0 || index >= _sfxClips.Length)
        {
			Debug.LogWarning($"존재하지 않는 효과음입니다. Type: {sfx}");
			return;
		}

        _sfxSource.PlayOneShot(_sfxClips[index]);
    }

    /// <summary>
    /// 지정한 위치에서 효과음을 재생하는 함수
    /// </summary>
    /// <param name="sfx"></param>
    /// <param name="pos"></param>
    public void PlaySfx(Sfx sfx, Vector3 pos)
    {
        //효과음 음소거 여부 체크
        if (_isBgmMuted == true) return;

		int index = (int)sfx;
		if (index < 0 || index >= _sfxClips.Length)
		{
			Debug.LogWarning($"존재하지 않는 효과음입니다. Type: {sfx}");
			return;
		}

        //간단하게 지정된 위치에서 사운드클립을 재생하는 함수
        AudioSource.PlayClipAtPoint( _sfxClips[index], pos, _sfxVolume );
	}

    public void SetBgmVolume(float volume)
    {
        _bgmVolume = volume;

        PlayerPrefs.SetFloat(_bgmVolumeKey, _bgmVolume);
        PlayerPrefs.Save();

        _bgmSource.volume = _bgmVolume;
    }

    public void SetSfxVolume(float volume)
    {
        _sfxVolume = volume;

		PlayerPrefs.SetFloat(_sfxVolumeKey, _sfxVolume);
		PlayerPrefs.Save();

        _sfxSource.volume = _sfxVolume;
	}

	public void SetBgmMute(bool isMuted)
	{
		_isBgmMuted = isMuted;

        PlayerPrefs.SetInt(_bgmMuteKey, _isBgmMuted? 1: 0);
        PlayerPrefs.Save();

        _bgmSource.mute = _isBgmMuted;
	}

    public void SetSfxMute(bool isMute)
    {
        _isSfxMuted = isMute;

		PlayerPrefs.SetInt(_sfxMuteKey, _isSfxMuted ? 1 : 0);
		PlayerPrefs.Save();

        _sfxSource.mute = _isSfxMuted;
	}
}
