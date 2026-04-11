using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlJugador : MonoBehaviour
{
    private Movimiento movimiento;
    private LanzaProyectiles lanzaProyectiles;
    private Vector2 entradacontrol;
    // Start is called before the first frame update
    void Start()
    {
        movimiento = GetComponent<Movimiento>();
        lanzaProyectiles = GetComponent<LanzaProyectiles>();
    }

    // Update is called once per frame
    void Update()
    {
        movimiento.Moverse(entradacontrol.x);
        movimiento.Escalar(entradacontrol.y);

        if (Mathf.Abs(entradacontrol.x) > Mathf.Epsilon)
        {
            movimiento.VoltearTransform(entradacontrol.x);
        }
    }
    public void AlMoverse(InputAction.CallbackContext context)
    {
        entradacontrol = context.ReadValue<Vector2>();
    }
    public void AlSaltar(InputAction.CallbackContext context)
    {
        movimiento.Saltar(context.action.triggered);
    }
    public void AlLanzar(InputAction.CallbackContext context)
	{
    	if (!context.action.triggered) { return; }
    	lanzaProyectiles.Lanzar();
	}

}
