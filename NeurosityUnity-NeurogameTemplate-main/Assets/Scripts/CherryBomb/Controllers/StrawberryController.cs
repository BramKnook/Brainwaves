using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberryController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("StrawberryBox"))
        {
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(1);
            }

            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Basket"))
        {
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(-1);
            }

            Destroy(gameObject);
        }
    }
}
