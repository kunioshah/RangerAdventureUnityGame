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
        RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2(xMoveDirection, 0));
        //Debug.Log(rigidbody2D.velocity);
        if (rigidbody != null)
        {
            rigidbody.velocity = new Vector2(xMoveDirection * EnemySpeed, 0) ;
        }
        Debug.Log("hit distance is " + hit.distance);
        if (hit.distance < 1.8f) {
           // FlipPlayer();
            if (hit.collider.tag == "Player")
            {
                StartCoroutine(Die());
            }
        }
    }

    void FlipPlayer()
    {
        if (xMoveDirection > 0)
        {
            var scaleX = gameObject.transform.localScale.x;
            gameObject.transform.localScale = new Vector3(scaleX * -1, 5, 1);
            xMoveDirection = -1;

        } else {
            var scaleX = gameObject.transform.localScale.x;
            gameObject.transform.localScale = new Vector3(scaleX * -1, 5, 1);
            xMoveDirection = 1;
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Science_Project");
    }

    void EnemyRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left);
        RaycastHit2D hitright = Physics2D.Raycast(transform.position, Vector2.right);
        if (hit.distance < distanceToSide && hit.collider.tag == "Player")
        {
            Debug.Log("player died" + hit.distance + distanceToSide); 
            StartCoroutine(Die());
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("collided with player");
        }
    }
    public void Crush()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        FlipPlayer();
    }
}
