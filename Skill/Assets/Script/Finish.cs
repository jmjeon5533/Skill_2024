using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public float playerStack;
    public float enemyStack;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerStack++;
            if (playerStack >= 2) GameManager.Instance.GameEnd(true);
        }
        else if (other.CompareTag("Enemy"))
        {
            enemyStack++;
            if (playerStack >= 2) GameManager.Instance.GameEnd(false);
        }
    }
}
