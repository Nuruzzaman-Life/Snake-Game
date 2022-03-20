using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RespawnSystem : MonoBehaviour
{
    public Transform targetSpawnBorder;
    public float offset;
    public bool changeX = false;
    public bool changeY = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            
            //Debug.Log("collison hoise");
            if(changeX)
            {
                other.gameObject.transform.position = new Vector2 (targetSpawnBorder.position.x + offset, other.gameObject.transform.position.y);
            }
            else if (changeY)
            {
                other.gameObject.transform.position = new Vector2 (other.gameObject.transform.position.x ,targetSpawnBorder.position.y + offset);
            }
        }
    }
}
