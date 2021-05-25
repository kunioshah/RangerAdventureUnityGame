using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour
{
    public int EnemySpeed;
    public int xMoveDirection;
    [SerializeField] public float distanceToSide;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (rigidbody != null)
        {
            rigidbody.velocity = new Vector2(xMoveDirection * EnemySpeed, 0) ;
        }
    }

    void FlipPlayer()
    {
        if (xMoveDirection > 0)
        {
            var scaleX = gameObject.transform.localScale.x;
            gameObject.transform.localScale = new Vector3(scaleX * -1, 3, 1);
            xMoveDirection = -1;

        } else {
            var scaleX = gameObject.transform.localScale.x;
            gameObject.transform.localScale = new Vector3(scaleX * -1, 3, 1);
            xMoveDirection = 1;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().PlayerDeath();
            Debug.Log("collided with player");
        }
    }
    public void Crush(float x = -3.75f)
    {
        var bones = GameObject.FindGameObjectWithTag("Bones");
        bones.transform.position = new Vector3(gameObject.transform.position.x, x);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        FlipPlayer();
    }
}
