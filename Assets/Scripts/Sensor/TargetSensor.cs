using UnityEngine;

/// <summary>
/// 일정 거리 이내의 게임오브젝트들을 감지하는 역할
/// </summary>
public class TargetSensor : MonoBehaviour
{
    [Header("----- 런타임 데이터 -----")]
    [SerializeField] LayerMask _targetLayerMask;        //대상 레이어 마스크

    Collider2D[] _collider2Ds;                          //감지된 콜라이더들 저장용 배열

    /// <summary>
    /// 범위 내 가장 가까운 대상을 찾아 반환하는 함수
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    public Transform GetNearestTarget(float range)
    {
        _collider2Ds = Physics2D.OverlapCircleAll(transform.position, range, _targetLayerMask);

        //감지된 대상이 있는 경우
        if (_collider2Ds.Length > 0)
        {
            Transform target = _collider2Ds[0].transform;
            float minDist = Vector3.Distance(transform.position, target.position);
            for (int i = 1; i < _collider2Ds.Length; i++)
            {
                float dist = Vector3.Distance(transform.position, _collider2Ds[i].transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    target = _collider2Ds[i].transform;
                }
            }
            return target;  
        }
        return null;
    }

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 10);
	}
}
