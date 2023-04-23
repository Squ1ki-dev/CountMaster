using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public static ButtonController Instance;

    private GameObject scoreParent;
    private Button playBut, restartBut, nextLevelBut;

    public Text scoreText,totalScore, levelText;

    private void Awake()
    {
        Instance = this;
        ButtonReference();
        scoreParent = scoreText.transform.parent.gameObject;
    }

    private void Start() => UpdateLevelNumber();

    public void PlayButton()
    {
        playBut.gameObject.SetActive(false);
        scoreParent.SetActive(true);

        GameStateEvent.Fire_OnChangeGameState(GameState.Play);
    }

    public void RestartButton()
    {
        restartBut.gameObject.SetActive(false);
        LevelManager.Instance.LevelCreated();
    }

    public void NextLevelButton()
    {
        nextLevelBut.gameObject.SetActive(false);
        LevelManager.Instance.LevelCreated();
        UpdateLevelNumber();
    }

    public void UpdateLevelNumber()
    {
        int levelNumber = PlayerPrefs.GetInt(Tags.Level) + 1;
        levelText.text = $"{Tags.Level} {levelNumber}";
    }

    public void ButtonReference()
    {
        playBut = transform.GetChild(0).GetComponent<Button>();
        nextLevelBut = transform.GetChild(1).GetComponent<Button>();
        restartBut = transform.GetChild(2).GetComponent<Button>();
    }
}