using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSalud : MonoBehaviour
{
	[SerializeField] private AudioClip sonidoRecogerItem;

	private bool fueActivado = false;
	private AudioSource audioSource;
	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
    	audioSource = GetComponent<AudioSource>();
    	spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
    	if (fueActivado) { return; }

		// Buscamos si el objeto que chocó tiene el componente Salud
    	if (collision.gameObject.TryGetComponent<Salud>(out Salud salud))
    	{
			// Verificamos que el jugador no esté muerto y que sí le falte vida para curarlo
			if (!salud.EstaMuerto() && salud.ObtenerFraccion() < 1f)
			{
        		fueActivado = true;
        		salud.CurarCompletamente();
        		
				ReproducirSonido();
        		spriteRenderer.enabled = false;
				
				// Se destruye después de que suene por completo
				float duracion = sonidoRecogerItem != null ? sonidoRecogerItem.length : 0f;
        		Destroy(gameObject, duracion);
			}
    	}
	}

	private void ReproducirSonido()
	{
    	if (sonidoRecogerItem == null) { return; }
    	audioSource.PlayOneShot(sonidoRecogerItem);
	}
}