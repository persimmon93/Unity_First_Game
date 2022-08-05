using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseActive : MonoBehaviour
{
    /// <summary>
    /// This script will set inverse the set active of 2 different gameobject.
    /// </summary>
    public GameObject active;
    public GameObject inverseActive;

    // Update is called once per frame
    void Update()
    {
        if (active.activeSelf)
        {
            inverseActive.SetActive(false);
        } else
        {
            inverseActive.SetActive(true);
        }
    }
}
