using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EfectoMuerte : MonoBehaviour
{
    public Image damageOverlay;
    public TextMeshProUGUI textoMuerte;

    public float velocidad = 2f;

    public void ActivarMuerte()
    {
        if (textoMuerte != null)
            textoMuerte.gameObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(FlashRojo());
    }

    IEnumerator FlashRojo()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.unscaledDeltaTime * velocidad;

            if (damageOverlay != null)
                damageOverlay.color = new Color(1, 0, 0, t * 0.6f);

            yield return null;
        }
    }

    public void ResetMuerte()
    {
        if (textoMuerte != null)
            textoMuerte.gameObject.SetActive(false);

        if (damageOverlay != null)
            damageOverlay.color = new Color(1, 0, 0, 0);
    }
}
