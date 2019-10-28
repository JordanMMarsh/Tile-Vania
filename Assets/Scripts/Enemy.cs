using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] float enemyMoveSpeed = 2f;

    Rigidbody2D myRigidBody;
    CapsuleCollider2D enemyCollider2D;
    bool facingRight = true;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        enemyCollider2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update () {
        Move();
	}

    private void Move()
    {
        if (enemyCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (facingRight)
            {
                myRigidBody.velocity = new Vector2(enemyMoveSpeed, myRigidBody.velocity.y);
            }
            else
            {
                myRigidBody.velocity = new Vector2(-enemyMoveSpeed, myRigidBody.velocity.y);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var facing = 1;
        if (facingRight) { facing = -1; }
        transform.localScale = new Vector2(facing, 1f);
        facingRight = !facingRight;
    }
}
