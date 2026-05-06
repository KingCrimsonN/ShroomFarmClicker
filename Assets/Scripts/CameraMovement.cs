using DG.Tweening;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector3 farmPosition;
    [SerializeField] private Vector3 brewPosition;
    [SerializeField] private Vector3 shopPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = brewPosition;
    }

    public void MoveToFarm()
    {
        transform.DOMove(farmPosition, 1f);
    }

    public void MoveToBrew()
    {
        transform.DOMove(brewPosition, 1f);
    }

    public void MoveToShop()
    {
        transform.DOMove(shopPosition, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
