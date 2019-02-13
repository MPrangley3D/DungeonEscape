using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canDamage = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);

        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null && _canDamage == true)
        {
            StartCoroutine(WoundCooldown());
            hit.Damage();
            if(other.tag == "Enemy" && GameManager.Instance.hasSword == true)
            {
                hit.Damage();
                hit.Damage();
            }

        }
    }

    IEnumerator WoundCooldown()
    {
        //Wound Cooldown
        _canDamage = false;
        yield return new WaitForSeconds(0.5f);
        _canDamage = true;
    }

}
