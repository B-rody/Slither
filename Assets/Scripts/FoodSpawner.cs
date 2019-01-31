using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {

    public static FoodSpawner instance;
    public GameObject foodPrefab;
    public GameObject currentFood;
    private float xMin;
    private float yMin;
    private float xMax;
    private float yMax;

	// Use this for initialization
	void Start () {
        instance = this;
        SetUpBoundaries();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentFood == null && GameManager.instance.snakeSpawned)
        {
            SpawnFood();
        }
	}

    private void SetUpBoundaries()
    {
        Camera gameCamera = Camera.main;
        SpriteRenderer foodSprite = foodPrefab.GetComponent<SpriteRenderer>();
        xMin = gameCamera.ViewportToWorldPoint(new Vector2(0, 0)).x + foodSprite.bounds.size.x / 2;
        xMax = gameCamera.ViewportToWorldPoint(new Vector2(1, 0)).x - foodSprite.bounds.size.x / 2;
        yMin = gameCamera.ViewportToWorldPoint(new Vector2(0, 0)).y + +foodSprite.bounds.size.y / 2;
        yMax = gameCamera.ViewportToWorldPoint(new Vector2(0, 1)).y - +foodSprite.bounds.size.y / 2;
    }

    //Need to verify each location of the snake pieces rather than just the head.
    private void SpawnFood()
    {
        bool foodSpawned = false;
        while (!foodSpawned)
        {
            Vector3 potentialSpawnLocation = new Vector3(Mathf.RoundToInt(Random.Range(xMin, xMax)), Mathf.RoundToInt(Random.Range(yMin, yMax)));
            if (potentialSpawnLocation != GameManager.instance.snakeHeadInstance.transform.position)
            {
                currentFood = Instantiate(foodPrefab, potentialSpawnLocation, Quaternion.identity);
                foodSpawned = true;
            }
        }
    }
}
