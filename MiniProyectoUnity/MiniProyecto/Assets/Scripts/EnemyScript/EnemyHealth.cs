using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int currentHealth;
    private Animator animator;
    public CapsuleCollider2D enemyCollider;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = health;
        enemyCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health < currentHealth)
        {
            currentHealth = health;
            animator.SetTrigger("Attacked");
        }
        if (health <= 0)
        {
            animator.SetBool("isDead", true);
            //Debug.Log("Eneny is dead");
        }
    }
    public void NoHitEnemy()
    {
        enemyCollider.enabled = false;
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
