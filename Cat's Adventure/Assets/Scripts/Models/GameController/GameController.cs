using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// Variable para acceder al componente de PlayerMovement
    /// </summary>
    private PlayerMovement _player;

    /// <summary>
    /// Delegado para hacer uso del evento de ShowLifes
    /// </summary>
    public event Action ShowLifes;

    /// <summary>
    /// Variable para controlar si es nueva partida o no
    /// </summary>
    public static bool isNewParty = true;

    private void Awake()
    {
        // Se obtiene los componente del objeto Player, PlayerMovement
        _player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        // En caso de que sea null
        if(_player == null)
        {
            // Debugeara un error
            Debug.LogError(StringsType.PlayerMovementIsNull);
        }
        
    }

    private void Start()
    {
        // Preguntar si es partida nueva
        if(isNewParty)
        {
            // Se crea una nueva instancia de Binary Formatter
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            // Se abre el archivo
            FileStream fileStream = File.Open(Application.persistentDataPath +
                StringsType.FileStreamPath, FileMode.Create);

            // Se crea una nueva instancia de DataContainer
            DataContainer dataContainer = new DataContainer();

            // Se editan las variables del data container
            dataContainer.health = 3;
            dataContainer.posX = 0;
            dataContainer.posY = 0;
            //dataContainer.score = _player.GetScore();

            // Se serializan las variables
            binaryFormatter.Serialize(fileStream, dataContainer);
        }
        else
        {
            // Si no es partida nueva
            LoadParty();

        }
    }

    /// <summary>
    /// Metodo para guardar la partida
    /// </summary>
    public void SaveParty()
    {
        //// Limpiamos la lista para que no nos añada nuevos
        //DataContainer.infoRecolectables.Clear();

        //// Se busca el objeto que se llame recolectables de la scene
        //Transform recolectables = GameObject.Find("Recolectables").transform;

        //// Se pasa por cada uno de los obj
        //foreach (Transform rec in recolectables)
        //{
        //    // Se obtiene la informacion de si estan activos o no
        //    DataContainer.TypeInfoRecolectables itemRec = new DataContainer.TypeInfoRecolectables
        //    {
        //        isActive = rec.gameObject.activeSelf
        //    };

        //    // Se añade a la lista
        //    DataContainer.infoRecolectables.Add(itemRec);
        //}





        // Se crea una nueva instancia de BinaryFormatter
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        // Se crea el archivo dentro de la path puesta
        FileStream fileStream = File.Open(Application.persistentDataPath +
            StringsType.FileStreamPath, FileMode.Create);

        // Se crea una instancia de DataContainer
        DataContainer dataContainer = new DataContainer();

        // De ajustan los valores de las variables dependiendo de las del Player
        dataContainer.health = _player.GetHealth();
        dataContainer.posX = _player.GetPositionX();
        dataContainer.posY = _player.GetPositionY();
        //dataContainer.score = _player.GetScore();

        // Se serailizan los datos
        binaryFormatter.Serialize(fileStream, dataContainer);

        // Se cierra el archivo
        fileStream.Close();
    }

    /// <summary>
    /// Metodo para cargar la partida
    /// </summary>
    public void LoadParty()
    {
        ////Se busca el objeto que se llame recolectables de la scene
        //Transform recolectables = GameObject.Find("Recolectables").transform;

        //int i = 0;

        //// Se pasa por cada uno de los obj
        //foreach (Transform rec in recolectables)
        //{
        //    rec.gameObject.SetActive(DataContainer.infoRecolectables[i++].isActive);
        //}





        // Si existe el archivo 
        if (File.Exists(Application.persistentDataPath + StringsType.FileStreamPath))
        {
            // Se crea una nueva instancia de BinaryFormatter
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            // Se abre el archivo 
            FileStream fileStream = File.Open(Application.persistentDataPath +
                StringsType.FileStreamPath, FileMode.Open);

            // Se deserializan las variables
            DataContainer dataContainer = (DataContainer)binaryFormatter.Deserialize(fileStream);

            // Se cierra el archivo
            fileStream.Close();

            // Se ajustan las variables del Player a las que se guardaron
            _player.SetHealth(dataContainer.health);
            _player.SetPosition(dataContainer.posX, dataContainer.posY);
            //_player.SetScore(dataContainer.score);

            // Se revisa que el delegado no sea null
            if (ShowLifes != null)
            {
                // Ejecuta el evento de mostrar las vidas
                ShowLifes();

            }
        }

    }
}

/// <summary>
/// Contenedor de datos
/// </summary>
[Serializable]
public class DataContainer
{
    /// <summary>
    /// Vida del jugador guardada
    /// </summary>
    public int health;

    /// <summary>
    /// Score del jugador guardada
    /// </summary>
    public int score;

    /// <summary>
    ///  Posicion en X del jugador guardada
    /// </summary>
    public float posX;

    /// <summary>
    /// Posicion en Y del jugador guardada
    /// </summary>
    public float posY;

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
