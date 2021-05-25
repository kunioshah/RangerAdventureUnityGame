using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] public int playerSpeed = 10;
    [SerializeField] TextMeshProUGUI textToChange;
    Collider2D collisioning;
    [SerializeField] GameObject trigger1;
    [SerializeField] GameObject trigger2;
    [SerializeField] GameObject trigger3;
    [SerializeField] GameObject trigger4;
    [SerializeField] GameObject trigger5;
    [SerializeField] GameObject trigger6;
    [SerializeField] GameObject trigger7;
    [SerializeField] GameObject trigger8;
    [SerializeField] GameObject trigger9;
    [SerializeField] GameObject trigger10;
    [SerializeField] GameObject trigger11;
    [SerializeField] GameObject trigger12;
    [SerializeField] float[] checkpoints = { 0, 15, 100, 150};
    public bool facingRight = true;
    bool hasPlayer = false;
    [SerializeField] public int playerJump = 250;
    public float moveX;
    public bool isGrounded;
    private string whatweon;
    private GameSession gameSession;
    public string directionstomped;
    [SerializeField] string textsStyle;
    [SerializeField] public float distanceToBottom;
    SceneLoader sceneLoader;
    public bool whatere = false;
    [SerializeField] GameObject floor;
    private bool isDead = false;
    int currentDialogCounter = 0;
    int currentSubDialog = 0;
    bool colliderTriggered = false;
    GameObject dialogueImage;
    string[][] dialog = new string[][]
    {
        new string[] {"Hey there! Welcome to my 8th Grade Life Sciences Project.",
        "This project was made by Kunal Shah, 8th grade.",
        "I'll be explaining a few principles of life science through this game! Let's get started!",
        "You play as a ranger documenting ingo, with your robot guide helping you.",
        "The only commands you need are the arrow keys to move and space to jump!",
        "Have fun!"},
        new string[]{"Hmm, that looks like cacti... Make sure not to touch it!"},
        new string[]{"Look at this hill! There are so many layers in it, and the hill is filled with fossils! Fascinating."},
        new string[]{"As the layers go up, the fossils change... This is like a natural time scale! We call this the fossil record....",
            "As you go up, the types of rock change." ,
            "So the lower rock layers must be the eldest layers, as more rock is piled on it.",
            "That's called the law of superposition, by the way!",
            "So the types of fossils in the rock were from the time period of the rock...",
            "Meaning that the fossils in the lower rock are older!",
        "This is called relative dating - finding out how old fossils and rock is compared to other fossils and rock."},
        new string[]{"You can see how the organisms became more complex overtime." ,
            "A lot of them look similar, they must have evolved from the older organisms.",
            "Plus, you see that little spiral fossil? How it's only in that one layer?",
            "That's what we call an index fossil, a recognizable fossil only in one time period that we use as an index for others.",
            "Of course, the other kind of dating - talking about rocks, anyways - is radioactive dating.",
            "Basically, elements decay at a certain rate, called their half life.",
            "By calculating the rate of decay of a rock against how much of it has decayed..." ,
            "we can see how old it is, and then place it into the time scale." },
        new string[]{"The Geologic Time Scale is the time and eras that Earth has gone through." ,
            "Yup, that's the age of the earth. And everything it's gone through.",
            "Hmm, there sure are a lot of fossils in this layer. Must have been a big extinction event.",
            "That's when a major disaster happens that kills many species.",
            "And then, new organisms come, and then they die."},
        new string[]{"Oh wow, look at that bee fossil! Those are huge wings!"},
        new string[]{"Wow, that's a large eagle! It's strange how it's wings look so different from the bee...",
        "You know? Like the structure looks so different but it fulfills the same purpose.",
        "That's called an analogous structure - it's a different structure that fulfills the same purpose.",
        "So the bee may have evolved differently from the eagle. Sometimes you'll see the opposite...",},
        new string[]{"Look at that dinosaur! Did you know that it may have been the ancestor of that eagle?",
        "Yeah, look at the head structure, it's similar.",
        "It should be millions of years dead! This is a freak of nature!",
        "When a structure on two organisms is similar even though they may have a different purpose...",
        "That's called a homologous structure.",
        "When you see one, it usually means that those organisms have a common ancestor and evolved from it.",
        "You can see homologous similarities on multiple living organisms...",
        "And fossils! It's interesting to trace an animal's evolutionary path.",
        "We can even trace similarities in animals' embryo stages!",
        "Animals with similar embryos could have evolved from a common ancestor.",
        "Another indicator of common ancestors is a vestigial structure.",
        "These are structures that aren't useful on an organism but are likely left over from an ancestor.",
        "Leg bones on whales, extra toe bones, stuff like that."},
        new string[]{ "Man, look at these lizards.",
            "Notice how most of them are orange? They're trying to blend in with the environment.",
            "This is called camoflauge. The lizards are trying to mimic the ground and blend in.",
            "There's probably an interesting past behind why they're all orange.",
            "The lizards were probably all a different color - just like that green one over there.",
            "There were probably some orange variations, or genetic mutations of the lizard.",
            "Then as the area grew more, well... orange...",
            "The orange mutations were killed by predators less and bred more.",
            "These lizards had orange traits, or characteristics, that were passed down to the offspring.",
            "Meanwhile, the green ones were killed more, and bred less.",
            "There's a theory for this called survival of the fittest.",
            "It states that the organisms best adapted to their environment will live as the others die.",
            "So over time, almost the entire population became orange.",
            "This is called natural selection.",},
        new string[]{"Aww, look at these baby birds! Are they alone?",
        "You know. We could probably find out who their parents are.",
        "How? Oh, it's simple, we can look at their traits!",
        "These birds have dark blue eyes with blue specks and white skin.",
        "We can see what genes may have caused this.",
        "Here, I'll pull up a diagram. This is called a punnet square.~Images/shareimage1",
        "Those letters are called alleles. Alleles are your genes.~Images/shareimage1",
        "Each parent passes down one allele for each trait. Both alleles together form your genotype.~Images/shareimage1",
        "That's both of those letters together.~Images/shareimage1",
        "Each letter represents a different trait.~Images/shareimage1",
        "So, for example, capital W means white skin and lowercase w means brown skin.~Images/shareimage1",
        "Since each of your parents have 2 alleles in one trait, they pass down one of them.~Images/shareimage1",
        "The chance of either gene is random.~Images/shareimage1",
        "So, as you see in this punnet sqsuare, if one parent has Ww gene and one has ww gene...~Images/shareimage1",
        "The probability of any of their offspring having Ww is 1/2, or 2 of the four squares...~Images/shareimage1",
        "The probability of any of them having ww is 1/2, or 2 of the four squares, too.~Images/shareimage1",
        "Capital letter genes are called dominant genes.~Images/shareimage1",
        "If they are in any genotype, then they will be apparent. In a Ww type gene, W will be shown.~Images/shareimage1",
        "Lowercase letter genes are called recessive genes~Images/shareimage1",
        "They will only be apparent if both genese are recessive.~Images/shareimage1",
        "So they can be passed down through generations and only express themselves after being inherited from generations ago.~Images/shareimage1",
        "Any genotype with two of the same gene are called homozygous, or purebred.~Images/shareimage1",
        "Two dominant genes, like WW, or two recessive ones, like ww, are homozygous.~Images/shareimage1",
        "Any genotype with two different genes are called heterozygous, or hybrid.~Images/shareimage1",
        "So that would be Ww in this case.~Images/shareimage1",
        "Of course, there's not just homozygous and heterozygous.",
        "There's also codominance and incomplete dominance! I'll show another punnet square.~Images/shareimage2",
        "Codominance is when both traits express themselves.~Images/shareimage2",
        "For example, if one bird had dark blue eyes (BB) and the other had light blue eyes (LL)...~Images/shareimage2",
        "Then the offspring might have dark blue eyes with light blue dots in them.~Images/shareimage2",
        "Both parent traits are expressing themselves, they're both dominant.~Images/shareimage2",
        "Incomplete dominance is when neither trait is shown, and the offspring has its own trait.~Images/shareimage2",
        "So in the case of BB and LL, or dark blue and light blue...~Images/shareimage2",
        "The offspring might have normal blue eyes. Neither parent trait is shown.~Images/shareimage2",
        "If we examine all of the traits, we might be able to match parents!",
        "We'll send a team to do it right away.",
        "Thanks for your help on this expedition! We documented so much information!",
        "We couldn't have done it without you! Farewell for now!",
        ""},
        //new string[]{"~Images/shareimage2"},
        //new string[]{""}
    };


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

        //textToChange.text = "Alright, let's get started!";
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameSession = FindObjectOfType<GameSession>();
        dialogueImage = GameObject.FindGameObjectWithTag("DialogueImage");
        if (dialogueImage)
        {
            dialogueImage.SetActive(false);
        }
        Debug.Log("gameSession.GetCheckpoint()=" + gameSession.GetCheckpoint());
        if (gameSession.GetCheckpoint() > 0)
        {
            Vector3 p = transform.position;
            p.x = checkpoints[gameSession.GetCheckpoint()];
            transform.position = p;

            currentDialogCounter = gameSession.GetDialogCounter();
            FindObjectOfType<Canvas>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (hasPlayer && Input.GetKeyDown(KeyCode.A))
        if (Input.GetKeyDown(KeyCode.F) && colliderTriggered)
        {
            if (currentSubDialog > dialog[currentDialogCounter].Length - 1)
            { 
                FindObjectOfType<Canvas>().enabled = false;
                textToChange.text = "...";
                UnPause();
                whatere = true;
                currentSubDialog = 0;
                currentDialogCounter++;
                colliderTriggered = false;
                dialogueImage.SetActive(false);
                if (currentDialogCounter == dialog.Length)
                {
                    StartCoroutine(EndGame());
                }
            }
            else
            {
                Pause();
                //Debug.Log("currentdialogcounter=" + currentDialogCounter + " currentSubDialog=" + currentSubDialog);
                dialogueImage.SetActive(false);
                var dialogTextArray = dialog[currentDialogCounter][currentSubDialog].Split('~');
                textToChange.text = dialogTextArray[0];
                if (dialogTextArray.Length > 1)
                {

                    dialogueImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(dialogTextArray[1]);
                    dialogueImage.SetActive(true);
                }
                currentSubDialog++;
            }

        }
        PlayerMove();
        PlayerRaycast();
        if (this.transform.position.y != floor.transform.position.y)
        {
            //isGrounded = false;
        }
        //if (Input.GetKeyDown(KeyCode.A)) {
        //    Pause();
        //}
        SetCheckpoint();
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("EndScreen");
    }

    private void SetCheckpoint()
    {
        var gameSessionCheckpoint = gameSession.GetCheckpoint();
        if (checkpoints.Length > gameSessionCheckpoint + 1)
        {
            //Debug.Log("x=" + gameObject.transform.position.x + " gamesessionchekpoint=" + checkpoints + " checkpoint=" + checkpoints[gameSessionCheckpoint + 1]);
            if (gameObject.transform.position.x > checkpoints[gameSessionCheckpoint + 1])
            {
                //Debug.Log("Inside setcheckpoint");
                gameSession.SetCheckpoint(gameSessionCheckpoint+1);
                gameSession.SetDialogCounter(currentDialogCounter);
                //Debug.Log("gameSession.GetCheckpoint1()=" + gameSession.GetCheckpoint());
            }
        }
    }

    void PlayerMove()
    {
        if (isDead)
        {
            playerState = PlayerState.Death;
            ChangeAnimation();
            return;
        }
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            PlayerJump();
        }
        if (moveX < 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveX > 0.0f)
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
        
