using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    /// <summary>
    /// Obj de la vida
    /// </summary>
    [SerializeField] private GameObject _life;

    /// <summary>
    /// Lista donde guardara las vidas
    /// </summary>
    [SerializeField] private List<Image> _lifes;

    /// <summary>
    /// Variable donde se guardara el PlayerMovement
    /// </summary>
    private PlayerMovement _playerMovement;

    /// <summary>
    /// Variable donde se guardara el GameController
    /// </summary>
    private GameController _gameController;

    private void Start()
    {
        // Se busca y se obtiene el componente
        _playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();

        // Se revisa si son nulos para mandar un error
        if(_playerMovement == null)
        {
            Debug.LogError(StringsType.PlayerMovementIsNull);
        }

        if(_gameController == null)
        {
            Debug.LogError(StringsType.GameControllerIsNull);
        }

        // Se subscriben los metodos a los eventos
        _playerMovement.DamageTaken += UpdateLifes;
        _playerMovement.LifesUpgraded += AddLifes;
        _gameController.ShowLifes += RestartLifes;

        // Se recorre un for para instanciar las vidas al inicio
        for (int i = 0; i < 3; i++)
        {
            GameObject objCurrent = Instantiate(_life, this.transform);

            _lifes.Add(objCurrent.GetComponent<Image>());
        }
    }

    /// <summary>
    /// Metodo de actualizacion de vidas
    /// </summary>
    void UpdateLifes()
    {
        // Se ontiene la vida del jugador
        int lifeFill = _playerMovement.GetHealth();

        // Se eliminan vidas
        foreach (Image i in _lifes)
        {
            i.fillAmount = lifeFill;
            lifeFill -= 1;
        }
    }

    /// <summary>
    /// Metodo para agregar vidas
    /// </summary>
    void AddLifes()
    {
        // Todas las vidas creadas se eliminan
        foreach (Image i in _lifes)
        {
            Destroy(i.gameObject);
        }

        // Se limpia la lista
        _lifes.Clear();

        // Se vuelven a generar las vidas dependiendo de la vida del jugador
        for (int i = 0; i < _playerMovement.GetHealth(); i++)
        {
            GameObject objCurrent = Instantiate(_life, this.transform);

            _lifes.Add(objCurrent.GetComponent<Image>());
        }
    }

    /// <summary>
    /// Metodo para reiniciar las vidas
    /// </summary>
    void RestartLifes()
    {
        // Se destruyen todas las vidas
        foreach (Image i in _lifes)
        {
            Destroy(i.gameObject);
        }

        // Se limpia la lista
        _lifes.Clear();

        // Se reinician las vidas dependiendo de la vida del jugador
        for (int i = 0; i < _playerMovement.GetHealth(); i++)
        {
            GameObject objCurrent = Instantiate(_life, this.transform);

            _lifes.Add(objCurrent.GetComponent<Image>());
        }
    }
}
