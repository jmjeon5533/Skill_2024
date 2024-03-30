using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        var g = GameManager.Instance;
        if (other.CompareTag("Player"))
        {
            g.playerStack++;
            if (g.playerStack >= 2) GameManager.Instance.GameEnd(true);
        }
        else if (other.CompareTag("Enemy"))
        {
            g.enemyStack++;
            if (g.enemyStack >= 2) GameManager.Instance.GameEnd(false);
        }
    }
}
