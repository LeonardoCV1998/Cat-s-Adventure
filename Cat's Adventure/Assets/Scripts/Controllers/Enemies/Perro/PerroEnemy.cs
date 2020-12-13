using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerroEnemy : EnemyParent
{
    /// <summary>
    /// Acceso al ScriptableObject
    /// </summary>
    public PerroScriptableObject perroScriptableObject;

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

    /// <summary>
    /// Variable que controla cuando puede hacer daño y cuando no
    /// </summary>
    private bool _canDamage = false;

    /// <summary>
    /// Tiempo actual para que pueda hacer daño
    /// </summary>
    private float _timeToDamageCurrent;

    void Start()
    {
        // Iniciamos la posicion donde esta el transform 
        // como la inicial
        _nextPos = _startPos.position;

        // Se le da el valor a el tiempo current del tiempo total para hacer daño
        _timeToDamageCurrent = perroScriptableObject.timeToDamage;
    }

    void Update()
    {
        // Metodo de comportamiento abstracto
        Behaviour();

        // Si no puede hacer daño
        if(!_canDamage)
        {
            // Se le resta al tiempo current para que pueda hacer daño
            _timeToDamageCurrent -= Time.deltaTime;

            // Si llega a cero
            if(_timeToDamageCurrent <= 0)
            {
                // Puede hacer daño
                _canDamage = true;

                // Y se le asigna de nuevo el tiempo total
                _timeToDamageCurrent = perroScriptableObject.timeToDamage;
            }
        }
    }

    /// <summary>
    /// Se hara el comportamiento sobrescribiendo este metodo
    /// heredado de la clase padre.
    /// </summary>
    protected override void Behaviour()
    {
        // Si el transform se encuentra en la posicion A
        if(transform.position == _pointA.position)
        {
            // La siguiente posicion sera la posicion B
            _nextPos = _pointB.position;
        }

        // Si el transform se encuentra en la posicion B
        if(transform.position == _pointB.position)
        {
            // La siguiente posicion sera la posicion A
            _nextPos = _pointA.position;
        }

        // Se mueve el transform dependiendo de cual es la  
        // siguiente posicion y con respecto a la velocidad
        transform.position = Vector3.MoveTowards(transform.position, _nextPos, perroScriptableObject.speed * Time.deltaTime);
    }

    public void ReciveDamage(int damage)
    {
        life -= damage;

        if(life < 1)
        {
            Destroy(this.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si entra el player
        if(collision.tag.Equals("Player"))
        {
            // Se obtiene componente de PlayerMovement
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

            // Si es diferente a null
            if(playerMovement != null)
            {
                // Si puede hacer daño
                if(_canDamage)
                {
                    // Hara daño al jugador mandando a llamar su metodo y su daño
                    playerMovement.ReciveDamage(damage);

                    // Ya no puede hacer daño
                    _canDamage = false;
                }
            }
        }
    }
}
