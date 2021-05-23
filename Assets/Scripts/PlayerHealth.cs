using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] bool hasDied;
    [SerializeField] int health;
    // Start is called before the first frame update
    void Start()
    {
        hasDied = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -7)
        {
            hasDied = true;
        }
        if (hasDied == true)
        {
            StartCoroutine("Die");
            StartCoroutine(Die());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.tag);
            if (collision.collider.tag == "Death")
            {
                Debug.Log("I have died");
                StartCoroutine(Die());
            }
            Debug.Log("I bleh died");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "Death")
        {
            Debug.Log("I have died");
            StartCoroutine(Die());
        }
        Debug.Log("I bleh died");
    }


    IEnumerator Die() {
        SceneManager.LoadScene("Science_Project");
        yield return null;

    }
}
