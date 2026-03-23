using UnityEngine;

public class HealthItem : MonoBehaviour
{
	[SerializeField] HeroModel _heroModel;

    [Header("----- 체력 보상 -----")]
    [SerializeField] float _hpReward;

	public void Initialize(HeroModel heroModel)
	{
		_heroModel = heroModel;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") == true)
		{
			_heroModel.HpRecover(_hpReward);
			Destroy(gameObject);
		}
	}
}
