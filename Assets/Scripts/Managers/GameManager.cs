using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject whiteHand;
    [SerializeField] private GameObject blackHand;
    private Hand handAttacking;
    private int whiteHandPoints;
    private int blackHandPoints;

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
            Debug.Log("Punto para la mano blanca. " +  whiteHandPoints);
        }

        if (attackerHand == Hand.Black)
        {
            blackHandPoints++;
            Debug.Log("Punto para la mano negra. " + blackHandPoints);
        }
    }
}
