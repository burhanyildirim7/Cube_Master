using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ZeminScript : MonoBehaviour
{
    [SerializeField] private Material _kahverengi;
    [SerializeField] private Material _yesil;

    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = _kahverengi;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cember")
        {
            gameObject.GetComponent<MeshRenderer>().material = _yesil;
            transform.DOLocalMoveY(0.5f, 0.2f).OnComplete(() => transform.DOLocalMoveY(0f, 0.2f));
        }
        else
        {

        }
    }
}
