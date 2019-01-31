using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {

    public static SnakeController instance;
    public GameObject snakePiecePrefab;
    public float timeBetweenMoves;

    private float timeBetweenMovesCounter;
    private List<GameObject> theSnake = new List<GameObject>();
    private bool ateFood;
    private bool moving = false;
    private bool moveMadeThisTurn = false; // Prevents user from abusing update called per frame rather than independent of movement time. Allows one valid key input per timeBetweenMoves.
    private bool gotSnakeHead = false;
    private float deltaX = 0;
    private float deltaY = 0;

    // Use this for initialization
    void Start () {
        instance = this;
        if (GameManager.instance.snakeSpawned)
        {
            theSnake.Add(GameManager.instance.snakeHeadInstance);
            gotSnakeHead = true;
        }
        timeBetweenMovesCounter = timeBetweenMoves;
    }
	
	// Update is called once per frame
	void Update () {
        if (!gotSnakeHead && GameManager.instance.snakeSpawned)
        {
            theSnake.Add(GameManager.instance.snakeHeadInstance);
            gotSnakeHead = true;
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Horizontal") != (deltaX * -1) && !moveMadeThisTurn)
            {
                deltaX = Input.GetAxisRaw("Horizontal");
                deltaY = 0f;
                if (!moving)
                {
                    moving = true;
                }
                moveMadeThisTurn = true;
            }
            else if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Vertical") != (deltaY * -1) && !moveMadeThisTurn)
            {
                deltaY = Input.GetAxisRaw("Vertical");
                deltaX = 0f;
                if (!moving)
                {
                    moving = true;
                }
                moveMadeThisTurn = true;
            }
            if (moving)
            {
                timeBetweenMovesCounter -= Time.deltaTime;

                if (timeBetweenMovesCounter <= 0)
                {
                    Vector2 posToMoveTo = theSnake[0].transform.position;
                    Vector2 previousPiecePos;
                    theSnake[0].transform.position = SnapToGrid(posToMoveTo);
                    timeBetweenMovesCounter += timeBetweenMoves;
                    moveMadeThisTurn = false;

                    if (ateFood)
                    {
                        theSnake.Insert(1, Instantiate(snakePiecePrefab, posToMoveTo, Quaternion.identity));
                        ateFood = false;
                    }
                    else
                    {
                        for (int i = 1; i < theSnake.Count; i++)
                        {
                            previousPiecePos = theSnake[i].transform.position;
                            theSnake[i].transform.position = posToMoveTo;
                            posToMoveTo = previousPiecePos;
                        }
                    }
                }
            }
        }
    }

    private Vector2 SnapToGrid(Vector2 currentPos)
    {
        return new Vector2((Mathf.RoundToInt(currentPos.x + (deltaX * GameManager.instance.snakeSpriteRenderer.bounds.size.x))), Mathf.RoundToInt(currentPos.y + (deltaY * GameManager.instance.snakeSpriteRenderer.bounds.size.y)));  
    }

    public void EatFood()
    {
        ateFood = true;
    }

    public void Die()
    {
        foreach (GameObject snakePiece in this.theSnake)
        {
            Destroy(snakePiece);
        }
        theSnake = new List<GameObject>();
        moving = false;
        gotSnakeHead = false;
    }

}
