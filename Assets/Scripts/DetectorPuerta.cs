using UnityEngine;

public class DoorTriggerDetector : MonoBehaviour
{
    [SerializeField] GameObject eText;

    public Cilindro roller;  

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("DoorTrigger"))
            {
                eText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Puerta door = hit.collider.GetComponent<Puerta>();

                    if (door != null)
                    {
                        door.OpenDoor();

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