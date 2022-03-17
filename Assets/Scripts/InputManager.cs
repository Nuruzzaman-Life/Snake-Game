using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float Horizontal = 0;
    public float Vertical = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetValueToAxis(string axisname, float value)
    {
        if (axisname == "Horizontal")
        {
            Horizontal = value;
        }
        else if(axisname == "Vertical")
        {
            Vertical = value;
        }
    }
    public void ResteValues()
    {
        Horizontal = 0;
        Vertical = 0;
    }
}
