using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class AnimationController : MonoBehaviour
{
    // Referencias a los Animators de ambos jugadores
    public Animator player1Animator;
    public Animator player2Animator;

    // Referencia al Input Action Asset
    [SerializeField] private InputActionAsset inputActionAsset;

    // Input Actions
    private InputAction player1AttackAction;
    private InputAction player1RetireAction;
    private InputAction player2AttackAction;
    private InputAction player2RetireAction;

    // Duración del Lerp (ajústala para cambiar la velocidad de la transición)
    public float lerpDuration = 0.5f; // Reducir este valor hará que la animación sea más rápida

    // Coroutines actuales
    private Coroutine player1AttackCoroutine;
    private Coroutine player1RetireCoroutine;
    private Coroutine player2AttackCoroutine;
    private Coroutine player2RetireCoroutine;

    private void Awake()
    {
        // Asignar el mapa de acciones
        var handInteractions = inputActionAsset.FindActionMap("HandInteractions");

        // Asignar las acciones
        player1AttackAction = handInteractions.FindAction("AttackWhite");
        player1RetireAction = handInteractions.FindAction("RetireWhite");
        player2AttackAction = handInteractions.FindAction("AttackBlack");
        player2RetireAction = handInteractions.FindAction("RetireBlack");

        // Asignar los callbacks
        player1AttackAction.performed += ctx => StartLerp(ref player1AttackCoroutine, player1Animator, "Attack", 1f);
        player1AttackAction.canceled += ctx => StartLerp(ref player1AttackCoroutine, player1Animator, "Attack", 0f);
        player1RetireAction.performed += ctx => StartLerp(ref player1RetireCoroutine, player1Animator, "Retire", 1f);
        player1RetireAction.canceled += ctx => StartLerp(ref player1RetireCoroutine, player1Animator, "Retire", 0f);

        player2AttackAction.performed += ctx => StartLerp(ref player2AttackCoroutine, player2Animator, "Attack", 1f);
        player2AttackAction.canceled += ctx => StartLerp(ref player2AttackCoroutine, player2Animator, "Attack", 0f);
        player2RetireAction.performed += ctx => StartLerp(ref player2RetireCoroutine, player2Animator, "Retire", 1f);
        player2RetireAction.canceled += ctx => StartLerp(ref player2RetireCoroutine, player2Animator, "Retire", 0f);
    }

    private void OnEnable()
    {
        // Habilitar el mapa de acciones
        player1AttackAction.Enable();
        player1RetireAction.Enable();
        player2AttackAction.Enable();
        player2RetireAction.Enable();
    }

    private void OnDisable()
    {
        // Deshabilitar el mapa de acciones
        player1AttackAction.Disable();
        player1RetireAction.Disable();
        player2AttackAction.Disable();
        player2RetireAction.Disable();
    }

    private void StartLerp(ref Coroutine coroutine, Animator animator, string parameterName, float targetValue)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(SlerpParameter(animator, parameterName, targetValue));
    }

    private IEnumerator SlerpParameter(Animator animator, string parameterName, float targetValue)
    {
        float startValue = animator.GetFloat(parameterName);
        Vector3 startVector = new Vector3(startValue, 0, 0);
        Vector3 targetVector = new Vector3(targetValue, 0, 0);
        float timeElapsed = 0f;

        Debug.Log($"Starting Slerp: {parameterName} from {startValue} to {targetValue} over {lerpDuration} seconds");

        while (timeElapsed < lerpDuration)
        {
            timeElapsed += Time.deltaTime;
            Vector3 newVector = Vector3.Slerp(startVector, targetVector, timeElapsed / lerpDuration);
            float newValue = newVector.x; // Extract the interpolated value
            Debug.Log($"Slerping {parameterName}: {newValue} at {timeElapsed}/{lerpDuration}");
            animator.SetFloat(parameterName, newValue);
            yield return null;
        }

        animator.SetFloat(parameterName, targetValue); // Ensure the target value is set at the end
        Debug.Log($"Completed Slerp: {parameterName} set to {targetValue}");
    }
}
