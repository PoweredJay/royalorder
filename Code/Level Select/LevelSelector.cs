using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelector : MonoBehaviour
{
    public MusicClass menuMusic;
    public void Start()
    {
        menuMusic = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>();
        menuMusic.PlayMusic();
        menuMusic.ChangeVolume(MenuSystem.globalVolume);
    }
    public void LoadSelectedScene(string scene)
    {
        SceneManager.LoadScene(scene);
        
    }
}