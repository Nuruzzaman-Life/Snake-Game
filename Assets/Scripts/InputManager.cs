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
    public void SetValueToAxis(string axisName, float value)
    {
        if (axisName == "Horizontal")
        {
            Horizontal = value;
        }
        else if(axisName == "Vertical")
        {
            Vertical = value;
        }
    }
    public void ResteValuesAfterWait()
    {
        
        Horizontal = 0;
        Vertical = 0;
    }

    public IEnumerator ResetValues()
    {
        yield return new WaitForSeconds(0.2f);
        ResteValuesAfterWait();
    }
}
