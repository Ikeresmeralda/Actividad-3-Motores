using UnityEngine;

public class Trampa : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SaludJugador salud = other.gameObject.GetComponent<SaludJugador>();

            if (salud != null)
            {
                // Mata al jugador y activa el panel de muerte
                salud.TakeDamage(999f);
            }
            else
            {
                Debug.LogError("El jugador no tiene el script SaludJugador");
            }
        }
    }
}
