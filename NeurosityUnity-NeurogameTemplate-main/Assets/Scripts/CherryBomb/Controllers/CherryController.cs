using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public GameObject particleEffect;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Basket"))
        {
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(1);
            }

            Instantiate(particleEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
