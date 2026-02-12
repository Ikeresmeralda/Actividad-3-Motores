using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{
    public NavMeshAgent agente;
    public Transform jugador;
    public Transform[] puntosPatrulla;
    public Transform puntoInicio; // <--- Arrastra aquí el SpawnPoint
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
        // Esto imprimirá en consola el nombre de CUALQUIER COSA que toque el enemigo
        Debug.Log("El enemigo ha tocado a: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("¡JUGADOR DETECTADO! Teletransportando...");

            if (puntoInicio != null)
            {
                // Forzamos la posición
                CharacterController cc = other.GetComponent<CharacterController>();
                if (cc != null)
                {
                    cc.enabled = false; // Desactivar para que no bloquee el movimiento
                    other.transform.position = puntoInicio.position;
                    cc.enabled = true; // Reactivar
                }
                else
                {
                    other.transform.position = puntoInicio.position;
                }
            }
            else
            {
                Debug.LogError("ERROR: No has arrastrado el punto de inicio al script de la IA");
            }
        }
    }
}