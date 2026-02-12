using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaludJugador : MonoBehaviour
{
    [Header("Vida")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI Corazones")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Muerte")]
    public GameObject deathPanel;

    private RespawnJugador respawnScript;
    private MovimientoJugador movementScript;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        respawnScript = GetComponent<RespawnJugador>();
        movementScript = GetComponent<MovimientoJugador>();

        if (deathPanel != null)
            deathPanel.SetActive(false);

        UpdateHearts();
    }

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

    void Die()
    {
        isDead = true;

        if (movementScript != null)
            movementScript.enabled = false;

        Time.timeScale = 0f;

        if (deathPanel != null)
            deathPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // BOTÓN REAPARECER
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        // Reactivar tiempo
        Time.timeScale = 1f;

        // Ocultar panel
        if (deathPanel != null)
            deathPanel.SetActive(false);

        // Mover al spawn
        if (respawnScript != null)
            respawnScript.Respawn();

        // Restaurar vida
        currentHealth = maxHealth;
        UpdateHearts();

        // Bloquear movimiento 1 segundo
        if (movementScript != null)
            movementScript.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yield return new WaitForSecondsRealtime(1f);

        if (movementScript != null)
            movementScript.enabled = true;

        isDead = false;
    }

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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Laser"))
        {
            TakeDamage(50f);
        }
    }
}
