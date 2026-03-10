using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WarriorHpBar : MonoBehaviour
{
    [SerializeField] Image _image;

    public void UpdateHpBar(float currentHp, float maxHp)
    {
        _image.fillAmount = currentHp / maxHp;
    }
}
