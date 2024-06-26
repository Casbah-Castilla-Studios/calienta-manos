using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject whiteHand;
    [SerializeField] private GameObject blackHand;
    [SerializeField] private Hand handAttacking;
    private int whiteHandPoints;
    private int blackHandPoints;

    public delegate void HandScore(Hand hand, int points);
    public static event HandScore OnHandScore;

    private void Awake()
    {
        handAttacking = Hand.Black;
        CheckHandTouch.OnHandTouched += HandleHandTouched;
        whiteHandPoints = 0;
        blackHandPoints = 0;
    }

    private void HandleHandTouched(Hand attackerHand, Hand defenderHand)
    {
        if (attackerHand != handAttacking) return;

        if (attackerHand == Hand.White)
        {
            whiteHandPoints++;
            OnHandScore?.Invoke(handAttacking, whiteHandPoints);
            Debug.Log("Punto para la mano blanca. " + whiteHandPoints);
        }

        if (attackerHand == Hand.Black)
        {
            blackHandPoints++;
            OnHandScore?.Invoke(handAttacking, blackHandPoints);
            Debug.Log("Punto para la mano negra. " + blackHandPoints);
        }
    }

    private void OnDestroy()
    {
        CheckHandTouch.OnHandTouched -= HandleHandTouched;
    }
}
