using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private EnemyStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.currentHealth < 45)
        {
            stats.berserkerMode = true;
        }
    }
}
