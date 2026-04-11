using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
	[SerializeField] private float ataque = 1f;
	[SerializeField] private float velocidad = 5f;

	private Rigidbody2D rb;
	private EquipoEnum equipoEnum;

	private void Awake()
	{
    	rb = GetComponent<Rigidbody2D>();
	}

	public void AjustarEquipoEnum(EquipoEnum equipoEnum)
	{
    	this.equipoEnum = equipoEnum;
	}

	public void AjustarDireccion(Vector2 direccion)
	{
    	rb.velocity = direccion.normalized * velocidad;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
    	if (!other.gameObject.TryGetComponent<Salud>(out Salud saludDelOtro)) { return; }
    	if (!other.gameObject.TryGetComponent<Equipo>(out Equipo equipoDelOtro)) { return; }
    	if (equipoDelOtro.EquipoActual == equipoEnum) { return; }

    	saludDelOtro.PerderSalud(ataque);
    	Destroy(gameObject);
	}
}
