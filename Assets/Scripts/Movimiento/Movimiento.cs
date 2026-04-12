using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private float velocidadCaminata = 4f;
    [SerializeField] private float velocidadCorrer = 7f;
    private bool estaCorriendo = false;

    [SerializeField] private float alturaSalto = 4.5f;
    [SerializeField] private int maxSaltosTotales = 3;
    [SerializeField] private LayerMask capaDeSalto;
    [SerializeField] private LayerMask capaDeEscalera;
    [SerializeField] private float velocidadEscalar = 3f;
    [Range(0, 1)]
    [SerializeField] private float modificadorVelSalto = 0.5f;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private Animator animator;
    private float escalaGravedad = 1f;
    private int saltosRestantes;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        escalaGravedad =rb.gravityScale;
    }

    void Update()
    {
        if (boxCollider.IsTouchingLayers(capaDeSalto))
        {
            // Restablecer los saltos si está en el suelo y no ascendiendo
            if (rb.velocity.y <= Mathf.Epsilon)
            {
                saltosRestantes = maxSaltosTotales;
            }
        }
        else if (saltosRestantes == maxSaltosTotales)
        {
            // Si el jugador cae de una plataforma sin saltar (pierde el salto base)
            saltosRestantes = maxSaltosTotales - 1;
        }
    }

    public void EstablecerCorrer(bool corriendo)
    {
        estaCorriendo = corriendo;
    }

    public void Moverse(float movimientoX)
    {
        float velocidadActual = estaCorriendo ? velocidadCorrer : velocidadCaminata;
        rb.velocity = new Vector2(movimientoX * velocidadActual, rb.velocity.y);
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
            bool tocandoSuelo = boxCollider.IsTouchingLayers(capaDeSalto);

            // Condición original (salto normal tocando el suelo) o salto extra (en el aire si le quedan saltos)
            if (tocandoSuelo || saltosRestantes > 0)
            {
                rb.velocity = new Vector2(
                    rb.velocity.x,
                    Mathf.Sqrt(-2f * escalaGravedad * Physics2D.gravity.y * alturaSalto)
                );
                
                // Si hizo el primer salto del suelo, igual gasta uno.
                saltosRestantes--;
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
