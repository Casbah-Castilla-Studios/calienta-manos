using UnityEngine;

public enum Hand { White, Black };

public class CheckHandTouch : MonoBehaviour
{
    [SerializeField] private Hand hand;

    private Hand enemyHand;

    public delegate void HandTouched(Hand hand, Hand enemyHand);
    public static event HandTouched OnHandTouched;

    public Hand Hand { get => hand; }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"OnCollisionEnter with {collision.transform.name}");

        if (collision == null)
        {
            Debug.LogError("Mano sin box collider");
            return;
        }

        if (!collision.transform.TryGetComponent<CheckHandTouch>(out var enemyCheckHandTouch))
        {
            Debug.LogError("Objeto colisionado sin componente CheckHandTouch");
            return;
        }

        enemyHand = enemyCheckHandTouch.Hand;
        OnHandTouched?.Invoke(hand, enemyHand);
    }
}
