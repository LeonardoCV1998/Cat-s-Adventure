using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();

            if(gameController != null)
            {
                gameController.SaveParty();

                Destroy(this.gameObject.GetComponent<BoxCollider2D>());
            }
        }
    }
}
