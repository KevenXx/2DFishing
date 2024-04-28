
using UnityEngine;

public class teleportToGame : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            SceneController.instance.NextLevel();
        }
    }
}
