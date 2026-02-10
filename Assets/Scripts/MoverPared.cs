using System.Collections;
using UnityEngine;

public class MoverPared : MonoBehaviour
{
    public Transform wall;
    public float speed = 4f;
    public float distance = 8f;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;
            StartCoroutine(MovePared());
        }
    }

    IEnumerator MovePared()
    {
        Vector3 startPos = wall.position;
        Vector3 targetPos = startPos + Vector3.left * distance;

        while (Vector3.Distance(wall.position, targetPos) > 0.2f)
        {
            wall.position = Vector3.MoveTowards(
                wall.position,
                targetPos,
                speed * Time.deltaTime
            );
            yield return null;
        }
    }
}
