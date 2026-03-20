using UnityEngine;

/// <summary>
/// 체력 회복 업그레이드
/// </summary>
public class Recoverer : MonoBehaviour, IUpgradable
{
	[Header("----- 대상 -----")]
	[SerializeField] HeroModel _heroModel;

	[Header("----- 설정 데이터 -----")]
	[SerializeField] string _upgradeName;
	[SerializeField] string _desc;
    [SerializeField] Sprite _iconSprite;

	public string UpgradeName => _upgradeName;

	public string Desc => _desc;

	public Sprite IconSprite => _iconSprite;

	public bool IsMaxLevel => false;

	public int Level => int.MinValue;

	public void Upgrade()
	{
		_heroModel.Recover();
	}
}
