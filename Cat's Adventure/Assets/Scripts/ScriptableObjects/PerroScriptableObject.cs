using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object del perro para guardar data.
/// </summary>
[CreateAssetMenu(fileName = "Perro Enemy", menuName = "Scriptable Objects/ New Perro Enemy")]
public class PerroScriptableObject : ScriptableObject
{
    /// <summary>
    /// Velocidad con la que hara el recorrido.
    /// </summary>
    public float speed;

    /// <summary>
    /// Tiempo en el cual durara para hacer daño de nuevo.
    /// </summary>
    public float timeToDamage = 2.0f;
}
