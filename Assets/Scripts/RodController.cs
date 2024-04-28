using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodController : MonoBehaviour
{

   private void OnTriggerEnter2D(Collider2D collision)
   {
        PlayerController player = collision.GetComponent<PlayerController>();

        if(player){
            player.numRods++;
            Destroy(this.gameObject);
        }
}
}
