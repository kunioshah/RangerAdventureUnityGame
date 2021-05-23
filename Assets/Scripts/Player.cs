using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] public int playerSpeed = 10;
    [SerializeField] TextMeshProUGUI textToChange;
    [SerializeField] GameObject trigger1;
    [SerializeField] GameObject trigger2;
    [SerializeField] GameObject trigger3;
    [SerializeField] GameObject trigger4;
    public bool facingRight = true;
    bool hasPlayer = false;
    [SerializeField] public int playerJump = 250;
    public float moveX;
    public bool isGrounded;
    private string whatweon;
    private GameSession gameSession;
    public string directionstomped;
    [SerializeField]string textsStyle;
    [SerializeField] public float distanceToBottom;
    SceneLoader sceneLoader;
    [SerializeField] GameObject floor;
    private enum PlayerState
   
    {
        Idle,
        Running,
        Jumping,
        Falling,
        Death
    }

    private PlayerState playerState = PlayerState.Idle;

    void Start()
    {
        
        textToChange.text = "Alright, let's get started!";
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameSession = FindObjectOfType<GameSession>();
        //textsStyle = sceneLoader.getStyle();
//        Debug.Log(sceneLoader.getStyle());
        //textsStyle = "manual";
        
        Debug.Log(textsStyle);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPlayer && Input.GetKeyDown(KeyCode.A))
        {
          //  Debug.Log("a pressed");
            textToChange.text = "...";
            UnPause();

        }
        PlayerMove();
        PlayerRaycast();
        if (this.transform.position.y != floor.transform.position.y) {
            //isGrounded = false;
        }
        //if (Input.GetKeyDown(KeyCode.A)) {
        //    Pause();
        //}
    }

    void PlayerMove() {
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown ("Jump") && isGrounded == true)
        {
            PlayerJump();
        }
        if (moveX < 0.0f )
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveX > 0.0f )
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        var playerVelocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        gameObject.GetComponent<Rigidbody2D>().velocity = playerVelocity;

       // StartCoroutine(ContinuousDebugs("" + playerVelocity.x));
        
        if ((playerVelocity.x > 0f || playerVelocity.x < 0))
        {
            playerState = PlayerState.Running;
        //    Debug.Log("changed to running");
            ChangeAnimation();

        }
        else if (playerVelocity.x == 0f)
        {
            playerState = PlayerState.Idle;
            ChangeAnimation();
         //   Debug.Log("changed to idle");
        }
    }

    void PlayerJump()
    {
       // Debug.Log("jumped");
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJump);
        isGrounded = false;
    }

    void FlipPlayer()
    {
            facingRight = !facingRight;
            Vector2 localScale = gameObject.transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        if (isGrounded)
        {
            
            
        } 
    }
    
    private void ChangeAnimation()
    {
        if (playerState == PlayerState.Idle)
        {
            GetComponent<Animator>().SetTrigger("IsIdle");
            whatweon = "Idle";
        }
        else if (playerState == PlayerState.Running){
            GetComponent<Animator>().SetTrigger("IsRunning");
            whatweon = "running";
        }
        else if (playerState == PlayerState.Jumping)
        {
            GetComponent<Animator>().SetTrigger("IsJumping");
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Surface") {
    //    //    Debug.Log("hit the ground");
    //        isGrounded = true;
    //        playerState = PlayerState.Idle;
    //    }
    //}

    public IEnumerator ContinuousDebugs(string debugs)
    {
        yield return new WaitForSeconds(1);
        Debug.Log(debugs);
    }

    void playerDeathCast()
    {
        Vector2 originLeft = new Vector2(transform.position.x - 0.5f + 0.2f, transform.position.y - 1f);
        Vector2 originMiddle = new Vector2(transform.position.x, transform.position.y - 1f);
        Vector2 originRight = new Vector2(transform.position.x + 0.5f - 0.2f, transform.position.y - 1f);

        //RaycastHit2D floorLeft = Physics2D.Raycast(originLeft, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        //RaycastHit2D floorMiddle = Physics2D.Raycast(originMiddle, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        //RaycastHit2D floorRight = Physics2D.Raycast(originRight, Vector2.down, velocity.y * Time.deltaTime, floorMask);
    }
    void PlayerRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        Debug.Log("hit distance is " + hit.distance + "dustance to bottom is " + distanceToBottom + "hit.collider.tag is " + hit.collider.tag);
        if (hit.collider)
        {
            if (hit.collider.tag == "")
            {

            }
        }
        // Debug.Log("hit.distnace: " + hit.distance + "distance to bottom:" + distanceToBottom);
        if (hit.distance <= distanceToBottom && hit.collider.tag == "Surface")
        {
            //isGrounded = true;
            //Debug.Log("touching ground");
        } else if (hit.distance > distanceToBottom && hit.collider.tag == "Surface") {
           // isGrounded = false;
        }

       
        if (hit.distance <= distanceToBottom && hit.collider.tag == "bounceEnemy")
        {
            Debug.Log("raycast down");
            Debug.Log("entered this thing");
            Debug.Log("hit distance is " + hit.distance + " dustance to bottom is " + distanceToBottom);
            directionstomped = "down";
            hit.collider.GetComponent<EnemyMove>().Crush();
            IEnumerator destroyAfterTime()
            {
                yield return null;
                //if (hit.collider.gameObject.GetComponent<PolygonCollider2D>() != null)
                //{
                //    Debug.Log("destroying box collider");
                //    Destroy(hit.collider.gameObject.GetComponent<PolygonCollider2D>());
                //}
                //if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                //{
                //    Debug.Log("destroying rigid boyd");
                //    Destroy(hit.collider.gameObject.GetComponent<Rigidbody2D>());
                //}
                //Destroy(hit.collider);
            }

            StartCoroutine(destroyAfterTime());
        }
    }

    public IEnumerator waitforTime(int time)
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 500);
        yield return new WaitForSeconds(2);
        Debug.Log("waited for 2 seconds");
