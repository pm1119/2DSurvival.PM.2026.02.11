using UnityEngine;

/// <summary>
/// 무기 설정 데이터 
/// 무기 하나의 레벨별 스탯 데이터
/// </summary>
[CreateAssetMenu(menuName = "GameSettings/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] int _index;                    //무기 번호
    [SerializeField] string _weaponName;            //무기 이름
    [TextArea(3, 5)] [SerializeField] string _desc;                  //무기 설명
    [SerializeField] Sprite _iconSprite;            //아이콘 스프라이트
    [SerializeField] WeaponStatData[] _statDatas;   //레벨별 스탯 데이터

    public int Index => _index;

    public string Name => _weaponName;

    public string Desc => _desc;

    public Sprite IconSprite => _iconSprite;

    public int MaxLevel => _statDatas.Length - 1;
    
    /// <summary>
    /// 레벨에 따른 스탯 데이터를 반환하는 함수
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public WeaponStatData GetStatData(int level)
    {
        level = Mathf.Clamp(level, 0, _statDatas.Length - 1);
        return _statDatas[level];
    }
}
