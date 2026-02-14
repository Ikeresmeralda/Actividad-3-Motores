using UnityEngine;

public class DoorTriggerDetector : MonoBehaviour
{
    [SerializeField] private GameObject eText;
    [SerializeField] private GameObject mirilla;
    [SerializeField] private Cilindro roller;

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("DoorTrigger"))
            {
                // Mostrar texto solo si la mirilla no está activa
                if (mirilla != null && !mirilla.activeSelf)
                {
                    if (eText != null)
                        eText.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Puerta door = hit.collider.GetComponentInParent<Puerta>();

                    if (door != null)
                    {
                        door.OpenDoor();

                        if (mirilla != null)
                            mirilla.SetActive(true);

                        if (eText != null)
                            eText.SetActive(false);

                        if (roller != null)
                            roller.StartRolling();
                    }
                    else
                    {
                        Debug.LogWarning("No se encontró el script Puerta en el objeto.");
                    }
                }

                return;
            }
        }

        // Si no estamos mirando la puerta
        if (eText != null)
            eText.SetActive(false);
    }

    // 🔥 RESET AL RESPWAN
    public void ResetDoorUI()
    {
        if (eText != null)
            eText.SetActive(false);

        if (mirilla != null)
            mirilla.SetActive(false);
    }
}
