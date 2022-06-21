//have fun modifying the game however you want!

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int points;
    public Text score;
    public static bool isInGame;
    public GameObject rectangles;

    private void Start()
    {
        isInGame = false;
        points = 0;
    }
    private void Update()
    {
        if(points > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", points);
        }
        score.text = "Score: " + points + "\nHighScore: " + PlayerPrefs.GetInt("HighScore");
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
   
}
