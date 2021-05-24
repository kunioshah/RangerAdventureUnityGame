using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveNoBounce : MonoBehaviour
{
    public int EnemySpeed;
    public int xMoveDirection;

    private void Start()
    {
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * EnemySpeed;
        //Debug.Log("hit distance is " + hit.distance);
        if (hit.distance < 0.7f)
        {
           // FlipPlayer();
        }
    }

    void FlipPlayer()
    {
        if (xMoveDirection > 0)
        {
            xMoveDirection = -1;
        }
        else
        {
            xMoveDirection = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<Player>().PlayerDeath();
        }
    }
}

