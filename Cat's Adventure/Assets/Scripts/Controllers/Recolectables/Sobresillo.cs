using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sobresillo : MonoBehaviour
{
    /// <summary>
    /// Puntos de los cuales estara haciendo el efecto de ping pong.
    /// </summary>
    [SerializeField] private Transform _pointA, _pointB;

    /// <summary>
    /// Posicion inicial del transform.
    /// </summary>
    [SerializeField] private Transform _startPos;

    /// <summary>
    /// Seguiente posicion a la que ira.
    /// </summary>
    private Vector3 _nextPos;

    private void Update()
    {
        // Si el transform se encuentra en la posicion A
        if (transform.position == _pointA.position)
        {
            // La siguiente posicion sera la posicion B
            _nextPos = _pointB.position;
        }

        // Si el transform se encuentra en la posicion B
        if (transform.position == _pointB.position)
        {
            // La siguiente posicion sera la posicion A
            _nextPos = _pointA.position;
        }

        // Se mueve el transform dependiendo de cual es la  
        // siguiente posicion y con respecto a la velocidad
        transform.position = Vector3.MoveTowards(transform.position, _nextPos, 1 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si colisiona con el Player
        if(collision.tag.Equals(StringsType.PlayerTag))
        {
            // Manda a llamar el componente de PlayerMovement
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

            // Si es diferente de null
            if(playerMovement != null)
            {
                // Manda a llamar el metodo de Player de Power Up
                playerMovement.PowerUp();

                // Y despues se destruye
                Destroy(this.transform.parent.gameObject);
            }
        }
    }
}
