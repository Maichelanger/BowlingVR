using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallBehaviour : MonoBehaviour
{
    public XRBaseInteractor rHand;
    public XRBaseInteractor lHand;

    public string spawnPointName;
    public Canvas infoCanvas;

    private Transform spawnPointPos;
    private GameManager GmScript;
    private GameObject infoObject;

    private void Start()
    {
        spawnPointPos = GameObject.Find(spawnPointName).transform;
        GameObject GameManager = GameObject.Find("GameManager");
        GmScript = GameManager.GetComponent<GameManager>();
        infoObject = infoCanvas.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pit"))
        {
            ResetPos();
            GmScript.ballRespawned = true;
        }
        else if (other.gameObject.CompareTag("Out Of Bounds")) ResetPos();
    }

    public void onGaze()
    {
        if(!rHand.hasSelection && !lHand.hasSelection)
        {
            infoObject.SetActive(true);
        }
    }

    public void onLookAway()
    {
        if (!rHand.hasSelection && !lHand.hasSelection)
        {
            infoObject.SetActive(false);
        }
    }

    public void onGrab()
    {
        infoObject.SetActive(false);
    }

    private void ResetPos()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = spawnPointPos.position;
    }
}
