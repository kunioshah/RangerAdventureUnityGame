using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyProto : MonoBehaviour
{
    [SerializeField] Vector2 velocity;
    [SerializeField] LayerMask wallMask;
    [SerializeField] LayerMask floorMask;
    [SerializeField] float gravity;
    [SerializeField] float timeBeforeDestroy = 1f;

    private bool isWalkingLeft = true;
    private bool isGrounded = false;
    private EnemyState enemyState;
    private bool shouldDie = false;
    private float deathTimer = 0;


    private enum EnemyState
    {
        walking,
        falling,
        dead
    }

    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
    }
    public int EnemySpeed;
    public int xMoveDirection;

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


    // Update is called once per frame
    void Update()
    {
        UpdateEnemyPosition();
        CheckCrushed();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMoveDirection, 0));
        if (gameObject.GetComponent<Rigidbody2D>() != null)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * EnemySpeed;
        }
        // Debug.Log("hit distance is " + hit.distance);
        if (hit.distance < 0.7f)
        {
            FlipPlayer();
        };
    }

    public void Crush()
    {
        enemyState = EnemyState.dead;
        ///GetComponent<Animator>().SetBool("isCrushed", true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<AudioSource>().Play();
        shouldDie = true;

    }

    private void CheckCrushed()
    {
        if (shouldDie)
        {
            if (deathTimer <= timeBeforeDestroy)
            {
                deathTimer += Time.deltaTime;
            }
            else
            {
                shouldDie = false;
                Destroy(gameObject);
            }
        }
    }

    private void UpdateEnemyPosition()
    {
        if (enemyState != EnemyState.dead)
        {
            Vector3 position = transform.localPosition;
            Vector3 scale = transform.localScale;

            if (enemyState == EnemyState.falling)
            {
                position.y += velocity.y * Time.deltaTime;
                velocity.y -= gravity * Time.deltaTime;
            }

            if (enemyState == EnemyState.walking)
            {
                if (isWalkingLeft)
                {
                    position.x -= velocity.x * Time.deltaTime;
                    scale.x = -1;
                }
                else
                {
                    position.x += (velocity.x * Time.deltaTime);
                    scale.x = 1;
                }
            }

            if (velocity.y <= 0)
            {
                position = CheckFloorRays(position);
            }

            CheckWalls(position, scale.x);

            transform.localPosition = position;
            transform.localScale = scale;
        }
    }
    void CheckWalls(Vector3 position, float direction)
    {
        Vector2 originTop = new Vector2(position.x + direction * 0.4f, position.y + 0.5f - 0.2f);
        Vector2 originMiddle = new Vector2(position.x + direction * 0.4f, position.y);
        Vector2 originBottom = new Vector2(position.x + direction * 0.4f, position.y - 0.5f + 0.2f);

        RaycastHit2D wallTop = Physics2D.Raycast(originTop, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallMiddle = Physics2D.Raycast(originMiddle, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallBottom = Physics2D.Raycast(originBottom, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);

        if (wallTop.collider != null || wallMiddle.collider != null || wallBottom.collider != null)
        {
            RaycastHit2D hitRay = wallTop;
            if (wallMiddle)
            {
                hitRay = wallMiddle;
            }
            else if (wallBottom)
            {
                hitRay = wallBottom;
            }
            if (hitRay.collider.tag == "Player")
            {
                SceneManager.LoadScene("GameOver");
            }
            isWalkingLeft = !isWalkingLeft;
            enemyState = EnemyState.walking;
            //position.x -= velocity.x * Time.deltaTime * direction;
        }

    }

    Vector3 CheckFloorRays(Vector3 position)
    {
        Vector2 originLeft = new Vector2(position.x - 0.5f + 0.2f, position.y - 0.5f);
        Vector2 originMiddle = new Vector2(position.x, position.y - 0.5f);
        Vector2 originRight = new Vector2(position.x + 0.5f - 0.2f, position.y - 0.5f);

        RaycastHit2D floorLeft = Physics2D.Raycast(originLeft, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D floorMiddle = Physics2D.Raycast(originMiddle, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D floorRight = Physics2D.Raycast(originRight, Vector2.down, velocity.y * Time.deltaTime, floorMask);

        if (floorLeft.collider != null || floorMiddle.collider != null || floorRight.collider != null)
        {
            RaycastHit2D hitRay = floorRight;
            if (floorLeft)
            {
                hitRay = floorLeft;
            }
            else if (floorMiddle)
            {
                hitRay = floorMiddle;
            }

            enemyState = EnemyState.walking;
            isGrounded = true;
            velocity.y = 0;

            position.y = hitRay.collider.bounds.center.y + hitRay.collider.bounds.size.y / 2 + 0.5f;
        }
        else
        {
            if (enemyState != EnemyState.falling)
            {
                Fall();
            }
        }

        return position;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    void Fall()
    {
        velocity.y = 0;
        enemyState = EnemyState.falling;
        isGrounded = false;
    }
}
