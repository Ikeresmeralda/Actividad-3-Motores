using UnityEngine;

public class Cilindro : MonoBehaviour
{
    public float speed = 8f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void StartRolling()
    {
        rb.isKinematic = false;
        rb.linearVelocity = -transform.right * speed;
    }
    
}
