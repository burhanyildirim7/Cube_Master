using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamerayaBak : MonoBehaviour
{

    [SerializeField] private GameObject _cameraObjesi;


    private void FixedUpdate()
    {
        transform.LookAt(_cameraObjesi.transform.position);
    }

}
