using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.25f;
    public float sprintForce = 1.5f;
    float moveHorizontal;

    public float jumpForce = 5f;

    public GameObject groundRayObject;

    public bool isGrounded;
    public Rigidbody2D rb;

    public LayerMask floor, oneWay;
    [SerializeField] float raySize = 10f;
    [SerializeField] Vector2 boxSize = new Vector2(0.8f, 0.1f); // Ancho y altura del BoxCast

    Animator animator;

    [SerializeField] Vector2 hitBox;
    [SerializeField] LayerMask enemyLayer;
    public float hitDistance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerInputs();
        Jumplimit();
        AttackAnimation();
    }

    void PlayerInputs()
    {

        //Difinir inputs
        moveHorizontal = Input.GetAxis("Horizontal"); // A/D o las flechas izq/der para moverse
        if (moveHorizontal < 0)
        {
            animator.SetFloat("xVelocity", -1f);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (moveHorizontal > 0)
        {
            animator.SetFloat("xVelocity", 1f);
            transform.localScale = new Vector2(1, 1);
        }
        animator.SetFloat("xVelocity", (rb.velocity.x));
        animator.SetFloat("xVelocity", (-rb.velocity.x));

        //Crear vector de movimiento
        Vector2 movement = new Vector2(moveHorizontal, 0f);

        //Movimiento GameObject
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);


        animator.SetBool("isRunning", false);
        //sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector2 sprint = new Vector2(moveHorizontal, 0f);
            rb.velocity = new Vector2(moveHorizontal * moveSpeed * sprintForce, rb.velocity.y);
            animator.SetBool("isRunning", true);
        }

        animator.SetBool("isJumping", false);
        animator.SetBool("isJumping", !isGrounded);

        // Verificar si está corriendo (pero sin sobrescribir el salto)
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && moveHorizontal != 0 && isGrounded;
        animator.SetBool("isRunning", isRunning);

        // Verificar si está saltando (prioridad sobre correr)
        if (!isGrounded)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isRunning", false); // Asegurar que no se mantenga la animación de correr
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        //Input de salto 
        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            //Fuerza de salto
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("isJumping", true);
        }

        if ((Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift) && isGrounded == true))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * 1.11f);
            animator.SetBool("isJumping", true);
        }
    }
    public void Jumplimit()
    {
        // Definir tamaño del BoxCast (ajustar según el tamaño del personaje)


        // Crear el BoxCast en lugar del Raycast
        RaycastHit2D hitGround = Physics2D.BoxCast(
            groundRayObject.transform.position,
            boxSize,
            0f,
            Vector2.down,
            raySize,
            floor | oneWay
        );

        // Dibujar el BoxCast para depuración en la escena
        Color boxColor = (hitGround.collider != null) ? Color.green : Color.red;
        Debug.DrawRay(groundRayObject.transform.position + new Vector3(-boxSize.x / 2, 0), Vector2.down * raySize, boxColor);
        Debug.DrawRay(groundRayObject.transform.position + new Vector3(boxSize.x / 2, 0), Vector2.down * raySize, boxColor);

        // Verificar si el personaje realmente está en el suelo
        isGrounded = hitGround.collider != null;
    }
    public void AttackAnimation()
    {
        if (Input.GetKey(KeyCode.L))
        {
            animator.SetBool("isAttacking", true);
        }
    }
    public void EndAttack()
    {
        animator.SetBool("isAttacking",  false);  
    }

   public void RangeAnimation()
    {
        if (Input.GetKey(KeyCode.P))
        {
            animator.SetBool("isRanged", true);
        }
    }
}


