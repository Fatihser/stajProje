using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class button : MonoBehaviour
{
    public playerController script;
    public GameObject startButton;
    public Material playerRengi;

    private void Start()
    {
        playerRengi.color = Color.red;
    }
    public void startGame()
    {
        Destroy(startButton);
        script.enabled = true;
    }
    public void reStart()
    {
        SceneManager.LoadScene(0);
    }
}
