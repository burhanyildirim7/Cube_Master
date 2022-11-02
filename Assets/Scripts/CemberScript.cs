using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CemberScript : MonoBehaviour
{
    [SerializeField] private GameObject _acilacakCember;

    void Start()
    {
        CemberAktif();
    }

    public void CemberAktif()
    {
        _acilacakCember.SetActive(true);
        _acilacakCember.transform.DOScale(new Vector3(100, 100, 100), 10f);
    }
}
