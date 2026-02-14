using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaludJugador : MonoBehaviour
{
    [Header("Configuración de Vida")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Interfaz de Usuario (Corazones)")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Paneles de Estado")]
    public GameObject deathPanel;
    public GameObject winPanel;

    private RespawnJugador respawnScript;
    private MovimientoJugador movementScript;
    private DoorTriggerDetector doorDetector;
    private EfectoMuerte efectoMuerte;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        respawnScript = GetComponent<RespawnJugador>();
        movementScript = GetComponent<MovimientoJugador>();
        doorDetector = GetComponent<DoorTriggerDetector>();
        efectoMuerte = Object.FindFirstObjectByType<EfectoMuerte>();

        // Estado inicial de la UI
        if (deathPanel != null) deathPanel.SetActive(false);
        if (winPanel != null) winPanel.SetActive(false);
        if (efectoMuerte != null) efectoMuerte.ResetMuerte();

        Time.timeScale = 1f;
        UpdateHearts();
    }

    // Gestiona el daño recibido
    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            UpdateHearts();
            Die();
            return;
        }

        UpdateHearts();
    }

    // Función de VICTORIA (Arregla el error de Meta.cs)
    public void Ganar()
    {
        if (isDead) return;

        if (movementScript != null) movementScript.enabled = false;

        // Detiene el juego y muestra panel
        Time.timeScale = 0f;

        if (winPanel != null) winPanel.SetActive(true);

        // Libera el ratón
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Función de MUERTE
    void Die()
    {
        if (isDead) return;
        isDead = true;

        if (movementScript != null) movementScript.enabled = false;

        Time.timeScale = 0.2f; // Cámara lenta al morir

        if (deathPanel != null) deathPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (efectoMuerte != null) efectoMuerte.ActivarMuerte();
    }

    // Actualiza los iconos de vida en la pantalla
    void UpdateHearts()
    {
        int heartsToShow = Mathf.CeilToInt(currentHealth / 25f);

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < heartsToShow)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    // Reinicia el nivel desde los botones de la UI
    public void ReiniciarNivel()
    {
        StartCoroutine(Reiniciar());
    }

    IEnumerator Reiniciar()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Salida del juego
    public void SalirDelJuego()
    {
        Time.timeScale = 1f;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}