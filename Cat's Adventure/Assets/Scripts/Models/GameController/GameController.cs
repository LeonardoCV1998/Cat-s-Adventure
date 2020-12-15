using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class GameController : MonoBehaviour
{
    private PlayerMovement _player;

    public event Action ShowLifes;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        if(_player == null)
        {
            Debug.LogError(StringsType.PlayerMovementIsNull);
        }
        
    }

    private void Start()
    {
        if(InfoParty.thereSaveParty)
        {
            SaveParty();
        }
    }

    public void SaveParty()
    {
        InfoParty.InfoPlayer.health = _player.GetHealth();
        InfoParty.InfoPlayer.position = _player.GetPosition();
        //InfoParty.InfoPlayer.score = _score;

        InfoParty.thereSaveParty = true;

        // Limpiamos la lista para que no nos añada nuevos
        InfoParty.infoRecolectables.Clear();

        // Se busca el objeto que se llame recolectables de la scene
        Transform recolectables = GameObject.Find("Recolectables").transform;

        // Se pasa por cada uno de los obj
        foreach(Transform rec in recolectables)
        {
            // Se obtiene la informacion de si estan activos o no
            InfoParty.TypeInfoRecolectables itemRec = new InfoParty.TypeInfoRecolectables
            {
                isActive = rec.gameObject.activeSelf
            };

            // Se añade a la lista
            InfoParty.infoRecolectables.Add(itemRec);
        }




        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream fileStream = File.Open(Application.persistentDataPath + 
            "/DataContainer.dat", FileMode.Create);

        DataContainer dataContainer = new DataContainer();

        dataContainer.health = InfoParty.InfoPlayer.health;
        //dataContainer.position = _player.GetPosition();
        //dataContainer.score = _player.GetScore();

        binaryFormatter.Serialize(fileStream, dataContainer);

        fileStream.Close();
    }

    public void LoadParty()
    {
        _player.SetHealth(InfoParty.InfoPlayer.health);
        _player.SetPosition(InfoParty.InfoPlayer.position);
        //_score = InfoParty.InfoPlayer.score

        // Se busca el objeto que se llame recolectables de la scene
        Transform recolectables = GameObject.Find("Recolectables").transform;

        int i = 0;

        // Se pasa por cada uno de los obj
        foreach (Transform rec in recolectables)
        {
            rec.gameObject.SetActive(InfoParty.infoRecolectables[i++].isActive);
        }



        if(File.Exists(Application.persistentDataPath + "/DataContainer.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            FileStream fileStream = File.Open(Application.persistentDataPath +
                "/dataContainer.dat", FileMode.Open);

            DataContainer dataContainer = (DataContainer)binaryFormatter.Deserialize(fileStream);

            fileStream.Close();

            _player.SetHealth(dataContainer.health);
            //_player.SetPosition(dataContainer.position);
            //_player.SetScore(dataContainer.score);
        }

        if(ShowLifes != null)
        {
            ShowLifes();
            
        }
    }
}

[Serializable]
public class DataContainer
{
    public int health;
    //public Vector2 position;
    public int score;
}
