using TMPro;
using UnityEngine;

public class StatusView : MonoBehaviour
{
    [Header("----- ÄÄÆ÷³ÍÆ® -----")]
    [SerializeField] TMP_Text _killCount;

    public void KillCountText(int count)
    {
        _killCount.text = $"{count + 1}";
    }
}