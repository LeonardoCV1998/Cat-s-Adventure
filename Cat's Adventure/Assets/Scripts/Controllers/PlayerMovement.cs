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
    /// Vida del jugador, seran 3 vidas en total.
    /// </summary>
    [SerializeField] private int _health = 3;

    /// <summary>
    /// Punto de ataque en el que detectara a los enemigos
    /// </summary>
    [SerializeField] private Transform _attackPoint;

    /// <summary>
    /// Rango de ataque en el que le hara daño a los enemigos
    /// </summary>
    [SerializeField] private float _attackRange = 0.5f;

    /// <summary>
    /// Layers para detectar a los enemigos
    /// </summary>
    [SerializeField] private LayerMask _enemyLayers;

    /// <summary>
    /// Control de si salto o no
    /// </summary>
    private bool _jump = false;

    /// <summary>
    /// Control si hizo el slide o no
    /// </summary>
    private bool _slide = false;

    /// <summary>
    /// Control si ataco o no
    /// </summary>
    private bool _attack = false;
    
    /// <summary>
    /// Controles del Player.
    /// </summary>
    private PlayerController _playerController;

    /// <summary>
    /// Control del animator del player
    /// </summary>
    private Animator _animator;

    private void Start()
    {
        // Se obtienen los componente de PlayerController
        _playerController = GetComponent<PlayerController>();

        // Se obtienen los componente de Animator
        _animator = GetComponent<Animator>();

        // Se revisa si los componentes a los cuales se quieren obtener existan, 
        // si no existen se debugeara un error
        #region CHECK_COMPONENTS_EXITS

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

    private void Update()
    {
        // Chequeo de los inputs del jugador
        #region CHECK_INPUTS

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Estara saltando
            _jump = true;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // Se ajusta _slide a true ya que presiono la tecla
            _slide = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Se ajusta _attack a true ya que presiono la tecla
            //_attack = true;
            Attack();
        }

        #endregion
    }

    private void FixedUpdate()
    {
        // Se manda a llamar en todo momento el metodo de movement 
        // para controlar al player
        Movement();

        // Chequeo de estado de las variables
        #region CHECK_STATES

        if (_jump)
        {
            // Se hace falso ya que si no estaria saltando infinitamente
            _jump = false;

            Jump();
        }

        if (_slide)
        {
            Slide();
        }

        #endregion
    }

    private void OnDrawGizmosSelected()
    {
        // Si no existe el Transform de attackPoint
        if(_attackPoint == null)
        {
            // Retornara para no hacer nada mas
            return;
        }

        // Dibuja la esfera en la posicion de attackpoint y el radio de attackrange
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
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
        _animator.SetFloat(StringsType.SpeedParameter, Mathf.Abs(horizontalInput));

        // Se manda a llamar el metodo de Move y se le pasa la direccion y velocidad
        _playerController.Move(horizontalInput * Time.fixedDeltaTime, false, _jump, _slide);

    }

    /// <summary>
    /// Sistema de salto
    /// </summary>
    private void Jump()
    {
        // Se manda true al parametro de IsJumping para activar la animacion
        _animator.SetBool(StringsType.IsJumpingParameter, true);
    }

    /// <summary>
    /// Sistema de slide
    /// </summary>
    private void Slide()
    {
        // Se envia un true al parametro de IsSlide para la animacion
        _animator.SetBool(StringsType.IsSlideParameter, true);

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
        _animator.SetBool(StringsType.IsSlideParameter, false);
    }

    /// <summary>
    /// Metodo que maneja el evento de atacar
    /// </summary>
    private void Attack()
    {
        // Se le asigna un true al parametro de IsAttacking del animator
        _animator.SetTrigger(StringsType.IsAttackingParameter);

        // Se guarda lo obtenido en el OverlapCircle
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        // Se recorren todos los enemigos que toco el overlap
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Atacaste a: " + enemy.name);
        }
    }

    /// <summary>
    /// Al aterrizar se utiliza este metodo para mandarlo a llamar
    /// en el evento del PlayerController al momento de aterrizar
    /// </summary>
    public void OnLanding()
    {
        // Se manda false al parametro de IsJumping para desactivar la animacion
        _animator.SetBool(StringsType.IsJumpingParameter, false);
    }

    /// <summary>
    /// Cuando el player colisione con algun enemigo este sera el metodo
    /// que se llamara.
    /// </summary>
    /// <param name="damage"></param>
    public void ReciveDamage(int damage)
    {
        // Se resta la vida dependiendo del daño a causar
        _health -= damage;

        // Se manda a llamar la animacion de Hurt
        _animator.SetTrigger(StringsType.IsHurtParameter);

        // Si la vida es menor a 1 osea 0
        if(_health < 1)
        {
            // Se destruye el objeto de Player
            Destroy(this.gameObject);
        }
    }
}
