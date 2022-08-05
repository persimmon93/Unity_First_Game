using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will contain an audio clip and will change clips when player
/// touches object this script is attached to.
/// </summary>
public class AudioThemeChanger : MonoBehaviour
{
    public AudioClip theme;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (theme != null)
            {
                AudioManager.Instance.ChangeTrack(theme);
            }
        }
    }
}
