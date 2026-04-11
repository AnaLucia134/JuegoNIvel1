using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCuadrado : MonoBehaviour
{
    HolaMundo holamundo;
    // Start is called before the first frame update
    void Start()
    {
        holamundo = GetComponent<HolaMundo>();
        holamundo.Saludar();
    }

}
