//using Calientamanos.Enums;
//using Calientamanos.Gameplay;
//using UnityEngine;

//namespace Calientamanos.Mechanics
//{
//    public class HandMechanic : MonoBehaviour
//    {
//        [SerializeField] private Hand whiteHand;
//        [SerializeField] private Hand blackHand;

//        private EHand currentAttacker;
//        private bool slapMissed;

//        private void Awake()
//        {
//            currentAttacker = EHand.Black;
//            CheckHandTouch.OnHandTouched += HandleHandTouched;
//            Hand.OnHandSlapped += HandleHandSlapped;
//        }

//        private void OnDestroy()
//        {
//            CheckHandTouch.OnHandTouched -= HandleHandTouched;
//            Hand.OnHandSlapped -= HandleHandSlapped;
//        }

//        private void HandleHandTouched(EHand attackerHand, EHand defenderHand)
//        {
//            if (attackerHand == currentAttacker)
//            {
//                // Successful slap, no role inversion needed
//                slapMissed = false;
//            }
//        }

//        private void HandleHandSlapped(EHand defenderHand)
//        {
//            if (currentAttacker == defenderHand)
//            {
//                // Slap missed, invert roles
//                InvertRoles();
//                slapMissed = true;
//            }
//        }

//        private void InvertRoles()
//        {
//            currentAttacker = currentAttacker == EHand.White ? EHand.Black : EHand.White;
//            Debug.Log($"Roles inverted. New attacker: {currentAttacker}");
//        }

//        private void Update()
//        {
//            // Check for slap misses and handle role inversion here if needed
//            if (slapMissed)
//            {
//                InvertRoles();
//                slapMissed = false;
//            }
//        }
//    }
//}