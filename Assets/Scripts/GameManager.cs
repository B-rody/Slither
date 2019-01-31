using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameObject snakeHead;
    public GameObject snakeHeadInstance;
    public GameObject gameOverPanel;
    public SpriteRenderer snakeSpriteRenderer;
    public Text scoreText;
    public bool snakeSpawned;
    public int score;

	// Use this for initialization
	void Start () {
        instance = this;
        SpawnSnake();
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void SpawnSnake()
    {
        Vector2 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector2(.5f, .1f));
        spawnPoint = new Vector2(Mathf.RoundToInt(spawnPoint.x), Mathf.RoundToInt(spawnPoint.y));
        snakeHeadInstance = Instantiate(snakeHead, spawnPoint, Quaternion.identity);
        snakeSpriteRenderer = snakeHeadInstance.GetComponent<SpriteRenderer>();
        snakeSpawned = true;
    }

    public void EatFood(int points)
    {
        score += points;
        SnakeController.instance.EatFood();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        scoreText.text = score.ToString();
        gameOverPanel.SetActive(true);
        snakeSpawned = false;
    }

    //Need to reset snakecontroller deltas so you can move each direction on start
    public void PlayAgain()
    {
        Destroy(FoodSpawner.instance.currentFood);
        SnakeController.instance.Die();
        SpawnSnake();
        gameOverPanel.SetActive(false);
        score = 0;
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
