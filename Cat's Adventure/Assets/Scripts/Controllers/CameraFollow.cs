using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// Target que seguira
    /// </summary>
    [SerializeField] private Transform _target;

    /// <summary>
    /// Distancia de la camara al jugador
    /// </summary>
    [SerializeField] private Vector3 _offset;

    private void LateUpdate()
    {
        // La posicion deseada sera igual a la posicion del target
        // mas el offset que le des en el inspecto
        Vector3 desiredPosition = _target.position + _offset;

        // La posicion de la camara sera igual a la posicion deseada
        transform.position = desiredPosition;

        // Mirara al target en todo momento
        transform.LookAt(_target);

    }
}
