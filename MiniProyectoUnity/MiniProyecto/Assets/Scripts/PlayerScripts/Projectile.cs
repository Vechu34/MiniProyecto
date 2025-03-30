using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 direction;
    public float lifeTime;
    public float projectileSpeed = 5f;
    private Rigidbody2D rb;
    public float attackRadius = 0.5f;
    public LayerMask enemyLayer; // Capa de enemigos
    public LayerMask collisionLayers; // Capas con las que el proyectil debe desaparecer
    public int damage = 1; // Daño del proyectil

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Mueve el proyectil en la dirección en la que fue lanzado
        Destroy(gameObject, lifeTime);
    }
    public void SetDirection(bool isRight)
    {
        direction = isRight ? Vector2.right : Vector2.left;

        transform.rotation = isRight ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
    }

    private void Update()
    {
        transform.position += (Vector3)direction * projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int collisionLayer = collision.gameObject.layer;

        // Si el proyectil golpea un enemigo, le causa daño y desaparece
        if (((1 << collisionLayer) & enemyLayer) != 0)
        {
            //Debug.Log("Proyectil impactó a un enemigo: " + collision.gameObject.name);

            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.health -= damage;
                //Debug.Log("Daño aplicado, vida restante: " + enemyHealth.health);
            }

            Destroy(gameObject); // El proyectil desaparece al impactar
        }
        // Si el proyectil golpea cualquier otra capa en "collisionLayers", desaparece
        else if (((1 << collisionLayer) & collisionLayers) != 0)
        {
           // Debug.Log("Proyectil impactó con: " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}

