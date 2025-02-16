using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{
    private AudioSource menuMusic;

    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject keybindsMenu;
    public GameObject audioMenu;

    public Slider volumeSlider;
    public Toggle musicToggle;
    public Toggle sfxToggle;

    public static float volume = 1;
    public static bool musicOn = true;
    public static bool sfxOn = true;
    GameObject lastSelect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
