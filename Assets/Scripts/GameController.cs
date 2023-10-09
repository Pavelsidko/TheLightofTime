using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private static float health = 6;
    private static int maxHealth = 6;
    private static float moveSpeed = 5f;
    private static float fireRate = 0.5f;
    public TextMeshProUGUI healthText;
    private static float bulletSize = 0.5f;
    private bool bootCollected = false;
    private bool screwCollected = false;
    private bool Collecting = false;


    public List<string> collectedNames = new List<string>();

    public static float Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }

    
    void Update()
    {
        healthText.text = "Health: " + health;        
    }

    public static void DamagePlayer(int damage)
    {
        health -= damage;


        if (health <= 0)
        {
            KillPlayer();
        }

    }



    public static void HealPlayer(float healAmount) 
    {
        health = Mathf.Min(maxHealth, Health += healAmount);
    }

    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }
    public static void FireRateChange(float rate)
    {
        fireRate -= rate;
    }

    public static void BulletSizeChange(float size)
    {
        bulletSize += size;
    }



    public void UpdateCollectedItems(Collection item)
    {
        collectedNames.Add(item.item.name);

        foreach(string i in collectedNames)
        {
            switch (i)
            {
                case "Boot":
                    bootCollected = true; break;
                case "Screw":
                    screwCollected= true; break;
            }
        }


        if(bootCollected && screwCollected)
        {
            FireRateChange(0.25f);
        }
    }


    private static void KillPlayer()
    {
        //Destroy(gameObject);

    }

}
