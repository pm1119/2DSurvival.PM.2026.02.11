using UnityEngine;

/// <summary>
/// 자석 아이템, 플레이어 캐릭터가 획득 시 자석 효과 발동
/// </summary>
public class MagnetItem : MonoBehaviour
{
    EnemySpawner _enemySpawner;

	public void Initialize(EnemySpawner enemySpawner)
	{
		_enemySpawner = enemySpawner;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") == true)
		{
			_enemySpawner.StartMagnetEffect();

			//풀로 되돌아가거나 파괴
			gameObject.DestroyOrReturnPool();
		}
	}
}
