using UnityEngine;
using System.Collections;

public class TrampaLaser : MonoBehaviour
{
    public float onTime = 2f;
    public float offTime = 2f;

    private Collider col;
    private Renderer rend;

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
            // Encendido
            col.enabled = true;
            rend.enabled = true;
            yield return new WaitForSeconds(onTime);

            // Apagado
            col.enabled = false;
            rend.enabled = false;
            yield return new WaitForSeconds(offTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<RespawnJugador>().Respawn();
        }
    }
}
