using UnityEngine;

public class BlackWhiteColorChanger : ColorChanger
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			ChangeColor(Color.white);
		}
		else if (Input.GetKeyDown(KeyCode.B))
		{
			ChangeColor(Color.black);
		}
	}
}
