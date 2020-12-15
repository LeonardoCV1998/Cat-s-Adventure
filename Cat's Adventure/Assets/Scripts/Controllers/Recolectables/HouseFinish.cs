using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseFinish : MonoBehaviour
{
    [SerializeField] private GameObject finishTextObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            collision.gameObject.SetActive(false);

            finishTextObj.SetActive(true);

            GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();

            gameController.GameFinish();
        }
    }
}
