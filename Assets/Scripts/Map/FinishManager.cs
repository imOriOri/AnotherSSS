using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishManager : MonoBehaviour
{
    MapGenerator mapManager;
    public GameObject spawn;

    void Start()
    {
        mapManager = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();

        GetComponent<BoxCollider2D>().enabled = false;
        Vector2 boxSize = new Vector2(1, 0.5f);

        RaycastHit2D hit;

        for (int i = 0; i < 30; i++)
        {
            hit = Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.down, 0.25f);

            if (hit.transform != null)
            {
                break;
            }
            else
            {
                transform.position += Vector3.down * 0.25f;
            }
        }

        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            ResetMap(collision.gameObject);
        }
    }

    void ResetMap(GameObject player) 
    {
        mapManager.MapGenerate();
        player.transform.position = new Vector2(spawn.transform.position.x, spawn.transform.position.y + 1.2f);
    }
}
