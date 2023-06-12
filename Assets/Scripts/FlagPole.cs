using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FlagPole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entering flag collider");
        if (collision.tag == "Player")
        {
            //StartCoroutine(EndGame());
            SceneManager.LoadScene("EndScreen");
        }
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("EndScreen");
    }
}
