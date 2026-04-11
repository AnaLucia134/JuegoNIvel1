using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolaMundo : MonoBehaviour
{
    [SerializeField] private string mensaje = "Hola mundo";

    void Start()
    {
    }

    public void Saludar()
    {
        Debug.Log(mensaje);
    }
}
