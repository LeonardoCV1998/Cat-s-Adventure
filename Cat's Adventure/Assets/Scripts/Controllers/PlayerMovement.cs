﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Control de la velocidad de correr
    /// </summary>
    [SerializeField] private float _runSpeed = 40f;

    /// <summary>
    /// Vida del jugador, seran 3 vidas en total.
    /// </summary>
    [SerializeField] private int _health;

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
    /// Tiempo que durara el efecto del powerUp
    /// </summary>
    [SerializeField] private float _timePowerUp = 5;

    /// <summary>
    /// Color inicial del jugador cuando tome el power up
    /// </summary>
    [SerializeField] private Color _initialColor;

    /// <summary>
    /// Color final cuando termine el tiempo del power up
    /// </summary>
    [SerializeField] private Color _finalColor;

    /// <summary>
    /// Variable donde se guardaran los ratones
    /// </summary>
    [SerializeField] private int _mouses;

    /// <summary>
    /// Control de si puede recibir daño o no el Player
    /// </summary>
    private bool _damageable = false;

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

    /// <summary>
    /// Componente de sprite renderer
    /// </summary>
    private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// Componente de GameController
    /// </summary>
    private GameController _gameController;

    /// <summary>
    /// Componente de UIController
    /// </summary>
    private UIController _uiController;

    /// <summary>
    /// Se realiza un evento el cual se llamara cuando se haga daño al Player
    /// </summary>
    public event Action DamageTaken;

    /// <summary>
    /// Se realiza un evento el cual se llamara cuando se aumente la vida del Player
    /// </summary>
    public event Action LifesUpgraded;

    /// <summary>
    /// Revisara si cae el jugador y muere
    /// </summary>
    private bool _isDead = false;


    private void Start()
    {
        // Se busca y se obtiene el GameController
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();

        // Se busva y se obtiene el UIController
        _uiController = GameObject.Find("GameController").GetComponent<UIController>();

        // Se obtienen los componente de PlayerController
        _playerController = GetComponent<PlayerController>();

        // Se obtienen los componente de Animator
        _animator = GetComponent<Animator>();

        _spriteRenderer = GetComponent<SpriteRenderer>();

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

        if(_spriteRenderer == null)
        {
            Debug.LogError(StringsType.SpriteRendererIsNull);
        }

        if(_gameController == null)
        {
            Debug.LogError(StringsType.GameControllerIsNull);
        }

        #endregion

        // Se hace true ya que al iniciar podra recibir daño
        _damageable = true;


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

        // Definimos la barrera del inicio para que no vaya hacia la izquierda
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5.0f, Mathf.Infinity), transform.position.y);

        if(transform.position.y <= -15.0f)
        {
            _isDead = true;

            if(_isDead)
            {
                _isDead = false;
                _damageable = true;
                ReciveDamage(1);
            }
        }

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
            enemy.GetComponent<PerroEnemy>().ReciveDamage(1);
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
        // Si puede recibir daño el jugador 
        if(_damageable)
        {
            // Se resta la vida dependiendo del daño a causar
            _health -= damage;

            if(DamageTaken != null)
            {
                DamageTaken();
            }

            // Se manda a llamar la animacion de Hurt
            _animator.SetTrigger(StringsType.IsHurtParameter);

            
            // Si la vida es menor a 1 osea 0
            if (_health < 1)
            {
                _gameController.GameOver();
            }
        }
    }

    /// <summary>
    /// Metodo que se encargara de realizar la accion cuando colisione con el Sobresillo
    /// </summary>
    public void PowerUp()
    {
        // No puede recibir daño 
        _damageable = false;

        // Color inicial al momento de tomar el powerup
        _spriteRenderer.color = _initialColor;

        // Empieza la coroutina de tiempo para el power up
        StartCoroutine(TimePowerUp());
    }

    /// <summary>
    /// Coroutina para controlar el tiempo del power up
    /// </summary>
    /// <returns>Tiempo que dura el power up</returns>
    private IEnumerator TimePowerUp()
    {
        // Tiempo de espera
        yield return new WaitForSeconds(_timePowerUp);

        // Cuando termine el tiempo podra recibir daño de nuevo
        _damageable = true;

        // Color final al terminar el tiempo
        _spriteRenderer.color = _finalColor;
    }

    /// <summary>
    /// Metodo que incrementa una vida
    /// </summary>
    public void IncreaseLife()
    {
        // Se incrementa uno de vida
        _health++;

        if (LifesUpgraded != null)
        {
            LifesUpgraded();
        }
    }

    /// <summary>
    /// Metodo para obtener la velocidad de correr
    /// </summary>
    public void GetRunSpeed(float value)
    {
        _runSpeed = value;
    }

    /// <summary>
    /// Metodo para obtener la vida
    /// </summary>
    /// <returns></returns>
    public int GetHealth()
    {
        return _health;
    }

    /// <summary>
    /// Metodo para obtener la posicion en X
    /// </summary>
    /// <returns></returns>
    public float GetPositionX()
    {
        return transform.position.x;
    }

    /// <summary>
    /// Metodo para obtener la posicion en Y
    /// </summary>
    /// <returns></returns>
    public float GetPositionY()
    {
        return transform.position.y;
    }

    /// <summary>
    /// Metodo para ajustar la vida
    /// </summary>
    /// <param name="health"></param>
    public void SetHealth(int health)
    {
        _health = health;
    }

    /// <summary>
    /// Metodo para ajustar la posicion
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetPosition(float x, float y)
    {
        transform.position = new Vector2(x, y);
    }

    /// <summary>
    /// Metodo para obtener los mouses
    /// </summary>
    /// <returns></returns>
    public int GetMouses()
    {
        return _mouses;
    }

    /// <summary>
    /// Metodo para ajustar los mouses
    /// </summary>
    /// <param name="value"></param>
    public void SetMouses(int value)
    {
        _mouses += value;

    }
}
