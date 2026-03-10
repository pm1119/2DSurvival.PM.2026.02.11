using UnityEngine;

public class AnyColorChanger : ColorChanger
{
	[SerializeField] Color _color;

	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            ChangeColor(_color);
        }
    }
}
