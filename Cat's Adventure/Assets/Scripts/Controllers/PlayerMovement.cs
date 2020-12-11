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

    /// <summary>
    /// Control del animator del player
    /// </summary>
    private Animator _animator;


    void Start()
    {
        // Se obtienen los componente de PlayerController
        _playerController = GetComponent<PlayerController>();

        // Se obtienen los componente de Animator
        _animator = GetComponent<Animator>();

        #region CHECK_COMPONENTS_EXITS

        // Se revisa si los componentes a los cuales se quieren obtener existan, 
        // si no existen se debugeara un error

        if (_playerController == null)
        {
            Debug.LogError(StringsType.PlayerControllerIsNull);
        }

        if(_animator == null)
        {
            Debug.LogError(StringsType.AnimatorIsNull);
        }

        #endregion
    }

    void FixedUpdate()
    {
        // Se manda a llamar en todo momento el metodo de movement 
        // para controlar al player
        Movement();

        #region CHECK_INPUTS

        // Chequeo de presion de teclas
        if (Input.GetKeyDown(KeyCode.Space))
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

        #endregion
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

        // Se edita el parametro del animator Speed para controlar la animacion
        // Se manda el Horizontal Input en Abs para obtener el valor siempre en positivo
        _animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // Se manda a llamar el metodo de Move y se le pasa la direccion y velocidad
        _playerController.Move(horizontalInput * Time.fixedDeltaTime, false, _jump, _slide);

    }

    /// <summary>
    /// Sistema de salto
    /// </summary>
    private void Jump()
    {
        _jump = true;
        _animator.SetBool("IsJumping", true);
    }

    /// <summary>
    /// Sistema de slide
    /// </summary>
    private void Slide()
    {
        // Se ajusta _slide a true ya que presiono la tecla
        _slide = true;

        // Se envia un true al parametro de IsSlide para la animacion
        _animator.SetBool("IsSlide", true);

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

        // Se envia un false al parametro de IsSlide para la animacion
        _animator.SetBool("IsSlide", false);
    }

    private void Attack()
    {
        Debug.Log("Attacking");
    }

    public void OnLanding()
    {
        _animator.SetBool("IsJumping", false);
    }
}
