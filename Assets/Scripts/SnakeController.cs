using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    
    public float MoveSpeed = 5f;
    public float SteerSpeed = 100f;
    public float BodySpeed = 10f;
    public int Gap = 5;
    [SerializeField] private float _bodyGrowthIntarval = 3f;

    public GameObject BodyPrefab;
    private List<GameObject> _bodyParts = new List<GameObject>();
    private List<Vector3> _positionHistory = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move forward
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if(h > 0)
        {
            transform.position += transform.right * MoveSpeed * Time.deltaTime;
            transform.Rotate(Vector3.right * SteerSpeed * Time.deltaTime);
        }
        else if(h < 0)
        {
            transform.position += transform.right * -MoveSpeed * Time.deltaTime;
        }
        if(v > 0)
        {
            transform.position += transform.up * MoveSpeed * Time.deltaTime;
        }
        else if(v < 0)
        {
            transform.position += transform.up * -MoveSpeed * Time.deltaTime;
        }
        
        
        //steer
        float steerDirection = v;
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);

        SnakeLikeMovement();
        AutomaticBodyGrowth();
    }

    private void SnakeLikeMovement ()
    {
        //store position history
        _positionHistory.Insert(0, transform.position);

        //move body parts
        int index = 0;
        foreach (var body in _bodyParts)
        {
            index++;
            Vector3 point = _positionHistory[Mathf.Min(index * Gap , _positionHistory.Count -1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
            body.transform.LookAt(point);

        }
    }

    private void GrowBody()
    {
        GameObject body = Instantiate (BodyPrefab);
        _bodyParts.Add(body);
    }

    float _nextBodyGrowthTime;
    private void AutomaticBodyGrowth()
    {
        if(Time.time >= _nextBodyGrowthTime)
        {
            GrowBody();
            _nextBodyGrowthTime += _bodyGrowthIntarval;
        }
    }
}
