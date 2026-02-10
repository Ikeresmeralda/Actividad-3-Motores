using UnityEngine;

public class ParedTrampa : MonoBehaviour
{
    public float speed = 10f;
    public float distance = 8f;

    private Rigidbody rb;
    private Vector3 startPos;
    private Vector3 targetPos;
    private bool activated = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        targetPos = startPos + transform.forward * distance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;
            rb.linearVelocity = transform.forward * speed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<RespawnJugador>().Respawn();
        }
    }
}

