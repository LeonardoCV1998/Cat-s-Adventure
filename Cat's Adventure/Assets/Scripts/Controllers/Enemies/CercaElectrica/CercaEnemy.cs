using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CercaEnemy : EnemyParent
{
    /// <summary>
    /// Rayos que se activaran y desactivaran.
    /// </summary>
    [SerializeField] private GameObject rayos;

    /// <summary>
    /// Scriptable Object para obtener datas.
    /// </summary>
    public CercaScriptableObject cercaScriptableObject;

    /// <summary>
    /// BoxCollider del gameobject.
    /// </summary>
    private BoxCollider2D _collider;

    /// <summary>
    /// Tiempo actual para activar los rayos actuales.
    /// </summary>
    private float _timeToActiveRayosCurrent;

    /// <summary>
    /// Tiempo actual para desactivar los rayos.
    /// </summary>
    private float _timeToDesactiveRayosCurrent;

    /// <summary>
    /// Variable que controla cuando puede hacer daño y cuando no.
    /// </summary>
    public bool _canDamage = false;

    private void Start()
    {
        // Se obtiene el componente de BoxCollider2D
        _collider = GetComponent<BoxCollider2D>();

        // Se revisa si no es null
        if(_collider == null)
        {
            // En caso de que sea null mandara el mensaje en error
            Debug.LogError(StringsType.Collider2DIsNull);
        }

        // Se le asigna el tiempo al tiempo actual
        _timeToActiveRayosCurrent = cercaScriptableObject.timeToActiveRayos;
        _timeToDesactiveRayosCurrent = cercaScriptableObject.timeToDesactiveRayos;
    }

    private void Update()
    {
        // Se hace mencion al metodo sobrescrito
        Behaviour();
    }

    /// <summary>
    /// Metodo sobrescrito del padre
    /// </summary>
    protected override void Behaviour()
    {
        // Se resta el tiempo para activar los rayos
        _timeToActiveRayosCurrent -= Time.deltaTime;

        // En caso de que llegue a cero 
        if (_timeToActiveRayosCurrent <= 0)
        {
            // Se activan los rayos
            rayos.SetActive(true);

            // Se enciende el collider
            _collider.enabled = true;

            // Puede hacer daño al jugador
            _canDamage = true;

            // Se resta el tiempo para desactivar los rayos
            _timeToDesactiveRayosCurrent -= Time.deltaTime;

            // Si el tiempo llega a 0
            if(_timeToDesactiveRayosCurrent <= 0)
            {
                // Se desactivan los rayos
                rayos.SetActive(false);

                // Se desactiva el collider
                _collider.enabled = false;
                
                // Se reasignan los tiempos maximos
                _timeToActiveRayosCurrent = cercaScriptableObject.timeToActiveRayos;
                _timeToDesactiveRayosCurrent = cercaScriptableObject.timeToDesactiveRayos;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el collision es igual a la etiqueta de Player
        if(collision.tag.Equals("Player"))
        {
            // Se obtiene el componente de PlayerMovement
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

            // Si no es null
            if(playerMovement != null)
            {
                // Si puede hacer daño
                if (_canDamage)
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
