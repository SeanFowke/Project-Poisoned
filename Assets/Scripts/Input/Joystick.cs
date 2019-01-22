using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * this script is used to simplify getting joystick inputs and returning a vector
 * allows for stuff like deadzones
 */
public class Joystick
{
    // stores all axis to get input from
    private string[] xAxisName;
    private string[] yAxisName;

    private float DeadZone;

    public Joystick(string xAxis, string yAxis)
    {
        // defaults to one string and sets it
        xAxisName = new string[1];
        xAxisName[0] = xAxis;

        yAxisName = new string[1];
        yAxisName[0] = yAxis;

        DeadZone = 0.1f;
    }

    public Joystick(string[] xAxis, string[] yAxis)
    {
        // stores the entire axisname
        xAxisName = xAxis;
        yAxisName = yAxis;

        DeadZone = 0.1f;
    }

    public Vector2 get()
    {
        Vector2 axis = Vector2.zero;
        
        for (int i = 0; i < xAxisName.Length; i++)
        {
            //gets axis
            axis.x = Input.GetAxis(xAxisName[i]);
            axis.y = Input.GetAxis(yAxisName[i]);

            //Debug.Log(axis);

            //checks axis to see if it can be returned
            if (Mathf.Abs(axis.x) >= DeadZone || Mathf.Abs(axis.y) >= DeadZone)
            {
                //sets the x value to -1, 1, or 0
                if (axis.x >= DeadZone) { axis.x = 1.0f; }
                else if (axis.x <= -DeadZone) { axis.x = -1.0f; }
                else { axis.x = 0.0f; }

                //sets the y value to -1, 1, or 0
                if (axis.y >= DeadZone) { axis.y = 1.0f; }
                else if (axis.y <= -DeadZone) { axis.y = -1.0f; }
                else { axis.y = 0.0f; }
                

                return axis;
            }
        }

        return Vector2.zero;
    }

    public void setDeadZone(float deadZone_)
    {
        DeadZone = deadZone_;
        if (DeadZone < 0.1f)
        {
            DeadZone = 0.1f;
        }
        else if (DeadZone > 0.9f)
        {
            DeadZone = 0.9f;
        }
    }
}
