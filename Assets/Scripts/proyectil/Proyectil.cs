using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
	[SerializeField] private float ataque = 1f;
	[SerializeField] private float velocidad = 5f;
	[SerializeField] private float tiempoDeVida = 3f; // Tiempo antes de destruirse automáticamente

	private Rigidbody2D rb;
	private EquipoEnum equipoEnum;

	private void Awake()
	{
    	rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		// Destruir el proyectil después del tiempo de vida establecido
		Destroy(gameObject, tiempoDeVida);
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
		// 1. Si el objeto pertenece a nuestro propio equipo, evitamos chocar (atraviesa al jugador)
		Equipo equipoDelOtro = other.gameObject.GetComponentInParent<Equipo>();
		if (equipoDelOtro != null && equipoDelOtro.EquipoActual == equipoEnum) { return; }

		// 2. Si chocó contra algo que SÍ tiene vida (como un enemigo), le bajamos la vida
		if (other.gameObject.TryGetComponent<Salud>(out Salud saludDelOtro))
		{
			saludDelOtro.PerderSalud(ataque);
		}

		// 3. Se destruye al chocar contra cualquier otra cosa (esto incluye enemigos y el Tilemap/suelo)
    	Destroy(gameObject);
	}
}
