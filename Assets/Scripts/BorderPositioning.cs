using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderPositioning : MonoBehaviour
{
    private Vector2 _screenBounds;

    public GameObject leftBorder;

    public GameObject rightBorder;

    public GameObject topBorder;
    
    public GameObject bottomBorder;

    public float offset = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Debug.Log("scrren bounds "+ _screenBounds);
        leftBorder.transform.position = new Vector3(-_screenBounds.x - offset, 0, 0);
        rightBorder.transform.position = new Vector3(_screenBounds.x + offset, 0, 0);
        topBorder.transform.position = new Vector3(0, _screenBounds.y + offset, 0);
        bottomBorder.transform.position = new Vector3(0, -_screenBounds.y - offset, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
