using System.Collections;
using UnityEngine;

public class Trampa : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<RespawnJugador>().Respawn();
        }
    }

    
    


    
}
