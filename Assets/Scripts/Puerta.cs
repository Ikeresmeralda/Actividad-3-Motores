using UnityEngine;


public class Puerta : MonoBehaviour
{
    public Cilindro roller;  
    public void OpenDoor()
    {
        gameObject.SetActive(false); 

        if (roller != null)
        {
            roller.StartRolling();
        }
    }
}