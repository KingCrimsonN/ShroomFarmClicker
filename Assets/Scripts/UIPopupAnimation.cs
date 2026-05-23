using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UiPopupAnimator : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float duration = 0.3f; // Slightly longer for travel distance
    [SerializeField] private AnimationCurve motionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Tooltip("How far below its resting spot the panel starts. 1500-2000 keeps it safely off-screen on most mobile displays.")]
    [SerializeField] private float slideDistance = 1800f;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Coroutine activeAnimation;

    private Vector2 restingAnchoredPosition;
    private Vector2 offScreenAnchoredPosition;
    private bool isOpen = false;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();

        // 1. Capture the exact coordinate where the panel belongs when fully open
        restingAnchoredPosition = rectTransform.anchoredPosition;

        // 2. Project the starting hide-out coordinate directly beneath it
        offScreenAnchoredPosition = restingAnchoredPosition + new Vector2(0, -slideDistance);

        // 3. Teleport off-screen immediately on game start
        rectTransform.anchoredPosition = offScreenAnchoredPosition;
        gameObject.SetActive(false);
    }

    public void OpenPopup()
    {
        if (isOpen) return;
        if (activeAnimation != null) StopCoroutine(activeAnimation);
        gameObject.SetActive(true);

        // Slide up from dark-hidden to bright-resting
        activeAnimation = StartCoroutine(AnimateSlideAndFade(offScreenAnchoredPosition, restingAnchoredPosition, 0f, 1f));
        if (!isOpen) isOpen = true; // Prevents redundant open calls from stacking animations, but allows close->open to work smoothly
    }

    public void ClosePopup()
    {
        if (!isOpen) return;
        if (activeAnimation != null) StopCoroutine(activeAnimation);

        // Slide back down to the hidden zone, then disable object properties
        activeAnimation = StartCoroutine(AnimateSlideAndFade(restingAnchoredPosition, offScreenAnchoredPosition, 1f, 0f, () =>
        {
            gameObject.SetActive(false);
        }));
        if (isOpen) isOpen = false; // Mark as closed after animation completes
    }

    private IEnumerator AnimateSlideAndFade(Vector2 startPos, Vector2 targetPos, float startAlpha, float targetAlpha, System.Action onComplete = null)
    {
        float time = 0;

        // Establish instant initial positions
        canvasGroup.alpha = startAlpha;
        rectTransform.anchoredPosition = startPos;
        canvasGroup.blocksRaycasts = targetAlpha > 0.5f; // Block accidental background taps during transition

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            float progress = motionCurve.Evaluate(time / duration);

            // Interpolate position and fade simultaneously
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, progress);
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, progress);

            yield return null;
        }

        // Lock perfectly into finalized targets
        canvasGroup.alpha = targetAlpha;
        rectTransform.anchoredPosition = targetPos;

        onComplete?.Invoke();
    }
}