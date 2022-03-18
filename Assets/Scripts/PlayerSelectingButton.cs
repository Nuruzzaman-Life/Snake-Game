using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
 
public class PlayerSelectingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
 
    public GameObject player;
    public bool buttonPressed;
    PlayerSelector PlayerSelector;

    private void Start() {
        PlayerSelector = FindObjectOfType<PlayerSelector>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!buttonPressed )
        {
            buttonPressed = true;
            PlayerSelector.ResetPlayersControl();
            player.GetComponent<PlayerController>()._canControl = true;
            player.GetComponent<PlayerController>().selectionIndicator.SetActive(true);

        }
       
        
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
        
    }
    
}