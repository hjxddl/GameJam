using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoverIcon : MonoBehaviour
{
    [SerializeField] private float speed;
    private void Start() 
    {
        transform.DOLocalMove(new Vector3(0, 2, 0), speed).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
