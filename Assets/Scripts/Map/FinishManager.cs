using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishManager : MonoBehaviour
{
    MapProcess mapManager;
    public LayerMask exceptLayer;

    void Start()
    {
        exceptLayer = ~exceptLayer;
        mapManager = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapProcess>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            mapManager.NextLevel();
        }
    }
}
