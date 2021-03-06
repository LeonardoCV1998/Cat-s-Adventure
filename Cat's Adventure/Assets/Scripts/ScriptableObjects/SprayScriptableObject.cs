﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object del spray para guardar data
/// </summary>
[CreateAssetMenu(fileName = "Spray Enemy", menuName = "Scriptable Objects/ New Spray Enemy")]
public class SprayScriptableObject : ScriptableObject
{
    /// <summary>
    /// Tiempo para iniciar el spray
    /// </summary>
    public float timeToSpray = 3f;

    /// <summary>
    /// Tiempo en que tarda sprayeando
    /// </summary>
    public float timeSpraying = 2f;

    /// <summary>
    /// Variable para obtener los states de Spray
    /// </summary>
    public SprayStates sprayStates;
}
