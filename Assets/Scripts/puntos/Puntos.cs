using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntos : MonoBehaviour
{
	[SerializeField] private TMP_Text textoPuntos;

	private int contadorPuntos = 0;

	private void Start()
	{
    	ActualizarTexto();
	}

	public void SumarPuntos(int puntos)
	{
    	contadorPuntos += puntos;
    	ActualizarTexto();
	}

	public void ActualizarTexto()
	{
    	textoPuntos.text = contadorPuntos.ToString();
	}
}
