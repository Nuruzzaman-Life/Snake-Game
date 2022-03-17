using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
 
public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
 
    public string axis = "Horizontal";
    public float value;
    public bool buttonPressed;
    InputManager inputManager;

    private void Start() {
        inputManager = FindObjectOfType<InputManager>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
        
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
        inputManager.ResteValues();
    }
    private void Update() {
        if(buttonPressed)
        {
           if (axis == "Horizontal")
            {
                inputManager.Horizontal = value;
                inputManager.Vertical = 0;
            }
            else if(axis == "Vertical")
            {
                inputManager.Vertical = value;
                inputManager.Horizontal = 0;
            }
        }
        else if(!buttonPressed)
        {
            //inputManager.ResteValues();
        }
    }
}