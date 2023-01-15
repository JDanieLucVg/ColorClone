using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillRotation : MonoBehaviour
{
    //creacion de la variable de velocidad del circuito
    [SerializeField]private float rotationSpeed = 100f;

    void Start()
    {
        
    }

    void Update()
    {
        //creacion para que la rotacion sea igual en cualquier ordenador
        transform.Rotate(new Vector3(0,0,rotationSpeed * Time.deltaTime));
    }
}
