using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum OperatorType { Addition, Multiplication };

public class StackController : MonoBehaviour
{
    [SerializeField] private OperatorType operatorType;
    [SerializeField] private int operatorCount;

    private Transform operatorParent;
    private TextMeshProUGUI operatorText, symbolText;

    public static int playerChildCount, scoreAmount;

    private void Awake()
    {
        InitializeVariables();
        LoadScore();
        playerChildCount = 1;
        operatorText.text = operatorCount.ToString();
        symbolText.text = operatorType == OperatorType.Addition ? "+" : "x";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerChild>())
        {
            ApplyOperator();
            SpawnChild(other.transform.position + Vector3.right * 0.1f);
            DeactivateOperators();
        }
    }

    private void InitializeVariables()
    {
        operatorParent = transform.parent;
        symbolText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        operatorText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void LoadScore()
    {
        scoreAmount = PlayerPrefs.GetInt("Score", 0);
        ButtonController.Instance.scoreText.text = scoreAmount.ToString();
    }

    private void ApplyOperator()
    {
        int operand = operatorCount;
        switch (operatorType)
        {
            case OperatorType.Addition:
                playerChildCount += operand;
                break;
            case OperatorType.Multiplication:
                playerChildCount *= operand;
                break;
        }
        scoreAmount += operand;
        ButtonController.Instance.scoreText.text = scoreAmount.ToString();
        operatorCount = operand;
        PlayerCount.playerCount.text = playerChildCount.ToString();
    }

    private void SpawnChild(Vector3 position)
    {
        for (int i = 0; i < operatorCount; i++)
        {
            ObjectPooling.Instance.GetSpawnObject(Tags.PlayerChild, position, Quaternion.identity);
        }
        EventManager.Fire_OnStartMovement();
    }

    private void DeactivateOperators()
    {
        for (int i = 0; i < operatorParent.childCount; i++)
        {
            operatorParent.GetChild(i).gameObject.SetActive(false);
        }
    }
}