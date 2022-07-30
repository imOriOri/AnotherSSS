using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject 
{
    public static void DownToGround(GameObject mapObject, LayerMask exceptLayer) 
    {
        mapObject.GetComponent<BoxCollider2D>().enabled = false;
        Vector2 boxSize = new Vector2(0.5F, 0.5f);

        RaycastHit2D hit;

        for (int i = 0; i < 30; i++)
        {
            hit = Physics2D.BoxCast(mapObject.transform.position, boxSize, 0, Vector2.down, 0.25f, exceptLayer);

            if (hit.transform != null)
            {
                break;
            }
            else
            {
                mapObject.transform.position += Vector3.down * 0.25f;
            }
        }

        mapObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}

public class FinishManager : MonoBehaviour
{
    MapGenerator mapManager;
    public LayerMask exceptLayer;

    void Start()
    {
        exceptLayer = ~exceptLayer;
        mapManager = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();
        Invoke("LateStart", 0.1f);
    }

    void LateStart() 
    {
        MapObject.DownToGround(gameObject, exceptLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            mapManager.MapGenerate();
        }
    }
}
