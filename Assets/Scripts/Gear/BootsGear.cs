using UnityEngine;

/// <summary>
/// 부츠 장비: 주인공 이동 속력을 증가시키는 장비
/// </summary>
public class BootsGear : Gear
{
	protected override void Apply()
	{
		_heroModel.SetSpeedMultiplier(_value);
	}
}
