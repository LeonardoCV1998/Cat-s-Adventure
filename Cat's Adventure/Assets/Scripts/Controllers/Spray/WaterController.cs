using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Si colisiona con el Player
        if(collision.tag.Equals("Player"))
        {

        }
    }
}
