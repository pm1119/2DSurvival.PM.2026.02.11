using UnityEngine;

public class EventTest : MonoBehaviour
{
    public void Attack()
    {
        Debug.Log("플레이어 공격");
    }

    public void UseSkill(int skillIndex)
    {
		Debug.Log($"플레이어 {skillIndex}번 스킬 사용");
	}
}
