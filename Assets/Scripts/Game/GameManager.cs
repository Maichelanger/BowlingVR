using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pinGroup;
    [SerializeField] private GameObject blocker;
    [SerializeField] private int gameResetTimeout = 5;
    [SerializeField] private int maxRounds = 10;
    [SerializeField] private GameObject scoreboard;

    internal bool ballRespawned = false;

    private GameObject instantiatedPinGroup;
    private GameObject instantiatedBlocker;
    private Scoreboard scoreboardScript;
    private int fallenPins = 0;
    private bool secondShot = false;
    private int allPins;
    private bool resetPins = false;
    private int currentRound = 1;
    private bool gameEnded = false;

    private void Start()
    {
        scoreboardScript = scoreboard.GetComponent<Scoreboard>();

        instantiatedPinGroup = Instantiate(pinGroup);

        allPins = GameObject.FindGameObjectsWithTag("Pin").Length;
    }

    private void Update()
    {
        if (gameEnded)
            return;

        if (ballRespawned && !resetPins)
        {
            StartCoroutine(ResetGame());
            ballRespawned = false;
        }
        else if (resetPins && !ballRespawned)
        {
            Debug.Log("Pins have been reset");

            resetPins = false;
        }
    }

    public void AddFallenPin()
    {
        fallenPins++;

        Debug.Log("Point awarded");
    }

    private void NewRound()
    {
        if (currentRound >= maxRounds)
        {
            gameEnded = true;
            EndGame();
            return;
        }

        scoreboardScript.UpdateScore(fallenPins);

        Destroy(instantiatedPinGroup);
        instantiatedPinGroup = Instantiate(pinGroup);

        fallenPins = 0;
        resetPins = true;
        ballRespawned = false;
        secondShot = false;
        currentRound++;
    }

    private void EndGame()
    {
        Debug.Log("Game over");

        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        Destroy(instantiatedPinGroup);
    }

    IEnumerator ResetGame()
    {
        Debug.Log("Resetting game");

        instantiatedBlocker = Instantiate(blocker);

        yield return new WaitForSeconds(gameResetTimeout);

        Destroy(instantiatedBlocker);

        if (secondShot)
        {
            if (fallenPins >= allPins)
            {
                Debug.Log("Spare");
            }

            NewRound();
        }
        else
        {
            if (fallenPins >= allPins)
            {
                Debug.Log("Strike");

                NewRound();
            }
            else secondShot = true;
        }
    }
}
