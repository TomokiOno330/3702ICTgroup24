using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int curHealth;
    public int maxHealth;

    public Slider healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        healthBar.value = curHealth;
        healthBar.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendDamage(int damageValue)
    {
        curHealth -= damageValue;
        healthBar.value = curHealth;
    }
}