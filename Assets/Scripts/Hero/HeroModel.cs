using UnityEngine;
using UnityEngine.Events;

public class HeroModel : CharacterModel
{
	[Header("----- 경험치 -----")]
	[SerializeField] float _currentExp;
	[SerializeField] float _maxExp;
	[SerializeField] int _level;

	/// <summary>
	/// 경험치 변경 이벤트
	/// </summary>
	public event UnityAction<float, float> OnExpChanged;

	/// <summary>
	/// 레벨 변경 이벤트(이전 레벨, 현재 레벨)
	/// </summary>
	public event UnityAction<int, int> OnLevelChanged;

	/// <summary>
	/// 경험치를 획득하는 함수
	/// </summary>
	/// <param name="amount"></param>
	public void AddExp(float amount)
	{
		//이전 레벨
		int prelevel = _level;

		//경험치 증가
		_currentExp += amount;

		//경험치가 다 찼으면
		while (_currentExp >= _maxExp)
		{
			_currentExp -= _maxExp;

			//레벨업
			_level++;
		}

		//레벨 변경 이벤트 발행
		//레벨이 올랐을 경우에만 이벤트 발행
		if (_level > prelevel)
			OnLevelChanged?.Invoke(prelevel ,_level);

		//경험치 변경 이벤트 발행
		OnExpChanged?.Invoke(_currentExp, _maxExp);
	}
}
