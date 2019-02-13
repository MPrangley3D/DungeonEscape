using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer enemySprite;
    protected Player player;
    protected bool isHit = false;
    public int Health { get; set; }
    protected bool living = true;
    protected BoxCollider2D collider;
    public GameObject diamondPrefab;
    protected int value;

    private void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        collider = GetComponentInChildren<BoxCollider2D>();
        anim = GetComponentInChildren<Animator>();
        enemySprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public virtual void Update()
    {
        if (living == true)
        {
            CheckWaypoint();
            CheckDistance();
            Flip();
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                return;
            }

            if (isHit == false)
            {
                Movement();
            }
        }
    }

    public virtual void Damage()
    {
        Debug.Log("Damage()");
        Health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);

        if (Health < 1)
        {
            anim.SetTrigger("Death");
            living = false;
            Destroy(collider);
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().value = value;
        }
    }


    public virtual Vector3 CheckDirection()
    {
        Vector3 direction = player.transform.localPosition - transform.localPosition;

        return direction;
    }

    public virtual void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < 1f)
        {
            isHit = true;
            anim.SetBool("InCombat", true);
        }
        if(distance > 2f)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }
        else
        {
            anim.SetBool("InCombat", true);
        }
    }

    public virtual void CheckWaypoint()
    {
        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("Idle");
        }
    }

    public virtual void Movement()
    {

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);   
    }


    public void Flip()
    {
        if (anim.GetBool("InCombat") == false)
        {
            Vector3 myFacing = transform.localScale;
            var size = Mathf.Abs(transform.localScale.x);

            if (currentTarget == pointA.position)
            {
                myFacing.x = -size;
            }
            else
            {
                myFacing.x = size;
            }

            transform.localScale = myFacing;
        }

        else
        {
            Vector3 playerDirection = CheckDirection();
            Vector3 myFacing = transform.localScale;
            var size = Mathf.Abs(transform.localScale.x);
            if (playerDirection.x > 0)
            {
                myFacing.x = size;
            }
            else
            {
                myFacing.x = -size;
            }
            transform.localScale = myFacing;
        }
    }


}
