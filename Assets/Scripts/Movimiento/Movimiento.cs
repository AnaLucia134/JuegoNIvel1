using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private float velocidadCaminata = 4f;
    [SerializeField] private float alturaSalto = 4.5f;
    [SerializeField] private LayerMask capaDeSalto;
    [SerializeField] private LayerMask capaDeEscalera;
    [SerializeField] private float velocidadEscalar = 3f;
    [Range(0, 1)]
    [SerializeField] private float modificadorVelSalto = 0.5f;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private Animator animator;
    private float escalaGravedad = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        escalaGravedad =rb.gravityScale;
    }

    public void Moverse(float movimientoX)
    {
        rb.velocity = new Vector2(movimientoX * velocidadCaminata, rb.velocity.y);
        animator.SetBool("isrunning", movimientoX != 0);
    }
    public void VoltearTransform(float movimientoX)
    {
        transform.localScale = new Vector2(
            Mathf.Sign(movimientoX) * Mathf.Abs(transform.localScale.x),
            transform.localScale.y
        );
    }
    public void Saltar(bool debeSaltar)
    {
        if (debeSaltar)
        {
            if (boxCollider.IsTouchingLayers(capaDeSalto))
            {
                rb.velocity = new Vector2(
                    rb.velocity.x,
                    Mathf.Sqrt(-2f * escalaGravedad * Physics2D.gravity.y * alturaSalto)
                );
            }
        }
        else
        {
            if (rb.velocity.y > Mathf.Epsilon)
            {
                rb.velocity = new Vector2(
                    rb.velocity.x,
                    rb.velocity.y * modificadorVelSalto
                );
            }
        }
    }
    public void Escalar(float movimientoY)
    {
        if (!boxCollider.IsTouchingLayers(capaDeEscalera))
        {
            Debug.Log("No toca escalera");
            rb.gravityScale = escalaGravedad;
            return;
        }

        Debug.Log("Tocando escalera");
        rb.velocity = new Vector2(rb.velocity.x, movimientoY * velocidadEscalar);
        rb.gravityScale = 0f;
    }
}
