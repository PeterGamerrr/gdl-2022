using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    [HideInInspector] public int freezeTime;
    [HideInInspector] public float range;
    [HideInInspector] public Collider2D[] collidersInRange;
    private List<GameObject> frozenEnemies = new List<GameObject>();
    Rigidbody2D rb;

    public IEnumerator FreezeEnemies()
    {
        foreach (Collider2D collider in collidersInRange)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<AIPath>().enabled = false;
                rb = collider.gameObject.GetComponent<Rigidbody2D>();
                rb.velocity = Vector3.zero;
                frozenEnemies.Add(collider.gameObject);
            }
        }
        yield return new WaitForSeconds(freezeTime);
        foreach (GameObject enemy in frozenEnemies)
        {
            enemy.gameObject.GetComponent<AIPath>().enabled = true;
        }
        frozenEnemies.Clear();
    }

}
