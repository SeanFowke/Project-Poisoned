using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * this script is used to simplify getting button inputs from an axis
 * 
 */
public class Button
{
    private string[] B_AxisName;
    private bool B_LastButtonPress;
    private bool B_ButtonDown;
    private bool B_ButtonUp;
    
    public Button(string[] axisNames)
    {
        B_AxisName = axisNames;
        B_LastButtonPress = false;
        B_ButtonDown = false;
        B_ButtonUp = false;
    }

    public Button(string axisName)
    {
        B_AxisName = new string[1];
        B_AxisName[0] = axisName;
        B_LastButtonPress = false;
        B_ButtonDown = false;
        B_ButtonUp = false;
    }

    public void update()
    {
        bool currentPress = false;
        for (int i = 0; i < B_AxisName.Length; i++)
        {
            currentPress |= Input.GetAxis(B_AxisName[i]) != 0.0f;
        }

        B_ButtonDown = false;
        B_ButtonUp = false;
        
        if (B_LastButtonPress && !currentPress)
        {
            B_ButtonUp = true;
        }
        else if (!B_LastButtonPress && currentPress)
        {
            B_ButtonDown = true;
        }

        B_LastButtonPress = currentPress;
    }

    public bool getHold()
    {
        return B_LastButtonPress;
    }

    public bool getUp()
    {
        return B_ButtonUp;
    }

    public bool getDown()
    {
        return B_ButtonDown;
    }
}
