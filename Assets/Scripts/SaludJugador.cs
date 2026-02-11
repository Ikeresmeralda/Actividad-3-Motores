using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaludJugador : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private RespawnJugador respawnScript;
    private MonoBehaviour movementScript; // script de movimiento
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        respawnScript = GetComponent<RespawnJugador>();
        movementScript = GetComponent<MovimientoJugador>(); // tu script de movimiento
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
            StartCoroutine(DeathRoutine());
            return;
        }

        UpdateHearts();
    }

    IEnumerator DeathRoutine()
    {
        isDead = true;

        respawnScript.Respawn();

        if (movementScript != null)
            movementScript.enabled = false;

        yield return new WaitForSeconds(2f);

        currentHealth = maxHealth;
        UpdateHearts();

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
