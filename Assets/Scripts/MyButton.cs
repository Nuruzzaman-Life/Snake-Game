using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
 
public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
 
    public string axis = "Horizontal";
    public float value;
    public bool buttonPressed;
    InputManager _inputManager;

    private void Start() {
        _inputManager = FindObjectOfType<InputManager>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
        
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(_inputManager.ResetValues());
        buttonPressed = false;
    }
    private void Update() {
        if(buttonPressed)
        {
           if (axis == "Horizontal")
           {
               _inputManager.Horizontal = value;
               _inputManager.Vertical = 0;
           }
           else if(axis == "Vertical")
           {
               _inputManager.Vertical = value;
               _inputManager.Horizontal = 0;
           }
        }
        else if(!buttonPressed)
        {
            //inputManager.ResteValues();
        }
    }
}