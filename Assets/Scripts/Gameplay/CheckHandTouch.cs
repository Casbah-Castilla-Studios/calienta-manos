using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public enum Hand { White, Black};

public class CheckHandTouch : MonoBehaviour
{
    [SerializeField] private Hand hand;

    private Hand enemyHand;
    private BoxCollider boxCollider;

    public delegate void HandTouched(Hand hand, Hand enemyHand);
    public static event HandTouched OnHandTouched;

    public Hand Hand { get => hand; }

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");

        if (collision == null)
        {
            Debug.LogError("Mano sin box collider");
            return;
        }

        CheckHandTouch enemyCheckHandTouch = collision.transform.GetComponent<CheckHandTouch>();

        if (enemyCheckHandTouch == null)
        {
            Debug.LogError("Objeto colisionado sin componente CheckHandTouch");
            return;
        }

        enemyHand = enemyCheckHandTouch.Hand;
        if(OnHandTouched != null) OnHandTouched(hand, enemyHand);
    }
}
