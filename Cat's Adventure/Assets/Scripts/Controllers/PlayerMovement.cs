using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Control de la velocidad de correr
    /// </summary>
    [SerializeField] private float _runSpeed = 40f;

    /// <summary>
    /// Control de si salto o no
    /// </summary>
    private bool _jump = false;

    /// <summary>
    /// Control si hizo el slide o no
    /// </summary>
    private bool _slide = false;
    
    /// <summary>
    /// Controles del Player.
    /// </summary>
    private PlayerController _playerController;


    void Start()
    {
        // Se obtienen los componente de PlayerController
        _playerController = GetComponent<PlayerController>();

        // En caso de que sea NULL
        if(_playerController == null)
        {
            // Se debugueara el error de que no se encontro el componente
            Debug.Log(StringsType.PlayerControllerIsNull);
        }
    }

    void Update()
    {
        // Se manda a llamar en todo momento el metodo de movement 
        // para controlar al player
        Movement();

        // Chequeo de presion de teclas
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        else
        {
            _jump = false;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Slide();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
    }

    /// <summary>
    /// Movimiento del Player: 
    ///     LeftArrow o A => Movimiento hacia la izquierda, 
    ///     RightArrow o D => Movimiento hacia la derecha.
    /// </summary>
    private void Movement()
    {
        // Entrada de la tecla, devuelve un -1, 0 o 1 dependiendo si va hacia la izquierda, idle o derecha,
        // por la velocidad que tendra el jugador
        float horizontalInput = Input.GetAxisRaw(StringsType.HorizontalInput) * _runSpeed;

        // Se manda a llamar el metodo de Move y se le pasa la direccion y velocidad
        _playerController.Move(horizontalInput * Time.fixedDeltaTime, false, _jump, _slide);

    }

    /// <summary>
    /// Sistema de salto
    /// </summary>
    private void Jump()
    {
        _jump = true;
    }

    /// <summary>
    /// Sistema de slide
    /// </summary>
    private void Slide()
    {
        // Se ajusta _slide a true ya que presiono la tecla
        _slide = true;
        
        // Se inicia la coroutina para parar el slide
        StartCoroutine(StopSlider());
    }

    /// <summary>
    /// Coroutina para parar el slide
    /// </summary>
    /// <returns></returns>
    private IEnumerator StopSlider()
    {
        // Tiempo del slide
        yield return new WaitForSeconds(_playerController.GetSlideTimeSpeed());

        // Se hace falso la variable de slide para que ya no lo haga
        _slide = false;
    }

    private void Attack()
    {
        Debug.Log("Attacking");
    }
}
