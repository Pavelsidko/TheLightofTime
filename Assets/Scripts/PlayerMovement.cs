using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    Rigidbody2D rb;
    public TextMeshProUGUI collectedText; 

    public static int collectedAmount = 0;
    public Joystick joystick;
    //public Joystick shootJoystick;

   //public GameObject bulletPrefab;
    //public float bulletSpeed;
    //private float lastFire;
    //public float fireDelay;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        //float shootHorizontal = shootJoystick.Horizontal;
        //float shootVertical = shootJoystick.Vertical;
        
        /*
        if(shootHorizontal != 0 || shootVertical != 0 && Time.time > lastFire + fireDelay)
        {
            //Shoot(shootHorizontal, shootVertical);
            lastFire = Time.time;
        }
        */

        rb.velocity = new Vector3(horizontal * speed, vertical * speed, 0);
        collectedText.text = "ITEMS COLLECTED: " + collectedAmount;
        speed = GameController.MoveSpeed;


        /*
        void Shoot(float x, float y)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed * Time.deltaTime,
                (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed * Time.deltaTime,
                0);
        }
        */
    }
}
