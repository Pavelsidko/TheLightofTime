using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    Idle,
    Wander, 
    Follow, 
    Die, 
    Attack
}; 


public enum EnemyType
{
    Melee,
    Ranged
};



public class EnemyController : MonoBehaviour
{
    GameObject player;
    public EnemyState currState = EnemyState.Idle;
    public EnemyType enemyType;

    public float range;
    public float speed;
    public float attackRange;
    private bool coolDownAttack = false;
    public float coolDown;
    public GameObject bulletPrefab;
    private bool chooseDir = false;
    private bool dead = false;
    private Vector3 randomDir;
    public bool notInRoom = false;  
    //private Vector2 target;
    


    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //target = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    
    void Update()
    {
        switch (currState)
        {
            case EnemyState.Idle:
                Idle(); break;
            case EnemyState.Wander:
                Wander();
                break;
            case EnemyState.Follow:
                Follow();
                break;
            case EnemyState.Die:
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }


        if(!notInRoom)
        {
            if (IsPlayerInRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Follow;
            }
            else if (!IsPlayerInRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Wander;
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                currState = EnemyState.Attack;
            }
        }
        else 
        {
            currState = EnemyState.Idle;
        }
       
         
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }



    private IEnumerator ChooseDirection()
    {
        chooseDir= true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }

    void Wander() 
    {
        if(!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }
        transform.position += -transform.right * speed * Time.deltaTime;
        if(IsPlayerInRange(range))
        {
            currState= EnemyState.Follow;
        }


    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void Attack()
    {
        if (!coolDownAttack)
        {
            switch (enemyType)
            {
                case (EnemyType.Melee):
                    GameController.DamagePlayer(1);
                    StartCoroutine(CoolDown());
                    break;
                case (EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<Bullet>().GetPlayer(player.transform);
                    //Vector2 direction = player.transform.position - transform.position;
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<Bullet>().isEnemyBullet = true;
                    StartCoroutine(CoolDown());
                    break;
            }
        }
            
    }

    private void Idle()
    {

    }

    public void Death()
    {
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        Destroy(gameObject);
    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }
}
