using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitBehaviour : MonoBehaviour
{
    private GameManager GmScript;

    private void Start()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GmScript = GameManager.GetComponent<GameManager>();
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && !GmScript.ballRespawned)
        {
            GmScript.ballRespawned = true;
        }
    }
}
