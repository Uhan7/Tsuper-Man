using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadSceneViaName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
