using DG.Tweening;
using UnityEngine;

public class FlameParticle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        transform.DOShakePosition(15, 0.05f, 5, 90, false, false, ShakeRandomnessMode.Full)
            .SetLoops(-1, LoopType.Restart);
    }

    void OnDisable()
    {
        transform.DOKill();
    }
}
