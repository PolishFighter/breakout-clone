using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private string scoreLabel = "SCORE ";
    private Text textUI;

    void Awake()
    {
        this.textUI = GetComponent<Text>();
    }
    public void SetScore(int score)
    {  
        this.textUI.text = this.scoreLabel + score;
    }
}
