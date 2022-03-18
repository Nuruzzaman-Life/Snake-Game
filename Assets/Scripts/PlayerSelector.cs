using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public GameObject [] players;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ResetPlayersControl()
    {
        foreach (var player in players)
        {
            player.GetComponent<PlayerController>()._canControl = false;
            player.GetComponent<PlayerController>().selectionIndicator.SetActive(false);
        }
    }
}
