using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPuntos : MonoBehaviour
{
	[SerializeField] private int puntosBrindados = 100;
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

    	if (collision.gameObject.TryGetComponent<Puntos>(out Puntos puntos))
    	{
        	fueActivado = true;
        	puntos.SumarPuntos(puntosBrindados);
        	ReproducirSonido();
        	spriteRenderer.enabled = false;
        	Destroy(gameObject,sonidoRecogerItem.length);
    	}
	}

	private void ReproducirSonido()
	{
    	if(sonidoRecogerItem == null) { return; }

    	audioSource.PlayOneShot(sonidoRecogerItem);
	}

}
