using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{

    private Vector3 _shotDirection;
    private Vector3 _direction;
    private Player _player;
    private bool _canDamage = true;

    private void Start()
    {
        Debug.Log("Acid Effect Start");
        Destroy(this.gameObject, 5f);
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _shotDirection = CheckDirection();
        if (_shotDirection.x > 0)
        {
            _direction = Vector3.right;
        }
        else
        {
            _direction = Vector3.left;
        }
    }

    private void Update()
    {
        transform.Translate(_direction * 3 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);

        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null && _canDamage == true)
        {
            StartCoroutine(WoundCooldown());
            hit.Damage();
            Destroy(this.gameObject);
        }
    }

    IEnumerator WoundCooldown()
    {
        //Wound Cooldown
        _canDamage = false;
        yield return new WaitForSeconds(0.5f);
        _canDamage = true;
    }

    public virtual Vector3 CheckDirection()
    {
        Vector3 direction = _player.transform.localPosition - transform.localPosition;
        return direction;
    }
}
