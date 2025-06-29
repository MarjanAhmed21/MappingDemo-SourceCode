using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform guy;
    [SerializeField] private Transform respawnPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            guy.transform.position = respawnPoint.transform.position;
            Physics.SyncTransforms();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.CompareTag("Player"))
        {
            guy.transform.position = respawnPoint.transform.position;
            Physics.SyncTransforms();
        }
    }
    
}
