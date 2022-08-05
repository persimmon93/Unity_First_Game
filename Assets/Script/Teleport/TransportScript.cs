using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TransportScript : MonoBehaviour
{
    public Transform destination;

    private void Start()
    {
        if (destination == null)
        {
            Debug.LogWarning("Teleport settings not set.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<NavMeshAgent>().Warp(destination.transform.position);
    }
}
