using UnityEngine;

public class RGBColorChanger : ColorChanger
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeColor(Color.red);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeColor(Color.green);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeColor(Color.blue);
        }
    }
}
