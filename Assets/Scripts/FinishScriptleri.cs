using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScriptleri : MonoBehaviour
{
    [SerializeField] private int _blokLeveli;

    [SerializeField] private ParticleSystem _confetti1;
    [SerializeField] private ParticleSystem _confetti2;

    [SerializeField] private GameObject _finishBlok;

    [SerializeField] private Material _finishBlokMat;

    [SerializeField] private float _carpimDegeri;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (PlayerController.instance._playerLevel >= 500 && _blokLeveli == 500)
            {
                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

                GameController.instance.isContinue = false;
                _confetti1.Play();
                _confetti2.Play();
                _finishBlok.GetComponent<Renderer>().material = _finishBlokMat;
                PlayerController.instance.CarpimDegeriGuncelle(_carpimDegeri);
                PlayerController.instance.PlayerKazandi();
            }
            else
            {
                if (PlayerController.instance._playerLevel <= (_blokLeveli + 10))
                {
                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

                    GameController.instance.isContinue = false;
                    _confetti1.Play();
                    _confetti2.Play();
                    _finishBlok.GetComponent<Renderer>().material = _finishBlokMat;
                    PlayerController.instance.CarpimDegeriGuncelle(_carpimDegeri);
                    PlayerController.instance.PlayerKazandi();
                }
                else
                {
                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

                    _confetti1.Play();
                    _confetti2.Play();
                    _finishBlok.GetComponent<Renderer>().material = _finishBlokMat;

                }
            }

        }
        else
        {

        }
    }
}
