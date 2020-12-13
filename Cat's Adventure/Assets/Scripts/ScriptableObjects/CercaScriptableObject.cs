using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object de la cerca para guardar data.
/// </summary>
[CreateAssetMenu(fileName = "Cerca Enemy", menuName = "Scriptable Objects/ New Cerca Enemy")]
public class CercaScriptableObject : ScriptableObject
{
    /// <summary>
    /// Tiempo para activar los rayos game object.
    /// </summary>
    public float timeToActiveRayos = 3.0f;

    /// <summary>
    /// Tiempo para desactivar los rayos game object.
    /// </summary>
    public float timeToDesactiveRayos = 1.0f;
    
}