//
    }

    void Pause()
    {
        Time.timeScale = 0;
    }

    void UnPause()
    {
        Time.timeScale = 1;
    }

    void playerDeath()
    {
        playerState = PlayerState.Death;
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Science_Project");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log("entered 2d upon colliosn");

        if (collision.collider.tag == "bounceEnemy" )
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 500);
        }
        if (collision.collider.tag == "Surface")
        {
            isGrounded = true;
        }

    }

    void dialogueProgress(GameObject trigger, Collider2D collisions, string dialogue)
    {
        if (collisions.gameObject.name == trigger.name && collisions.gameObject != null)
        {
            textToChange.text = dialogue;
            if (gameSession.GetGameState() == GameState.Manual)
            {
                Pause();
                //Destroy(trigger);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasPlayer = true;
        dialogueProgress(trigger1, collision, "Hmm, that looks like cacti... Make sure not to touch it!");
        Destroy(trigger1);
        dialogueProgress(trigger2, collision, "Look at this hill! There are so many layers in it, and the hill is filled with fossils! Fascinating.");
        //dialogueProgress(trigger3, collision, "As the layers go up, the fossils change... This is like a naturl");
        //Destroy(trigger2);a

        if (collision.gameObject.name == trigger3.name)
        {
            StartCoroutine(LizardText());
        }

    }

    IEnumerator LizardText()
    {
        textToChange.text = "As the layers go up, the fossils change... This is like a natural time scale! We call this the fossil record.";
        yield return new WaitForSeconds(3);
        textToChange.text = "As you go up, the types of rock change. So the lower rock layers must be the eldest layers, as more rock is piled on it. That's called the law of superposition, by the way! ";
        yield return new WaitForSeconds(3);
        textToChange.text = "So the types of fossils in the rock were from the time period of the rock, meaning that the fossils in the lower rock are older!";
        yield return new WaitForSeconds(3);
        textToChange.text = "This is called relative dating - finding out how old fossils and rock is compared to other fossils and rock. ";
        yield return new WaitForSeconds(3);
        textToChange.text = "Plus, you see that little spiral fossil? How it's only in that one layer? That's what we call an index fossil, a recognizable fossil only in one time period that we can use as an index for others.";
        yield return new WaitForSeconds(3);
        textToChange.text = "Of course, the other kind of dating - talking about rocks, anyways - is radioactive dating. ";
        yield return new WaitForSeconds(3);
        textToChange.text = "Basically, elements decay at a certain rate. By calculating the rate of decay of a rock against how much of it has decayed, we can see how old it is, and then place it into the time scale. ";
        yield return new WaitForSeconds(3);
        textToChange.text = "The Geologic Time Scale is the time and eras that Earth has gone through. Here, I'll pull up an image for you.";
        yield return new WaitForSeconds(3);
        textToChange.text = "Yup, that's the age of the earth. And everything it's gone through.";
        yield return new WaitForSeconds(3);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hasPlayer = false;
    }

}
