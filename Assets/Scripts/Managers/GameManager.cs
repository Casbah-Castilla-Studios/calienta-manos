using Calientamanos.Enums;
using Calientamanos.Gameplay;
using Calientamanos.Utils;
using UnityEngine;

namespace Calientamanos.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GameObject whiteHand;
        [SerializeField] private GameObject blackHand;
        [SerializeField] private EHand handAttacking;
        private int whiteHandPoints;
        private int blackHandPoints;

        public delegate void HandScore(EHand hand, int points);
        public static event HandScore OnHandScore;

        protected override void Awake()
        {
            base.Awake();
            handAttacking = EHand.Black;
            CheckHandTouch.OnHandTouched += HandleHandTouched;
            whiteHandPoints = 0;
            blackHandPoints = 0;
        }

        private void OnDestroy()
        {
            CheckHandTouch.OnHandTouched -= HandleHandTouched;
        }

        private void HandleHandTouched(EHand attackerHand, EHand defenderHand)
        {
            if (attackerHand != handAttacking) return;

            if (attackerHand == EHand.White)
            {
                whiteHandPoints++;
                OnHandScore?.Invoke(handAttacking, whiteHandPoints);
                Debug.Log("Punto para la mano blanca. " + whiteHandPoints);
            }

            if (attackerHand == EHand.Black)
            {
                blackHandPoints++;
                OnHandScore?.Invoke(handAttacking, blackHandPoints);
                Debug.Log("Punto para la mano negra. " + blackHandPoints);
            }
        }

    }
}