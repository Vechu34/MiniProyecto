using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    Vector2 checkpointPos;
    public LayerMask hitSpikeLayer;

    private void Start()
    {
        checkpointPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & hitSpikeLayer) != 0)
        {
            Die();
        }
    }
    void Die()
    {
        Respawm();
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }

    void Respawm()
    {
        transform.position = checkpointPos;
    }
}
