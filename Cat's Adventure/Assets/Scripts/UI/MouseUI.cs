using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseUI : MonoBehaviour
{
    public Text mouseText;

    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        if(_playerMovement == null)
        {
            Debug.LogError(StringsType.PlayerMovementIsNull);
        }
        
    }

    public void UpdateMouses()
    {
        mouseText.text = _playerMovement.GetMouses().ToString();
    }
}
