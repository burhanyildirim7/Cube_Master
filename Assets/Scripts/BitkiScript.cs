using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BitkiScript : MonoBehaviour
{
    [SerializeField] private GameObject _acilacakBitki;

    void Start()
    {
        _acilacakBitki.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cember")
        {
            _acilacakBitki.transform.localScale = Vector3.zero;
            _acilacakBitki.SetActive(true);
            _acilacakBitki.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        }
        else
        {

        }
    }
}
