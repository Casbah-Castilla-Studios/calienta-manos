using Calientamanos.Enums;
using UnityEngine;

namespace Calientamanos.Gameplay
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private EHand handType;
        [SerializeField] private float slapCooldown = 1.0f;
        [SerializeField] private float transitionDuration = 0.5f; // Duration for smooth transition

        [SerializeField] private bool canSlap;

        // Event to notify when a hand is slapped
        public static event System.Action<EHand> OnHandSlapped;

        private void Awake()
        {
            CheckHandTouch.OnHandTouched += HandleHandTouched;
            canSlap = EHand.Black == handType;
        }

        private void OnDestroy()
        {
            CheckHandTouch.OnHandTouched -= HandleHandTouched;
        }

        private void HandleHandTouched(EHand attackerHand, EHand defenderHand)
        {
            if (attackerHand == handType)
            {
                OnHandSlapped?.Invoke(defenderHand);
                Debug.Log($"{handType} hand slapped {defenderHand} hand!");
            }
        }
    }
}
