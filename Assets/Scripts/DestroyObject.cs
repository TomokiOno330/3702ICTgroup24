using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public int objectHealth;
    public GameObject[] itemPrefabs;
    public int number3;  
    private ScoreManager scoremanager;

    void Start()
    {

        scoremanager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shell"))
        {
            objectHealth -= 1;

            if (objectHealth > 0)
            {
                Destroy(other.gameObject);
            }
            else
            {
                Destroy(this.gameObject);

                scoremanager.AddScore(number3);
            }
        }
    }
}