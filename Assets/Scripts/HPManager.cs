using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HPManager : MonoBehaviour
{
    public int playerHp = 0;
    public PlayerHealth playerhealth;
    public Text healthText;

    void Start(){
      playerhealth = FindObjectOfType<PlayerHealth>();  
      Debug.Log(playerhealth.curHealth);
      
    }
    void Update()
    {
        healthText = GameObject.Find("HpLabel").GetComponent<Text>();
        healthText.text = "HP:" + playerhealth.curHealth;

        if(playerhealth.curHealth < 1){
            SceneManager.LoadScene("Gameover");
        }
    }

}