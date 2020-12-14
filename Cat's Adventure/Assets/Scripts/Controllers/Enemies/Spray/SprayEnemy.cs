using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayEnemy : EnemyParent
{
    /// <summary>
    /// Acceso al ScriptableObject de Spray
    /// </summary>
    public SprayScriptableObject sprayScriptableObject;

    /// <summary>
    /// Control de estados del Spray
    /// </summary>
    private SprayStates _sprayStates;

    /// <summary>
    /// Objeto que hara daño al Player
    /// </summary>
    [SerializeField] private GameObject _water;

    /// <summary>
    /// Tiempo actual para hacer el spray
    /// </summary>
    private float _timeToSprayCurrent;

    /// <summary>
    /// Tiempo actual spayeando
    /// </summary>
    private float _timeSprayingCurrent;

    private void Start()
    {
        // Se dan valor a las variables current
        _timeToSprayCurrent = sprayScriptableObject.timeToSpray;
        _timeSprayingCurrent = sprayScriptableObject.timeSpraying;

        // Se desactiva el agua
        _water.SetActive(false);

        // Se asgina el estado de NotSpraying, ya que es el inicial
        _sprayStates = SprayStates.NotSpraying;
    }

    private void Update()
    {
        Behaviour();
    }

    /// <summary>
    /// Metodo que se manda a llamar cuando no se esta sprayeando,
    /// este restara el tiempo para hacer el spray hasta llegar a cero
    /// </summary>
    private void NotSprayingAction()
    {
        // Se resta el tiempo para hacer el spray
        _timeToSprayCurrent -= Time.deltaTime;

        // Cuando termina el tiempo
        if (_timeToSprayCurrent <= 0)
        {
            // Cambia de estado a Sprayeando
            _sprayStates = SprayStates.Spraying;
        }
    }

    /// <summary>
    /// Metodo que se manda a llamar cuando se esta sprayeando,
    /// este restara el tiempo que tarda sprayeando hasta llegar a 0
    /// y volver al estado normal
    /// </summary>
    private void SprayingAction()
    {
        // Se activa el agua para que pueda hacer daño
        _water.SetActive(true);

        // Se resta el tiempo actual que esta sprayeando 
        _timeSprayingCurrent -= Time.deltaTime;

        // Cuando llegue a cero 
        if (_timeSprayingCurrent <= 0)
        {
            // Se desactiva el agua para que ya no haga daño
            _water.SetActive(false);

            // Se reasignan los tiempos normales
            _timeToSprayCurrent = sprayScriptableObject.timeToSpray;
            _timeSprayingCurrent = sprayScriptableObject.timeSpraying;

            // Se regresa al estado normal de no sprayear
            _sprayStates = SprayStates.NotSpraying;
        }
    }

    /// <summary>
    /// Sobrescrito del metodo de la clase padre
    /// </summary>
    protected override void Behaviour()
    {
        // Se realiza el switch para verificar el estado de Spray
        switch (_sprayStates)
        {
            case SprayStates.NotSpraying:
                {
                    // Cuando no se esta sprayeando se hace este metodo
                    NotSprayingAction();
                }
                break;

            case SprayStates.Spraying:
                {
                    // Cuando se esta sprayeando se hace este metodo
                    SprayingAction();
                }
                break;
        }
    }

    /// <summary>
    /// Metodo que retorna el valor de damage para ser usado
    /// </summary>
    /// <returns></returns>
    public int GetDamage()
    {
        return damage;
    }
}
