using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI whiteHandPointsTMP;
    [SerializeField] private TextMeshProUGUI blackHandPointsTMP;

    private void Awake()
    {
        GameManager.OnHandScore += HandleHandScore;
        whiteHandPointsTMP.text = "0";
        blackHandPointsTMP.text = "0";
    }

    private void HandleHandScore(Hand hand, int points)
    {
        if (hand == Hand.White)
        {
            whiteHandPointsTMP.text = points.ToString();
        }

        if (hand == Hand.Black)
        {
            blackHandPointsTMP.text = points.ToString();
        }
    }

    private void OnDestroy()
    {
        GameManager.OnHandScore -= HandleHandScore;
    }
}
