using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Equipo))]
public class ContactoAtaque : MonoBehaviour
{
	[SerializeField] private float cantAtaque = 1f;
	private Equipo equipo;

	void Awake()
	{
    	equipo = GetComponent<Equipo>();
	}

	private void OnCollisionStay2D(Collision2D other)
	{
    	if (!other.gameObject.TryGetComponent<Salud>(out Salud saludDelOtro)) { return; }
    	if (!other.gameObject.TryGetComponent<Equipo>(out Equipo equipoDelOtro)) { return; }
    	if (equipoDelOtro.EquipoActual == equipo.EquipoActual) { return; }

    	saludDelOtro.PerderSalud(cantAtaque);
	}
}