//        Debug.Log("Player state=" + playerState);
    }

    void PlayerJump()
    {
        // Debug.Log("jumped");
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJump);
        isGrounded = false;
    }
    private void ChangeAnimation()
    {
        if (isDead)
        {
            GetComponent<Animator>().SetTrigger("IsDead");
        }
        else if (playerState == PlayerState.Idle)
        {
            GetComponent<Animator>().SetTrigger("IsIdle");
            whatweon = "Idle";
        }
        else if (playerState == PlayerState.Running)
        {
            GetComponent<Animator>().SetTrigger("IsRunning");
            whatweon = "running";
        }
        else if (playerState == PlayerState.Jumping)
        {
            GetComponent<Animator>().SetTrigger("IsJumping");
        }
       
    }

    public IEnumerator ContinuousDebugs(string debugs)
    {
        yield return new WaitForSeconds(1);
        Debug.Log(debugs);
    }

    void PlayerRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        //        Debug.Log("hit distance is " + hit.distance + "dustance to bottom is " + distanceToBottom + "hit.collider.tag is " + hit.collider.tag);
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
        }
        else if (hit.distance > distanceToBottom && hit.collider.tag == "Surface")
        {
            // isGrounded = false;
        }


        if (hit.collider.tag == "bounceEnemy" || hit.collider.tag == "bounceEnemyHigher")
        {
            if (hit.distance < distanceToBottom)
            {
                Debug.Log("enemy touched the player");
                PlayerDeath();
            }
            else
            {
                Debug.Log("raycast down");
                Debug.Log("entered this thing");
                Debug.Log("hit distance is " + hit.distance + " dustance to bottom is " + distanceToBottom);
                directionstomped = "down";
                if (hit.collider.tag == "bounceEnemy")
                {
                    hit.collider.GetComponent<EnemyMove>().Crush();
                }
                else
                {
                    hit.collider.GetComponent<EnemyMove>().Crush(-4.5f);
                }

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

    public void PlayerDeath()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        playerState = PlayerState.Death;
        isDead = true;
        ChangeAnimation();
        yield return new WaitForSeconds(1.02f);
     
        SceneManager.LoadScene("Science_Project");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("entered 2d upon colliosn");

        if (collision.collider.tag == "bounceEnemy" || collision.collider.tag == "bounceEnemyHigher")
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
            FindObjectOfType<Canvas>().enabled = true;
            textToChange.text = dialog[currentDialogCounter][0];
            if (gameSession.GetGameState() == GameState.Manual)
            {
                Pause();
                Destroy(trigger);
                currentSubDialog++;
                colliderTriggered = true;
                //Destroy(collisions);
                //UnPause();
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasPlayer = true;
        if (trigger1)
        {
            dialogueProgress(trigger1, collision, "Hmm, that looks like cacti... Make sure not to touch it!");
        }
        if (trigger2)
        {
            dialogueProgress(trigger2, collision, "Look at this hill! There are so many layers in it, and the hill is filled with fossils! Fascinating.");
        }
        if (trigger3)
        {
            dialogueProgress(trigger3, collision, "Huh. Look at that hornet fossil. It's so well preserved!");
        }
        if (trigger4)
        {
            dialogueProgress(trigger4, collision, "Oh wow, an eagle! Look at those wings! Oh and it'll probably kill you.");
        }
        if (trigger5)
        {
            dialogueProgress(trigger5, collision, "Alright, let's get started!");
            //dialogueProgress(trigger3, collision, "As the layers go up, the fossils change... This is like a naturl");
            //Destroy(trigger2);a

            //if (collision.gameObject.name == trigger3.name)
            //{
            //    StartCoroutine(LizardText());
            //}

        }
        if (trigger6)
        {
            dialogueProgress(trigger6, collision, "rock layers balah askldghasldg;s.");
            //dialogueProgress(trigger6, collision, "ok so test if this works?.");
        }
        if (trigger7)
        {
            dialogueProgress(trigger7, collision, "blahblahblha");
        }
        if (trigger8)
        {
            dialogueProgress(trigger8, collision, "blahblahblha");
        }
        if (trigger9)
        {
            dialogueProgress(trigger9, collision, "blahblahblha");
        }
        if (trigger10)
        {
            dialogueProgress(trigger10, collision, "blahblahblha");
        }
        if (trigger11)
        {
            dialogueProgress(trigger11, collision, "blahblahblha");
        }
        

        IEnumerator LoadFinalScene()
        {
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("EndScreen");
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
            textToChange.text = "Plus, you see that little skull fossil? How it's only in that one layer? That's what we call an index fossil, a recognizable fossil only in one time period that we can use as an index for others.";
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

        void OnTriggerExit2D(Collider2D collision)
        {
            hasPlayer = false;
        }
    }
}
