using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float playerSpeed = 6f;
    [SerializeField] float jumpSpeed = 250f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] Vector2 thrownDistance = new Vector2(5f, 5f);
    [SerializeField] Collider2D playerFeet;
    SpriteRenderer playerSprite;
    Animator playerAnimator;
    Rigidbody2D player;
    Collider2D myCollider2D;
    

    float gravityAtStart;
    bool climbing = false;
    bool playerIsAlive = true;

    const string PLAYER_RUNNING_BOOL = "isRunning";
    const string PLAYER_CLIMBING_BOOL = "isClimbing";
    const string PLAYER_ALIVE_BOOL = "isAlive";

    private void Awake()
    {
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
        myCollider2D = GetComponent<Collider2D>();
        gravityAtStart = player.gravityScale;
        Time.timeScale = 1f;
    }
    
   
    void Update () {
        if (playerIsAlive)
        {
            PlayerMovement();
            Jump();
            Climb();
            Die();
        }        
	}

    private void PlayerMovement()
    {
        var movement = Input.GetAxis("Horizontal") * playerSpeed;

        if (movement != 0)
        {
            playerAnimator.SetBool(PLAYER_RUNNING_BOOL, true);
            if (movement > 0)
            {
                playerSprite.flipX = false;
            }
            else
            {
                playerSprite.flipX = true;
            }
        }
        else
        {
            playerAnimator.SetBool(PLAYER_RUNNING_BOOL, false);
        }
        player.velocity = new Vector2(movement, player.velocity.y);
    }

    private void Climb()
    {
        if (!playerFeet.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            player.gravityScale = gravityAtStart;
            climbing = false;
            playerAnimator.SetBool(PLAYER_CLIMBING_BOOL, false);
            return;
        }

        if (Input.GetButton("Vertical"))
        {
            climbing = true;
            player.gravityScale = 0f;
            playerAnimator.SetBool(PLAYER_CLIMBING_BOOL, true);
            playerAnimator.SetFloat("animationSpeed", 1);
            float verticalMovement = Input.GetAxis("Vertical");
            player.velocity = new Vector2(player.velocity.x, verticalMovement * climbSpeed);
        }        
        else
        {
            if (climbing)
            {
                playerAnimator.SetFloat("animationSpeed", 0);
                player.velocity = new Vector2(player.velocity.x, 0);
            }            
        }
    }

    private void Jump()
    {
        if (!playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            player.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    private void Die()
    {
        if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")) && playerIsAlive)
        {
            playerIsAlive = false;
            playerAnimator.SetBool(PLAYER_ALIVE_BOOL, false);
            player.velocity = thrownDistance;
            FindObjectOfType<GameSession>().KillPlayer();
            FindObjectOfType<SceneLoader>().LoadSceneIndex(1);
        }
    }
}
