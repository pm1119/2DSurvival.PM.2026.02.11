using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] SpriteRenderer _renderer;

    public void ChangeColor(Color color)
    {
        _renderer.color = color;
    }
}
