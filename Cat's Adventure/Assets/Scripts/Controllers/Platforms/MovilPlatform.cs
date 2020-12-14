using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovilPlatform : MonoBehaviour
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
    /// Velocidad con la cual se movera la plataforma
    /// </summary>
    [SerializeField] private float _speed;

    /// <summary>
    /// Variable que guardara el player para hacerlo hijo
    /// </summary>
    private PlayerMovement _playerTransform;

    /// <summary>
    /// Seguiente posicion a la que ira.
    /// </summary>
    private Vector3 _nextPos;

    /// <summary>
    /// Componente de PlatformEffector2D
    /// </summary>
    private PlatformEffector2D _platfformEfector2D;

    private void Awake()
    {
        // Obtener los componentes de PlatformEffector2D
        _platfformEfector2D = GetComponent<PlatformEffector2D>();

        // Obtener los componente de Player de Transform
        _playerTransform = GameObject.Find("Player").GetComponent<PlayerMovement>();

        // Si es null
        if(_playerTransform == null)
        {
            // Debugear un error de que no se encontro
            Debug.LogError(StringsType.PlayerMovementIsNull);
        }

        // Si es null
        if (_platfformEfector2D == null)
        {
            // Debugear un error de que no se encontro
            Debug.LogError(StringsType.PlatfformEfecto2DIsNull);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    /// <summary>
    /// Movimiento de la plataforma
    /// </summary>
    private void Movement()
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
        transform.position = Vector3.MoveTowards(transform.position, _nextPos, _speed * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Si el obj que colisiono es Player
        if(collision.gameObject.tag == "Player")
        {
            // Se hara hijo de este objeto
            _playerTransform.transform.parent = this.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Si el objeto que salio de la colision es Player
        if (collision.gameObject.tag == "Player")
        {
            // Ya no tendra hijo
            _playerTransform.transform.parent = null;
        }
    }
}
