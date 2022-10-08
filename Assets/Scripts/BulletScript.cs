using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int _bulletRenkKodu;

    [SerializeField] private Material _trailMaterialBeyaz, _trailMaterialSari, _trailMaterialMavi, _trailMaterialYesil, _trailMaterialKirmizi;

    void Start()
    {
        Destroy(gameObject, 0.75f);

        if (_bulletRenkKodu == 0)
        {
            gameObject.GetComponent<TrailRenderer>().material = _trailMaterialBeyaz;
        }
        else if (_bulletRenkKodu == 1)
        {
            gameObject.GetComponent<TrailRenderer>().material = _trailMaterialSari;
        }
        else if (_bulletRenkKodu == 2)
        {
            gameObject.GetComponent<TrailRenderer>().material = _trailMaterialMavi;
        }
        else if (_bulletRenkKodu == 3)
        {
            gameObject.GetComponent<TrailRenderer>().material = _trailMaterialYesil;
        }
        else if (_bulletRenkKodu == 4)
        {
            gameObject.GetComponent<TrailRenderer>().material = _trailMaterialKirmizi;
        }
        else
        {

        }

    }


    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * 25 * Time.deltaTime);
    }
}
