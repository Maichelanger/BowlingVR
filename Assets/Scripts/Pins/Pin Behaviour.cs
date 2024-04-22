using System.Collections;
using UnityEngine;

public class PinBehaviour : MonoBehaviour
{
    [SerializeField] private float angleThreshold = 0.5f;

    private GameManager GmScript;
    private bool hasCollided = false;
    private bool hasAwardedPoints = false;
    private bool inCoroutine = false;
    private float timeToFall = 3f;

    private void Start()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GmScript = GameManager.GetComponent<GameManager>();
    }

    private void Update()
    {
        if (hasCollided && !hasAwardedPoints && !inCoroutine)
        {
            StartCoroutine(CheckIfFallen());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasCollided)
            return;

        if (collision.gameObject.CompareTag("Ball"))
        {
            hasCollided = true;

            Debug.Log("Pin has collided with the ball");
        }
        else if (collision.gameObject.CompareTag("Pin"))
        {
            hasCollided = true;

            Debug.Log("Pin has collided with another pin");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pit"))
        {
            InPit();
        }
    }

    private void InPit()
    {
        Debug.Log("Pin entered pit");

        GmScript.AddFallenPin();
        hasAwardedPoints = true;

        Destroy(gameObject);
    }

    IEnumerator CheckIfFallen()
    {
        inCoroutine = true;
        yield return new WaitForSeconds(timeToFall);

        if (hasAwardedPoints)
        {
            inCoroutine = false;
            yield break;
        }

        bool noReturnPoint = transform.up.y < angleThreshold;

        if (noReturnPoint)
        {
            Debug.Log("Pin has fallen");

            GmScript.AddFallenPin();
            hasAwardedPoints = true;

            Destroy(gameObject);
        }
        else
        {
            hasCollided = false;
        }

        inCoroutine = false;
    }
}
