using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{
    public override void Init()
    {
        base.Init();
        Health = base.health;
        value = 100;
    }

    public override void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 2f)
        {
            isHit = true;
            anim.SetBool("InCombat", true);
        }
        if (distance > 4f)
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