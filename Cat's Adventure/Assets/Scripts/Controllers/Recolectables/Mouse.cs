using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

            if(playerMovement != null)
            {
                playerMovement.SetMouses(1);

                MouseUI mouseUI = GameObject.FindGameObjectWithTag("MouseUI").GetComponent<MouseUI>();

                mouseUI.UpdateMouses();

                Destroy(this.gameObject);
            }    
        }    
    }
}
