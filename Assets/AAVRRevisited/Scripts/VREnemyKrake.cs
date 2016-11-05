﻿using UnityEngine;
using System.Collections;

public class VREnemyKrake : MonoBehaviour {

    public int health;
    public int attack;
    public int cooldown;
    public int cdcounter;

    // Use this for initialization
    void Start () {
        this.health = 100;
        this.attack = 10;
        this.cooldown = 120;
        this.cdcounter = 0;
        
    }

    void Update() {
        this.cdcounter += 1;

        if (this.cdcounter == this.cooldown)
        {
            this.cdcounter = 0;
            this.Attack();
        }
    }

    void Attack() {
        // Start animation etc.
        Debug.Log("Krake attacks");
    }

    void Die() {

        Debug.Log("Krake died");
        // Death Animation
    }



    public void TakeDamage(int dmg)
    {
        this.health -= dmg;
        if (this.health <= 0)
        {
            this.Die();
        }
    }

}