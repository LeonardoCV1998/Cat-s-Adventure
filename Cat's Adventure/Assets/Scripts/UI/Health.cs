using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject _life;

    [SerializeField] private List<Image> _lifes;

    private PlayerMovement _playerMovement;

    private GameController _gameController;

    private void Start()
    {
        _playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();

        if(_playerMovement == null)
        {
            Debug.LogError(StringsType.PlayerMovementIsNull);
        }

        if(_gameController == null)
        {
            Debug.LogError(StringsType.GameControllerIsNull);
        }

        _playerMovement.DamageTaken += UpdateLifes;
        _playerMovement.LifesUpgraded += AddLifes;

        _gameController.ShowLifes += RestartLifes;

        for (int i = 0; i < 3; i++)
        {
            GameObject objCurrent = Instantiate(_life, this.transform);

            _lifes.Add(objCurrent.GetComponent<Image>());
        }
    }

    void UpdateLifes()
    {
        int lifeFill = _playerMovement.GetHealth();

        foreach (Image i in _lifes)
        {
            i.fillAmount = lifeFill;
            lifeFill -= 1;
        }
    }

    void AddLifes()
    {
        foreach (Image i in _lifes)
        {
            Destroy(i.gameObject);
        }

        _lifes.Clear();

        for (int i = 0; i < _playerMovement.GetHealth(); i++)
        {
            GameObject objCurrent = Instantiate(_life, this.transform);

            _lifes.Add(objCurrent.GetComponent<Image>());
        }
    }

    void RestartLifes()
    {
        foreach (Image i in _lifes)
        {
            Destroy(i.gameObject);
        }

        _lifes.Clear();

        for (int i = 0; i < _playerMovement.GetHealth(); i++)
        {
            GameObject objCurrent = Instantiate(_life, this.transform);

            _lifes.Add(objCurrent.GetComponent<Image>());
        }
    }
}
