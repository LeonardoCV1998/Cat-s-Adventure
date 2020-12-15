using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    /// <summary>
    /// Objeto que aparecera cuando este game over
    /// </summary>
    [SerializeField] private GameObject _gameOver;

    /// <summary>
    /// Variable que guardara el componente de GameController
    /// </summary>
    private GameController _gameController;

    private void Awake()
    {
        // Se obtiene el componente de GameController
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();

        // Si es igual a nulo
        if (_gameController == null)
        {
            // Se debugeara un error
            Debug.LogError(StringsType.GameControllerIsNull);
        }
        
    }

    /// <summary>
    /// Metodo de GameOver para activar el letrero y boton
    /// </summary>
    public void GameOver()
    {
        // Se activa letrero
        _gameOver.SetActive(true);

        // Se pausa el tiempo del juego
        Time.timeScale = 0;
    }

    /// <summary>
    /// Metodo del boton de Game Over
    /// </summary>
    public void OnGameOverButtonDown()
    {
        // Se despausa el tiempo del juego
        Time.timeScale = 1;

        // Se desactiva el letrero
        _gameOver.SetActive(false);

        // Se carga la partida
        _gameController.LoadParty();
    }

    /// <summary>
    /// Metodo para ir al menu principal
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
