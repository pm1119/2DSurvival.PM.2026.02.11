using UnityEngine;

public class PlaySetting : MonoBehaviour
{
    [Header("----- 선택한 플레이어 캐릭터 데이터(읽기 전용) -----")]
    [SerializeField] HeroData _selectedHero;

    public HeroData SelectedHero => _selectedHero;

    public void SetSelectedHero(HeroData selectedHero)
    {
        _selectedHero = selectedHero;
    }
}
