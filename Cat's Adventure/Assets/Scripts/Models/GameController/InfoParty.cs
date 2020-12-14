using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script donde se guardaran todos los datos de la partida
/// </summary>
public static class InfoParty
{
    /// <summary>
    /// Control de si hay partida guardada o no
    /// </summary>
    public static bool thereSaveParty = false;

    /// <summary>
    /// Clase para la informacion del Player
    /// </summary>
    public static class InfoPlayer
    {
        /// <summary>
        /// Se guardara la posicion
        /// </summary>
        public static Vector2 position;

        /// <summary>
        /// Se guardara la salud
        /// </summary>
        public static int health;

        /// <summary>
        /// Se guardara el score
        /// </summary>
        public static int score;

    }

    /// <summary>
    /// Clase para la informacion de los recolectables
    /// </summary>
    public class TypeInfoRecolectables
    {
        /// <summary>
        /// Variable que verifica si es activo o no
        /// </summary>
        public bool isActive;
    }

    /// <summary>
    /// Lista que guarda la info de los recolectables
    /// </summary>
    public static List<TypeInfoRecolectables> infoRecolectables = new List<TypeInfoRecolectables>();
}
