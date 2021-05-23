using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "Player")
        {
            StartCoroutine(Die());
            
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Science_Project");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    
}
