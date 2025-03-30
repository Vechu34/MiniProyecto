using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    public Transform firePosition;
    public Projectile projectile;
    public bool canAttack = true;
    public float cooldownTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)&& canAttack == true)
        {
            Projectile newProjectile = Instantiate(projectile, firePosition.position, transform.rotation);
            newProjectile.SetDirection(transform.localScale.x > 0); // Si el jugador mira a la derecha, va a la derecha
            StartCoroutine(CooldownAttack(cooldownTime));
        }
    }
    IEnumerator CooldownAttack(float time)
    {
        canAttack = false;
        yield return new WaitForSeconds(time);
        canAttack = true;
    }
}
