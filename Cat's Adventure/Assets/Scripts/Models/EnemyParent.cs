using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyParent : MonoBehaviour
{
    /// <summary>
    /// Salud del enemigo
    /// </summary>
    [SerializeField] protected int life;

    /// <summary>
    /// Daño que causara al Player
    /// </summary>
    [SerializeField] protected int damage;

    /// <summary>
    /// Metodo abstracto que utilizaran todos los enemigos
    /// pero de forma diferente
    /// </summary>
    protected abstract void Behaviour();

}
