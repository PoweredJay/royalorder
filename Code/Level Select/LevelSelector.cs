using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelector : MonoBehaviour
{
    public void LoadSelectedScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}