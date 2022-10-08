using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EnemyOyunSonu : MonoBehaviour
{
    public int _enemyLevel;

    private int _yazacakLevel;

    [SerializeField] private TextMeshProUGUI _levelTexti;

    [SerializeField] private GameObject _enemyKarakter;

    [SerializeField] private GameObject _splashObject;

    [SerializeField] private ParticleSystem _coinSplash;

    // Start is called before the first frame update
    void Start()
    {
        _yazacakLevel = _enemyLevel;
        _levelTexti.text = "Lv " + _yazacakLevel.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (PlayerController.instance._playerLevel >= _enemyLevel)
            {
                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

                PlayerController.instance.PlayerLevelGuncelle(1);

                _enemyKarakter.SetActive(false);

                gameObject.GetComponent<Collider>().enabled = false;

                _coinSplash.Play();

                _splashObject.SetActive(true);

                _levelTexti.gameObject.SetActive(false);
            }
            else
            {

            }

            Destroy(other.gameObject);
        }
        else
        {

        }


    }




}
