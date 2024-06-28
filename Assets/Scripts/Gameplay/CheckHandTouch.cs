using Calientamanos.Enums;
using UnityEngine;

namespace Calientamanos.Gameplay
{
    public class CheckHandTouch : MonoBehaviour
    {
        [SerializeField] private EHand hand;

        private EHand enemyHand;

        public delegate void HandTouched(EHand hand, EHand enemyHand);
        public static event HandTouched OnHandTouched;

        public EHand Hand { get => hand; }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"OnCollisionEnter with {collision.transform.name}");

            if (collision == null)
            {
                Debug.LogError("Collision is null");
                return;
            }

            if (collision.transform.CompareTag("Player"))
            {
                if (!collision.transform.TryGetComponent<CheckHandTouch>(out var enemyCheckHandTouch))
                {
                    Debug.LogError("Collided object does not have CheckHandTouch component");
                    return;
                }

                enemyHand = enemyCheckHandTouch.Hand;
                OnHandTouched?.Invoke(hand, enemyHand);
            }
        }
    }
}