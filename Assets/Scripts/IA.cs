using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{
    public NavMeshAgent agente;
    public Transform jugador;
    public Transform[] puntosPatrulla;
    public float distanciaVision = 8f;

    private int indicePatrulla = 0;
    private bool detectado = false;

    void Start()
    {
        if (agente == null) agente = GetComponent<NavMeshAgent>();
        IrAlSiguientePunto();
    }

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia < distanciaVision)
        {
            if (!detectado)
            {
                Debug.Log("¡TE HE DETECTADO!");
                detectado = true;
            }

            agente.SetDestination(jugador.position);
        }
        else
        {
            detectado = false;

            if (!agente.pathPending && agente.remainingDistance < 0.8f)
            {
                IrAlSiguientePunto();
            }
        }
    }

    void IrAlSiguientePunto()
    {
        if (puntosPatrulla.Length == 0) return;

        agente.destination = puntosPatrulla[indicePatrulla].position;
        indicePatrulla = (indicePatrulla + 1) % puntosPatrulla.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("El enemigo ha tocado a: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("¡JUGADOR ELIMINADO!");

            // Buscar el script de vida del jugador
            SaludJugador salud = other.GetComponent<SaludJugador>();

            if (salud != null)
            {
                // MATAR al jugador → esto activará tu panel y botón
                salud.TakeDamage(999f);
            }
            else
            {
                Debug.LogError("El jugador no tiene el script SaludJugador");
            }
        }
    }
}
