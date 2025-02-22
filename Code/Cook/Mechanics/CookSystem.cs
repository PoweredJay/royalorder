using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CookSystem : MonoBehaviour
{
    public int reputation;
    public int gold;
    public int goldGoal;
    public float goldTime;
    public float curTime;
    public bool gameDone;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI repText;
    // Start is called before the first frame update
    void Start()
    {
        goldText.text = "Gold: " + gold;
        repText.text = "Rep: " + reputation;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if(curTime >= goldTime)
        {
            curTime = 0;
            ModifyGold(-1);
        }
        if(gold == 0 && !gameDone)
        {
            StartCoroutine(GameOver());
            gameDone = true;
        }
        if(gold == goldGoal && !gameDone)
        {
            StartCoroutine(GameWin());
            gameDone = true;
        }
    }
    public void ModifySystem(int rep, int g)
    {
        reputation += rep;
        gold += g;
        goldText.text = "Gold: " + gold;
        repText.text = "Rep: " + reputation;
    }
    public void ModifyRep(int rep)
    {
        reputation += rep;
        repText.text = "Rep: " + reputation;
    }
    public void ModifyGold(int g)
    {
        gold += g;
        if(gold < 0)
        {
            gold = 0;
        }
        goldText.text = "Gold: " + gold;
    }

    public IEnumerator GameOver()
    {
        Debug.Log("You lost...");
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }
    public IEnumerator GameWin()
    {
        Debug.Log("You won!");
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
        
    }
}
