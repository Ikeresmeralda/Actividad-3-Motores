using UnityEngine;

public class RespawnJugador : MonoBehaviour
{
    public Transform respawnPoint;

    public void Respawn()
    {
        CharacterController cc = GetComponent<CharacterController>();

        if (cc != null)
            cc.enabled = false;   

        transform.position = respawnPoint.position;

        if (cc != null)
            cc.enabled = true;
    }
}
