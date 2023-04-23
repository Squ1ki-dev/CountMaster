using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            ScoreIncrease();
            EventManager.Fire_OnFinishArea();
            GameStateEvent.Fire_OnChangeGameState(GameState.Minigame);
            other.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void ScoreIncrease()
    {
        PlayerPrefs.SetInt(Tags.Score, PlayerPrefs.GetInt(Tags.Score) + StackController.scoreAmount);
        ButtonController.Instance.totalScore.text = PlayerPrefs.GetInt(Tags.Score).ToString();
        StackController.scoreAmount = 0;
    }
}