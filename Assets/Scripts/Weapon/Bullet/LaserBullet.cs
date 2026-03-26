using UnityEngine;

public class LaserBullet : Bullet
{
    [Header("----- 컴포넌트 -----")]
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] BoxCollider2D _collider2D;

    [Header("----- 리소스 데이터 -----")]
    [SerializeField] float _duration;

    float _timer;

    public void SetRange(float range)
    {
        //사정거리에 맞게 라인 렌더러 설정
        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, Vector3.right * range);

        //사정거리에 맞게 콜라이더 크기 설정
        Vector2 size = _collider2D.size;
        size.x = range;
        _collider2D.size = size;
    }
}
