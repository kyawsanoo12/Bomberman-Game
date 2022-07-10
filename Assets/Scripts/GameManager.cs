using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;

    public void CheckWinState()
    {
        int count = 0;
        foreach (GameObject player in players)
        {
            if (player.activeSelf)
            {
                count++;
            }
        }
        if (count <= 1)
        {
            Invoke(nameof(NewRound), 1f);
        }
    }
    private void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
