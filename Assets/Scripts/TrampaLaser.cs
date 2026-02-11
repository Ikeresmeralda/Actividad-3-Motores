using UnityEngine;
using System.Collections;

public class TrampaLaser : MonoBehaviour
{
    public float onTime = 2f;
    public float offTime = 2f;

    private Collider col;
    private Renderer rend;
    private bool canDamage = true;

    void Start()
    {
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
        StartCoroutine(LaserLoop());
    }

    IEnumerator LaserLoop()
    {
        while (true)
        {
            col.enabled = true;
            rend.enabled = true;
            canDamage = true;
            yield return new WaitForSeconds(onTime);

            col.enabled = false;
            rend.enabled = false;
            yield return new WaitForSeconds(offTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canDamage && other.CompareTag("Player"))
        {
            SaludJugador playerHealth = other.GetComponent<SaludJugador>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(50f);
                canDamage = false; // evita doble daño
            }
        }
    }
}
