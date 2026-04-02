using UnityEngine;
using UnityEngine.U2D.Animation;

/// <summary>
/// 주인공(플레이어)의 설정 데이터
/// </summary>
[CreateAssetMenu(menuName = "GameSettings/HeroData")]
public class HeroData : ScriptableObject
{
    [Header("----- 기본 능력치 -----")]
    [SerializeField] float _baseHp;                 //기본 체력
    [SerializeField] float _speed;                  //기본 이동 속력
    [SerializeField] float _baseExp;                //기본 경험치
    [SerializeField] float _expIncrementRate;       //경험치 증가 비율

    [Header("----- UI -----")]
    [SerializeField] Sprite _sprite;                //UI 아이콘 표시용 스프라이트
    [SerializeField] string _name;                  //이름
    [SerializeField] WeaponData _startWeaponData;   //시작 무기 데이터
    [SerializeField] Color _color;                  //색상

    [Header("----- 렌더러 -----")]
    [SerializeField] SpriteLibraryAsset _libraryAsset;       //스프라이트 라이브러리 에셋

    [Header("----- 해금 -----")]
    [SerializeField] bool _defaultUnlocked;         //기본 제공 여부
    [SerializeField] ChallangeType _unlockChallangeType;            //해금 도전 과제 종류
    [SerializeField] string _unlockGuide;           //해금 가이드

    public float BaseHp => _baseHp;

    public float Speed => _speed;

    public Sprite Sprite => _sprite;

    public string Name => _name;

    public WeaponData StartWeaponData => _startWeaponData;

    public Color Color => _color;

    public SpriteLibraryAsset LibraryAsset => _libraryAsset;

    public bool DefaultUnlocked => _defaultUnlocked;

    public ChallangeType UnlockChallangeType => _unlockChallangeType;

    public string UnlockGuide => _unlockGuide;

    /// <summary>
    /// 레벨업에 필요한 경험치 양을 반환하는 함수
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public float GetExp(int level)
    {
        if (level <= 0)
            return _baseExp;
        return _baseExp * Mathf.Pow(_expIncrementRate, level);
    }
}
