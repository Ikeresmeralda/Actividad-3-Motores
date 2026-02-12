using UnityEngine;

public class DoorTriggerDetector : MonoBehaviour
{
    [SerializeField] GameObject eText;
    [SerializeField] GameObject mirilla; // Arrastra aquí la imagen de la mirilla del Canvas

    public Cilindro roller;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("DoorTrigger"))
            {
                // Solo mostramos el texto si la mirilla NO está activa (puerta cerrada)
                if (!mirilla.activeSelf) eText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Puerta door = hit.collider.GetComponent<Puerta>();

                    if (door != null)
                    {
                        door.OpenDoor();

                        // ACTIVAMOS la mirilla y QUITAMOS el texto de la E
                        if (mirilla != null) mirilla.SetActive(true);
                        eText.SetActive(false);

                        if (roller != null)
                        {
                            roller.StartRolling();
                        }
                    }
                }
                return;
            }
        }

        eText.SetActive(false);
    }
}