using UnityEngine;
using TMPro;

public class SistemaXP : MonoBehaviour
{
    public static SistemaXP instancia; 

    public int xp = 0;
    public TextMeshProUGUI textoXP;

    void Awake()
    {
        instancia = this; 
    }

    public void AñadirXP(int cantidad)
    {
        xp += cantidad;
        ActualizarUI();
    }

    void Start()
    {
        ActualizarUI();
    }

    void ActualizarUI()
    {
        if (textoXP != null)
            textoXP.text = "XP: " + xp;
    }
}
