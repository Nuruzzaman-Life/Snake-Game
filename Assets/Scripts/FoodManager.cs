using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;

    public GameObject [] foods;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < foods.Length; i++)
        {
            var food = Instantiate(foods[i]);
            food.transform.position = GenerateRandomPos(maxX, minX, maxY, minY);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector2 GenerateRandomPos(float maxX, float minX, float maxY, float minY)
    {
        var x = Random.Range(minX, maxX);
        var y = Random.Range(minY, maxY);
        return new Vector2(x, y);
    }

    public void ReSpawnFood(string foodColor)
    {
        foreach (var food in foods)
        {
            if (food.name.StartsWith(foodColor))
            {
                var newFood = Instantiate(food);
                newFood.transform.position = GenerateRandomPos(maxX, minX, maxY, minY);
            }
        }
    }
}
