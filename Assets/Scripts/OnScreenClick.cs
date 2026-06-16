using UnityEngine;
using UnityEngine.InputSystem;

// For spawning particles on click
public class OnScreenClick : MonoBehaviour
{
    [SerializeField] private InputActionAsset _actions;
    [SerializeField] private GameObject _tapParticle;
    [SerializeField] private float screenDistance = 1f; // Ensure this is positive
    [SerializeField] private AudioClip audioClip;

    private InputAction _tapAction;
    private Camera _camera;
    public bool canClick = true;

    private void Awake()
    {
        _tapAction = _actions.FindAction("QuickTap");
        _actions.Enable();
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        if (_tapAction != null) _tapAction.performed += OnQuickTap;
    }

    private void OnDisable()
    {
        if (_tapAction != null) _tapAction.performed -= OnQuickTap;
    }

    public void OnQuickTap(InputAction.CallbackContext context)
    {
        if (!canClick) return;

        Vector2 mousePos = Pointer.current.position.ReadValue();

        // 1. Create the particle as a child of the Canvas
        GameObject particle = Instantiate(_tapParticle, transform);

        // 2. Simply set the position to the raw mouse position
        // In Screen Space - Overlay, 1 unit = 1 pixel.
        particle.transform.position = mousePos;

        // if (SoundFXManager.instance != null)
        // {
        //     SoundFXManager.instance.PlaySoundFX(audioClip, transform, 1f);
        // }
    }
}