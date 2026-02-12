using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    public float alcance = 15f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LanzarRayo();
        }
    }

    void LanzarRayo()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, alcance))
        {
            IA enemigo = hit.transform.GetComponent<IA>();
            if (enemigo != null)
            {
                Destroy(hit.transform.gameObject);
                Debug.Log("Enemigo destruido");
            }
        }
    }
}