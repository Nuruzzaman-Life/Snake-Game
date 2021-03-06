using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _bodySize = 5;
    [SerializeField] private GameObject _bodyPrefab;
    [SerializeField] private float _bodyMovingSpeed;
    [SerializeField] private int _bodyGap = 10;
    private List<GameObject> _bodyParts = new List<GameObject>();
    private List<Vector3> _positionHistory = new List<Vector3>();

    [HideInInspector] public GameObject _myLine;
    Vector3 [] checkpoints;
    // Start is called before the first frame update
     public float movingSpeed = 2f;
     public float rotationSpeed;
    //public GameObject[] checkpoints;
    public float _minDistance = 0.1f; //on which distance you want to switch to the next waypoint
    Vector3 _direction;
    Vector3 _targetPosition ;
    [HideInInspector] public bool _canMove = false;
    [HideInInspector] public bool _canControl = false;
    public GameObject selectionIndicator;
    public int foodCollectionPoint = 5;
    public int collisionDeductionPoint = 3;
    InputManager inputManager;
    
    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        _targetPosition = new Vector3 (transform.position.x, transform.position.y +1000, transform.position.z);
        GrowBody();
        GrowBody();
        GrowBody();
        GrowBody();
        _canMove = true;
        //BringPlayerUp();

    }

    // Update is called once per frame
     void FixedUpdate ()
     {
         if(_canMove)
         {
            _direction = Vector3.zero;
            //get the vector from your position to current waypoint
            _targetPosition = CalculateTargetPosition();
            _direction = _targetPosition - transform.position;
            //check our distance to the current waypoint, Are we near enough?
            
            //_direction = _direction.normalized;
            //Vector3 dir = _direction;
            float step =  movingSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);
            //transform.position = Vector3.Lerp(transform.position, _targetPosition, 0.2f);
            Vector3 dir = _targetPosition - transform.position;
            //get the angle from current direction facing to desired target
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //set the angle into a quaternion + sprite offset depending on initial sprite facing direction
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90 ));
            //Roatate current game object to face the target using a slerp function which adds some smoothing to the move
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            //GetComponent<Rigidbody2D>().velocity = new Vector2(_direction.x * actualSpeed, _direction.y * actualSpeed);
            ControlBodyParts();
         }
         
     }

     // private void FixedUpdate()
     // {
     //     if (_canMove)
     //     {
     //         //ControlBodyParts();
     //     }
     // }


     private void ControlBodyParts()
    {
        _positionHistory.Insert(0, transform.position);

        int index = 1;
        foreach (var body in _bodyParts)
        {
            Vector3 point = _positionHistory[Mathf.Min(index * _bodyGap, _positionHistory.Count -1)];
            //point = transform.position;
            //Debug.Log("point "+ point + " body " + body.transform.position);
            Vector3 moveDirection = (point - body.transform.position);
            if (Vector3.Distance(point, body.transform.position) > 0.8 )
            {
                Debug.Log("dure gase");
                body.SetActive(false);
            }
            else
            {
                body.SetActive(true);
            }
            
            //Debug.Log(moveDirection);
            body.transform.position += moveDirection *_bodyMovingSpeed * Time.deltaTime;
            //body.transform.position = point;
            // body.transform.LookAt(point);
            //body.transform.right = moveDirection;
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            body.transform.rotation = Quaternion.AngleAxis(angle -90, Vector3.forward);
            
            

            index++;
        }
    }
    public void GrowBody ()
    {
        for (int i = 0; i < _bodySize; i++)
        {
            UnitBodyGrow();
        }
        
    }
    void UnitBodyGrow()
    {
        Vector3 rotation;
            if(_bodyParts.Count > 0)
            {
                rotation = new Vector3 (0, 0, _bodyParts[_bodyParts.Count -1].transform.rotation.eulerAngles.z);
                
            }else
            {
                rotation = new Vector3 (0, 0, transform.rotation.eulerAngles.z);
            }
            GameObject body = Instantiate (_bodyPrefab, transform.position,  Quaternion.Euler(rotation));
            //GameObject body = Instantiate(_bodyPrefab);
            //Debug.Log ("rotation = " + rotation);
            _bodyParts.Add(body);
    }

    public void StartMoving(){
        checkpoints = new Vector3 [_myLine.GetComponent<LineRenderer>().positionCount ];
        _myLine.GetComponent<LineRenderer>().GetPositions (checkpoints);
        _canMove = true;
        var color = _myLine.GetComponent<LineRenderer>().endColor;
        Debug.Log(color+ "Player is moving");
    }
    float horizontal;
    float vertical;
    private float targetDistance = 100;
    private Vector3 CalculateTargetPosition()
    {
        horizontal = inputManager.Horizontal;
        vertical = inputManager.Vertical;
        //Debug.Log("values " +horizontal + " "+ vertical);
        

        if(_canControl)
        {
            if(horizontal > 0)
            {
                return new Vector3 (transform.position.x + targetDistance, transform.position.y, transform.position.z);
            }
            else if(horizontal < 0)
            {
                return new Vector3 (transform.position.x -targetDistance, transform.position.y, transform.position.z);
            }
            if(vertical > 0)
            {
                return new Vector3 (transform.position.x, transform.position.y +targetDistance, transform.position.z);
            }
            else if(vertical < 0)
            {
                return new Vector3 (transform.position.x, transform.position.y -targetDistance, transform.position.z);
            }
            else
            {
                return _targetPosition;
            }
        }
        else
        {
            return _targetPosition;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.name);
        
    }

    void FoodCollision(Collider2D other)
    {
        if(other.gameObject.CompareTag("Blue Food"))
        {
            if (_bodyPrefab.name.StartsWith("Blue"))
            {
                Destroy(other.gameObject);
                FindObjectOfType<UIManager>().AddPoints(foodCollectionPoint);
                FindObjectOfType<FoodManager>().ReSpawnFood("Blue");
                GrowBody();
            }
            else
            {
                //ControlGameOver();
            }
        }
        else if(other.gameObject.CompareTag("Red Food"))
        {
            if (_bodyPrefab.name.StartsWith("Red"))
            {
                Destroy(other.gameObject);
                FindObjectOfType<UIManager>().AddPoints(foodCollectionPoint);
                FindObjectOfType<FoodManager>().ReSpawnFood("Red");
                GrowBody();
            }
            else
            {
                //ControlGameOver();
            }
        }
        else if(other.gameObject.CompareTag("Green Food"))
        {
            if (_bodyPrefab.name.StartsWith("Green"))
            {
                Destroy(other.gameObject);
                FindObjectOfType<UIManager>().AddPoints(foodCollectionPoint);
                FindObjectOfType<FoodManager>().ReSpawnFood("Green");
                GrowBody();
            }
            else
            {
                //ControlGameOver();
            }
        }
    }

    [SerializeField] private float collisionCheckingIntarval = 1;
    private float nextCollisonCheckTime;
    private void OnTriggerEnter2D(Collider2D other) {
        FoodCollision(other);
        var obejct = other.gameObject;
        //Debug.Log("Trigger " +other.gameObject.name);
        if (Time.time >= nextCollisonCheckTime)
        {
            if(obejct.CompareTag("Player") || obejct.CompareTag("Body") )
            {
                FindObjectOfType<UIManager>().DeductPoints(collisionDeductionPoint);
                Debug.Log("Game Over");
                //FindObjectOfType<UIController>().OpenGameOverCanvas();
            }

            nextCollisonCheckTime = Time.time + collisionCheckingIntarval;
        }
        
    }

    void ControlGameOver()
    {
        FindObjectOfType<UIManager>().GameOver();
    }

    void BringPlayerUp()
    {
        transform.position = new Vector3 (transform.position.x, transform.position.y, -1);
    }
}
