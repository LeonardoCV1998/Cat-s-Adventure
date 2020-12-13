using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    /// <summary>
    /// Se utilizara para obtener los componentes de SprayEnemy
    /// </summary>
    private SprayEnemy _sprayEnemy;

    /// <summary>
    /// Control de cuando puede hacer daño y cuando no
    /// </summary>
    private bool _canDamage = false;

    private void Start()
    {
        // Obtener los componentes de SprayEnemy
        _sprayEnemy = transform.parent.GetComponent<SprayEnemy>();

        #region CHECK_COMPONENTS

        if(_sprayEnemy == null)
        {
            Debug.LogError(StringsType.SprayEnemyIsNull);
        }

        #endregion
    }
    
    /// <summary>
    /// Funcion de cuando se activa el objecto
    /// </summary>
    private void OnEnable()
    {
        // Puede hacer daño
        _canDamage = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Si colisiona con el Player
        if(collision.tag.Equals("Player"))
        {
            // Se obtiene el componente de PlayerMovement del objeto que colisiono
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

            // Si no es null
            if(playerMovement != null)
            {
                // Si puede hacer daño?
                if(_canDamage)
                {
                    // Hara el daño al jugador
                    playerMovement.ReciveDamage(_sprayEnemy.GetDamage());

                    // Ya no puede hacer daño hasta que vuelva a activarse
                    _canDamage = false;
                }
            }
        }
    }
}
