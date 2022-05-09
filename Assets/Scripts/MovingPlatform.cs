using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public float minX, minY, maxX, maxY;
    public float velocityX, velocityY;
    public bool moveWithPlayer;
    public bool movesOnlyWithPlayer;
    public bool stop;
    public Vector2 stopPosition;
    public bool flipSprite;
    public bool flipFacingLeft;
    public bool flipFacingRight;
    private bool move;
    private bool moveLeft;
    private bool moveRight;
    private bool moveUp;
    private bool moveDown;
    private GameObject P1;
    private Animator animator;
    private bool thereIsAnAnimator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "kat")
        {
            move = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "kat" && movesOnlyWithPlayer)
        {
            move = false;
        }
    }

    void Start () {
        Physics2D.IgnoreLayerCollision(8, 8, true);
        P1 = GameObject.Find("P1 position");
        if (this.GetComponent<Animator>() == true)
        {
            animator = this.GetComponent<Animator>();
            thereIsAnAnimator = true;
        }
    }
	
    void Move()
    {        
        if (this.transform.position.x <= minX)
        {
            if (flipSprite)
            {
                if (flipFacingLeft)
                {
                    this.GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (flipFacingRight)
                {
                    this.GetComponent<SpriteRenderer>().flipX = false;
                }                
            }
            moveLeft = false;
            moveRight = true;
        }
        else if (this.transform.position.x >= maxX)
        {
            if (flipSprite)
            {
                if (flipFacingLeft)
                {
                    this.GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (flipFacingRight)
                {
                    this.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            moveRight = false;
            moveLeft = true;
        }
        if (moveRight)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(
            velocityX,
            this.GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (moveLeft)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(
            - velocityX,
            this.GetComponent<Rigidbody2D>().velocity.y);
        }
        if (this.transform.position.y <= minY)
        {
            moveDown = false;
            moveUp = true;
        }
        else if (this.transform.position.y >= maxY)
        {
            moveUp = false;
            moveDown = true;
        }
        if (moveUp)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(
            this.GetComponent<Rigidbody2D>().velocity.x,
            velocityY);
        }
        else if (moveDown)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(
            this.GetComponent<Rigidbody2D>().velocity.x,
            - velocityY);
        }
    }
	
	void Update () {
        if (P1.transform.localScale.x == 1)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            if (thereIsAnAnimator == true)
            {
                animator.StartPlayback();
            }
        }
        else
        {
            if (thereIsAnAnimator == true)
            {
                animator.StopPlayback();
            }
            if (stop == false)
            {
                if (moveWithPlayer == false && movesOnlyWithPlayer == false)
                {
                    Move();
                }
                else
                {
                    if (move)
                    {
                        Move();
                    }
                    else
                    {
                        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                }
            }
            else
            {
                if (this.transform.position.x >= stopPosition.x || this.transform.position.y <= stopPosition.y)
                {
                    if (this.transform.position.x >= stopPosition.x)
                    {
                        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
                    }
                    else if (this.transform.position.y <= stopPosition.y)
                    {
                        this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 0);
                    }
                    else if (this.transform.position.x >= stopPosition.x && this.transform.position.y <= stopPosition.y)
                    {
                        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                        this.transform.position = new Vector3(stopPosition.x, stopPosition.y, this.transform.position.z);
                    }
                }
                else
                {
                    if (moveWithPlayer == false && movesOnlyWithPlayer == false)
                    {
                        Move();
                    }
                    else
                    {
                        if (move)
                        {
                            Move();
                        }
                        else
                        {
                            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                        }
                    }
                }
            }
        }
    }
}
