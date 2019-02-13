using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{

    public GameObject acidEffectPrefab;

    public override void Init()
    {
        base.Init();
        Health = base.health;
        value = 25;
    }

    public void Attack()
    {
        Debug.Log("Spider Attack Called");
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }

    public override void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 10f)
        {
            isHit = true;
            anim.SetBool("InCombat", true);
        }
        if (distance > 11f)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }
        else
        {
            anim.SetBool("InCombat", true);
        }
    }

}

