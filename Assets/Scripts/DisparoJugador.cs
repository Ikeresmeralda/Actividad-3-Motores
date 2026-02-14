using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    public float alcance = 50f;

    void Update()
    {
        // Detecta el clic izquierdo del ratón para disparar
        if (Input.GetMouseButtonDown(0))
        {
            LanzarRayo();
        }
    }

    void LanzarRayo()
    {
        RaycastHit hit;

        // Crea un rayo desde el centro exacto de la pantalla (cámara)
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        // Physics.Raycast detecta impactos. QueryTriggerInteraction.Collide permite golpear objetos aunque tengan Is Trigger activado.
        if (Physics.Raycast(ray, out hit, alcance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Collide))
        {
            Debug.Log("Impacto detectado en: " + hit.transform.name);

            // Intenta encontrar y eliminar el componente IAEsqueleto
            IAEsqueleto esqueleto = hit.transform.GetComponentInParent<IAEsqueleto>();
            if (esqueleto != null)
            {
                // Gestiona la experiencia y destruye al esqueleto
                if (SistemaXP.instancia != null) SistemaXP.instancia.AñadirXP(40);

                Destroy(esqueleto.gameObject);
                Debug.Log("Esqueleto eliminado");
                return;
            }

            // Intenta encontrar y eliminar el componente de IA normal
            IA enemigoNormal = hit.transform.GetComponentInParent<IA>();
            if (enemigoNormal != null)
            {
                if (SistemaXP.instancia != null) SistemaXP.instancia.AñadirXP(20);

                Destroy(enemigoNormal.gameObject);
                Debug.Log("Enemigo común eliminado");
            }
        }
    }
}