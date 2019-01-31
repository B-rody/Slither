using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public int points;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Snake Head")
        {
            Eat();
        }
    }

    private void Eat()
    {
        FoodSpawner.instance.currentFood = null;
        GameManager.instance.EatFood(points);
        Destroy(gameObject);
    }
}
