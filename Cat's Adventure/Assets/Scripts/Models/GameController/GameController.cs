using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

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

    /// <summary>
    /// Obj del texto de pausar
    /// </summary>
    [SerializeField] private GameObject _pauseText;

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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            _pauseText.SetActive(true);
            Time.timeScale = 0;
        }
        
    }

    /// <summary>
    /// Metodo para continuar jugando
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1;
        _pauseText.SetActive(false);
    }

    /// <summary>
    /// Metodo para ir al menu principal
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Metodo para guardar la partida
    /// </summary>
    public void SaveParty()
    {
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
        dataContainer.mouses = _player.GetMouses();

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
            _player.SetMouses(dataContainer.mouses);

            // Se revisa que el delegado no sea null
            if (ShowLifes != null)
            {
                // Ejecuta el evento de mostrar las vidas
                ShowLifes();

            }

            // Se obtine el componente de MouseUI del gameobject
            MouseUI mouseUI = GameObject.FindGameObjectWithTag("MouseUI").GetComponent<MouseUI>();

            // Se actualiza al cargar el texto de mouses
            mouseUI.mouseText.text = dataContainer.mouses.ToString();
        }
    }

    public void GameFinish()
    {
        
        StartCoroutine(TimeToMainMenu());
    }

    private IEnumerator TimeToMainMenu()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("UIDesign");
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
    /// Mouses del jugador guardada
    /// </summary>
    public int mouses;

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
