using System.Collections;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private CompositeCollider2D currentOneWayPlatform; // For Tilemaps
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private LayerMask oneWayLayer; // LayerMask for the platform

    void Update()
    {
        if (Input.GetKey(KeyCode.S) && currentOneWayPlatform != null)
        {
            StartCoroutine(DisableCollision());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object is in the OneWay layer
        if (((1 << collision.gameObject.layer) & oneWayLayer) != 0)
        {
            currentOneWayPlatform = collision.gameObject.GetComponent<CompositeCollider2D>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & oneWayLayer) != 0)
        {
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        CompositeCollider2D platformCollider = currentOneWayPlatform.GetComponent<CompositeCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
