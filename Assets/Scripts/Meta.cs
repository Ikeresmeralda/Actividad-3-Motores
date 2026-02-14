using UnityEngine;

public class Meta : MonoBehaviour
{
    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;

            SaludJugador salud = other.GetComponent<SaludJugador>();

            if (salud != null)
            {
                salud.Ganar();
            }
        }
    }
}
