using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


	// Use this for initialization
	void Start () {
        // Place camera in a position so each a snake piece fits perfectly into a rounded world unit point.
        // For example, x:0,y:0 would fit one snake unit perfectly, so would x:0,y:1 etc.
        float camPosX = (Camera.main.orthographicSize * Camera.main.aspect) - (GameManager.instance.snakeSpriteRenderer.bounds.size.x / 2);
        float camPosY = Camera.main.orthographicSize - (GameManager.instance.snakeSpriteRenderer.bounds.size.x / 2);
        transform.position = new Vector3(camPosX, camPosY, -10f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Snake Head")
        {
            GameManager.instance.GameOver();
        }
    }
}
