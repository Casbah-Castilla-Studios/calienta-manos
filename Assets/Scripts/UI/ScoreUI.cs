using Calientamanos.Enums;
using Calientamanos.Managers;
using TMPro;
using UnityEngine;

namespace Calientamanos.UI
{
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

        private void HandleHandScore(EHand hand, int points)
        {
            if (hand == EHand.White)
            {
                whiteHandPointsTMP.text = points.ToString();
            }

            if (hand == EHand.Black)
            {
                blackHandPointsTMP.text = points.ToString();
            }
        }

        private void OnDestroy()
        {
            GameManager.OnHandScore -= HandleHandScore;
        }
    }
}