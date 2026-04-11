using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnemigo : MonoBehaviour
{
    [Header("Detectar suelo")]
    [SerializeField] private Transform Detectorsuelo;
    [SerializeField] private float distanciaAlSuelo = 0.3f;
    [SerializeField] private LayerMask layerSuelo;

    Movimiento movimiento;
    Vector2 direccionMovimiento;

    void Start()
    {
        movimiento = GetComponent<Movimiento>();
        direccionMovimiento = new Vector2(1f, 0f);
    }

    void Update()
    {
        movimiento.VoltearTransform(direccionMovimiento.x);
        DetectarSuelo();
        movimiento.Moverse(direccionMovimiento.x);
    }

    void DetectarSuelo()
    {
        RaycastHit2D hit = Physics2D.Raycast(Detectorsuelo.position, Vector2.down, distanciaAlSuelo, layerSuelo);

        if (hit.collider == null)
        {
            direccionMovimiento.x *= -1f;
        }
    }
}