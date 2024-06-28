using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator playerAnimator;

    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private InputActionReference retireAction;

    // Input Actions
    private InputAction playerAttackAction;
    private InputAction playerRetireAction;

    // Duración del Lerp (ajústala para cambiar la velocidad de la transición)
    public float lerpDuration = 0.5f; // Reducir este valor hará que la animación sea más rápida

    // Coroutines actuales
    private Coroutine playerAttackCoroutine;
    private Coroutine playerRetireCoroutine;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();

        // Asignar las acciones
        playerAttackAction = attackAction.ToInputAction();
        playerRetireAction = retireAction.ToInputAction();

        // Asignar los callbacks
        playerAttackAction.performed += ctx => StartLerp(ref playerAttackCoroutine, playerAnimator, "Attack", 1f);
        playerAttackAction.canceled += ctx => StartLerp(ref playerAttackCoroutine, playerAnimator, "Attack", 0f);
        playerRetireAction.performed += ctx => StartLerp(ref playerRetireCoroutine, playerAnimator, "Retire", 1f);
        playerRetireAction.canceled += ctx => StartLerp(ref playerRetireCoroutine, playerAnimator, "Retire", 0f);
    }

    private void OnEnable()
    {
        // Habilitar el mapa de acciones
        playerAttackAction.Enable();
        playerRetireAction.Enable();
    }

    private void OnDisable()
    {
        // Deshabilitar el mapa de acciones
        playerAttackAction.Disable();
        playerRetireAction.Disable();
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
        float timeElapsed = 0f;

        while (timeElapsed < lerpDuration)
        {
            timeElapsed += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, targetValue, timeElapsed / lerpDuration);
            animator.SetFloat(parameterName, newValue);
            yield return null;
        }

        animator.SetFloat(parameterName, targetValue); // Ensure the target value is set at the end
    }
}
