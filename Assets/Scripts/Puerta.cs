using UnityEngine;

public class Puerta : MonoBehaviour
{
    public Cilindro roller;

    public void OpenDoor()
    {
        if (SistemaXP.instancia != null)
        {
            SistemaXP.instancia.AñadirXP(5);
            Debug.Log("+5 XP (puerta)");
        }

        gameObject.SetActive(false);

        if (roller != null)
        {
            roller.StartRolling();
        }
    }
}
