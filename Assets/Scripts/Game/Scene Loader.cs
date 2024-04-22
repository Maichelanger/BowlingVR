using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
