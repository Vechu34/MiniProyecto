using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Vida")]
    public int health = 3;
    public int numOfHearts = 3;

    [Header("Detección de daño")]
    [SerializeField] private LayerMask hitSpikeLayer;
    [SerializeField] private LayerMask hitEnemyLayer;
    [SerializeField] private LayerMask healLayer;

    [SerializeField] private float invulnerabilityTime = 1.0f; // Tiempo de invulnerabilidad tras recibir daño
    private bool isInvulnerable = false; // Estado de invulnerabilidad

    [Header("Interfaz de Usuario")]
    public Image[] heartsImg;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public PlayerMovement playerMovement;

    private void Update()
    {
        UpdateHeartsUI();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isInvulnerable) return; // No recibir daño si es invulnerable

        // Verificar si el objeto con el que colisiona pertenece a la capa "Spikes"
        if (((1 << collision.gameObject.layer) & hitEnemyLayer) != 0)
        {
            playerMovement.kbCounter = playerMovement.kbTotalTime;

            if(collision.transform.position.x <= transform.position.x)
            {
                playerMovement.knockFromRight = false;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerMovement.knockFromRight = true;
            }
  
            TakeDamage(1);
            //Debug.Log("¡Tocaste un pincho! Vida restante: " + health);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & (hitSpikeLayer | hitEnemyLayer)) != 0)
        {
            TakeDamage(1);
            if (((1 << collision.gameObject.layer) & hitEnemyLayer) != 0)
            {
                playerMovement.kbCounter = playerMovement.kbTotalTime;

                if (collision.transform.position.x <= transform.position.x)
                {
                    playerMovement.knockFromRight = false;
                }
                if (collision.transform.position.x > transform.position.x)
                {
                    playerMovement.knockFromRight = true;
                }
                //Debug.Log("¡Tocaste un pincho! Vida restante: " + health);
            }
        }

        if (((1 << collision.gameObject.layer) & healLayer) != 0)
        {
            Heal(1); // Heal by 1 heart (or modify as needed)
            Destroy(collision.gameObject); // Remove the health pickup
            Debug.Log("is healing");
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, numOfHearts); // Limita la vida entre 0 y numOfHearts

        if (health <= 0)
        {
           // Debug.Log("¡Game Over! El personaje murió.");
            // Aquí puedes agregar lógica para reiniciar el nivel o mostrar una pantalla de derrota.
            GameOver();
        }
        else
        {
            StartCoroutine(InvulnerabilityCoroutine());
        }
    }
    private void Heal(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, numOfHearts); // Prevent overhealing
        UpdateHeartsUI();
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < heartsImg.Length; i++)
        {
            heartsImg[i].sprite = (i < health) ? fullHeart : emptyHeart;
            heartsImg[i].enabled = (i < numOfHearts);
        }
    }
    private void GameOver()
    {
            SceneManager.LoadScene("GameOver");
    }
}