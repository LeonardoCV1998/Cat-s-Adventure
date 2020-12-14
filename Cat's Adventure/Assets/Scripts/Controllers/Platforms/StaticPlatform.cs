using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPlatform : MonoBehaviour
{
    /// <summary>
    /// Variable que guardara el tiempo para bajar de la platform
    /// </summary>
    [SerializeField] private float _waitTime = 0.5f;

    /// <summary>
    /// Componente de PlatformEffector2D
    /// </summary>
    private PlatformEffector2D _platfformEfector2D;

    private bool _playerIsOn = false;

    private void Start()
    {
        // Se obtiene el componente de PlatformEffector2D
        _platfformEfector2D = GetComponent<PlatformEffector2D>();

        // Si en Null
        if(_platfformEfector2D == null)
        {
            // Se manda un error de que no se encontro
            Debug.LogError(StringsType.PlatfformEfecto2DIsNull);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            _waitTime = 0.5f;
        }

        if(_playerIsOn)
        {
            // Si el jugador presiona la tecla S o la flecha para abajo
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                // Se espera a que el tiempo de espera sea 0
                if (_waitTime <= 0)
                {
                    // Cambia la rotacion del Offset para que baje el jugador de la plataforma
                    _platfformEfector2D.rotationalOffset = 180f;

                    // Regresa el valor a 0.5
                    _waitTime = 0.5f;
                }
                else
                {
                    // Resta el tiempo
                    _waitTime -= Time.deltaTime;
                }
            }

            
        }

        // Cuando el jugador tecle el salto
        if (Input.GetKey(KeyCode.Space))
        {
            // El offset sera a 0 para que pueda saltar por medio de la plataforma
            _platfformEfector2D.rotationalOffset = 0;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _playerIsOn = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerIsOn = false;
        }
    }
}
