using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;
    private float time;
    public float fireDelay;

    public GameObject bullet;
    public Transform point;

    public Joystick weaponJoystick;

    void Update()
    {
        fireDelay = GameController.FireRate;
        
        float rotateZ = Mathf.Atan2(weaponJoystick.Vertical, weaponJoystick.Horizontal) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);


        if (time <= 0f)
        {
            if (weaponJoystick.Horizontal > 0 || weaponJoystick.Vertical > 0 || weaponJoystick.Horizontal < 0 || weaponJoystick.Vertical < 0)
            {
                Instantiate(bullet, point.position, transform.rotation);
                time = fireDelay;
            }
        }
        else
        {
            time -= Time.deltaTime;
        }
    }


    

}
