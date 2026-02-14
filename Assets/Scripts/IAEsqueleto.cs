using UnityEngine;
using UnityEngine.AI;

public class IAEsqueleto : MonoBehaviour
{
    private float velocidadPatrulla = 2.5f;
    private float velocidadPersecucion = 4.5f;

    [Header("Configuración de IA")]
    public Transform jugador;
    public Transform[] puntosPatrulla;
    public float distanciaVision = 10f;

    private NavMeshAgent agente;
    private Animator anim;
    private int indicePatrulla = 0;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // Busca al jugador automáticamente por su etiqueta
        if (jugador == null)
            jugador = GameObject.FindGameObjectWithTag("Player").transform;

        IrAlSiguientePunto();
    }

    void Update()
    {
        if (jugador == null) return;

        // Calcula la distancia y actualiza la velocidad de la animación
        float distancia = Vector3.Distance(transform.position, jugador.position);
        anim.SetFloat("Speed", agente.velocity.magnitude);

        // Si el jugador está cerca, lo persigue; si no, patrulla
        if (distancia < distanciaVision)
        {
            agente.speed = velocidadPersecucion;
            agente.SetDestination(jugador.position);
        }
        else
        {
            Patrullar();
        }
    }

    void Patrullar()
    {
        agente.speed = velocidadPatrulla;

        // Si llega al punto actual, pasa al siguiente
        if (!agente.pathPending && agente.remainingDistance < 0.8f)
        {
            IrAlSiguientePunto();
        }
    }

    void IrAlSiguientePunto()
    {
        if (puntosPatrulla.Length == 0) return;

        // Asigna el destino y actualiza el índice del array
        agente.destination = puntosPatrulla[indicePatrulla].position;
        indicePatrulla = (indicePatrulla + 1) % puntosPatrulla.Length;
    }

    // Detecta el contacto físico directo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MatarJugador(collision.gameObject);
        }
    }

    // Detecta el contacto si el colisionador está en modo Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MatarJugador(other.gameObject);
        }
    }

    void MatarJugador(GameObject objJugador)
    {
        // Accede al script de salud del jugador y aplica daño mortal
        SaludJugador salud = objJugador.GetComponent<SaludJugador>();
        if (salud != null)
        {
            salud.TakeDamage(1000f);
            Debug.Log("¡El esqueleto ha eliminado al jugador!");
        }
    }

    public void Morir()
    {
        // Suma experiencia al sistema global y destruye el objeto
        if (SistemaXP.instancia != null)
        {
            SistemaXP.instancia.AñadirXP(40);
        }
        Destroy(gameObject);
    }
}