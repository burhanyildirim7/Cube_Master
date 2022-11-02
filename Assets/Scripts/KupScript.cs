using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class KupScript : MonoBehaviour
{
    [Header("Elle Girilecek")]
    public int _kupNumber;
    public ParticleSystem _tozEffect;
    [SerializeField] private ParticleSystem _starEffect;

    [Header("Kod Ayarliyor")]
    public GameObject _yerlesecegiNokta;
    public bool _tasiniyorMu;
    public bool _kupYerlesti;

    private float _timer;

    private bool _playerlaTemasta;

    public bool _tekrarYerlesebilir;

    private GameObject _sonBulunduguNokta;

    private Vector3 _ilkKonum;

    public bool _duvarIcinde;

    void Start()
    {
        _tasiniyorMu = false;
        _yerlesecegiNokta = null;
        _kupYerlesti = false;
        _playerlaTemasta = false;
        _tekrarYerlesebilir = true;
        _ilkKonum = gameObject.transform.position;
        _duvarIcinde = false;
    }


    void Update()
    {
        if (_yerlesecegiNokta != null)
        {
            if (_tasiniyorMu == true)
            {
                if (JoystickController.instance._velocityX == 0 && JoystickController.instance._velocityZ == 0)
                {
                    if (_kupYerlesti == false)
                    {
                        if (_tekrarYerlesebilir == true)
                        {
                            KupYerlestir();
                            _kupYerlesti = true;
                        }
                        else
                        {

                        }

                    }
                    else
                    {

                    }

                }
                else
                {

                }
            }
            else
            {
                if (gameObject.transform.parent == null)
                {
                    if (_kupYerlesti == false)
                    {
                        KupYerlestir();
                        _kupYerlesti = true;
                    }
                    else
                    {

                    }
                }
                else
                {

                }


            }
        }
        else
        {

        }

        if (GameController.instance.isContinue)
        {
            if (_playerlaTemasta && PlayerController.instance._elindeParcaVarMi == false)
            {
                _timer += Time.deltaTime;

                PlayerController.instance._timerSlider.value = _timer;

                if (_timer > 0.5f)
                {
                    PlayerController.instance.KupYerlestirPlayer();
                    //_yerlesecegiNokta.GetComponent<YerlesmeNoktasiScript>()._yerlesmeNoktasiDoluMu = false;
                    //_yerlesecegiNokta.GetComponent<YerlesmeNoktasiScript>().YerBosalt();
                    _playerlaTemasta = false;
                }
                else
                {

                }
            }
            else
            {

            }
        }
        else
        {
            PlayerController.instance._timerSlider.value = 0;
        }


        if (_duvarIcinde == true && _tasiniyorMu == false)
        {
            gameObject.transform.parent = null;
            gameObject.transform.DOKill();
            gameObject.transform.DOMove(_ilkKonum, 0.5f).OnComplete(() => _tozEffect.Play());
            _tasiniyorMu = false;
            _duvarIcinde = false;
        }
        else
        {

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerTemas")
        {
            if (_kupYerlesti)
            {
                if (gameObject.transform.localPosition == Vector3.zero)
                {
                    gameObject.transform.DOLocalMoveY(0.5f, 0.2f);
                    PlayerController.instance._yerlestirilmisKupSecildi = gameObject;
                    _playerlaTemasta = true;
                    _timer = 0;
                }
                else
                {

                }

            }
            else
            {

            }
        }
        else if (other.CompareTag("Duvar"))
        {
            _duvarIcinde = true;
            Debug.Log("Duvar Icinde");
        }
        else
        {

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerTemas")
        {
            if (_kupYerlesti)
            {
                gameObject.transform.DOLocalMoveY(0f, 0.2f);
                _playerlaTemasta = false;
                _timer = 0;

            }
            else
            {

            }
        }
        else if (other.CompareTag("Duvar"))
        {
            _duvarIcinde = false;
        }
        else
        {

        }
    }

    private void KupYerlestir()
    {
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

        gameObject.transform.parent = _yerlesecegiNokta.transform;
        gameObject.transform.DOKill();
        gameObject.transform.DOLocalJump(Vector3.zero, 2, 1, 0.5f).OnComplete(() => EffectCalistir());
        gameObject.transform.DOLocalRotate(Vector3.zero, 0.5f);
        PlayerController.instance._elindeParcaVarMi = false;
        _yerlesecegiNokta.GetComponent<YerlesmeNoktasiScript>()._yerlesmeNoktasiDoluMu = true;
        _yerlesecegiNokta.GetComponent<YerlesmeNoktasiScript>()._yerlesenKup = gameObject;
        _yerlesecegiNokta.GetComponent<MeshRenderer>().material = _yerlesecegiNokta.GetComponent<YerlesmeNoktasiScript>()._bosMaterial;

        gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
        //_yerlesecegiNokta.GetComponent<MeshRenderer>().material = _yerlesecegiNokta.GetComponent<YerlesmeNoktasiScript>()._bosMaterial;

        //GameObject.FindGameObjectWithTag("YerlestirilecekNoktalarParent").GetComponent<YerlestirilecekNoktalarParent>().DogrulukSorgula();

        //Debug.Log("Burayi Tamamladi");

    }

    public void KupYeriniDegistir(Transform transform)
    {
        _sonBulunduguNokta = _yerlesecegiNokta;

        gameObject.transform.parent = _yerlesecegiNokta.transform;
        gameObject.transform.DOKill();
        gameObject.transform.DOMove(transform.position, 0.2f).OnComplete(() => EffectCalistir());
        gameObject.transform.DOLocalRotate(Vector3.zero, 0.2f);
        //PlayerController.instance._elindeParcaVarMi = false;
        _yerlesecegiNokta.GetComponent<YerlesmeNoktasiScript>()._yerlesmeNoktasiDoluMu = true;
        _yerlesecegiNokta.GetComponent<YerlesmeNoktasiScript>()._yerlesenKup = gameObject;
        _yerlesecegiNokta.GetComponent<MeshRenderer>().material = _yerlesecegiNokta.GetComponent<YerlesmeNoktasiScript>()._bosMaterial;

        //GameObject.FindGameObjectWithTag("YerlestirilecekNoktalarParent").GetComponent<YerlestirilecekNoktalarParent>().DogrulukSorgula();
    }

    private void EffectCalistir()
    {
        if (_kupNumber == _yerlesecegiNokta.GetComponent<YerlesmeNoktasiScript>()._yerlesmeNoktasiNumber)
        {
            _tozEffect.Play();
            _starEffect.Play();
            _yerlesecegiNokta.GetComponent<YerlesmeNoktasiScript>()._kupDogru = true;

            GameObject.FindGameObjectWithTag("YerlestirilecekNoktalarParent").GetComponent<YerlestirilecekNoktalarParent>().DogrulukSorgula();
        }
        else
        {
            _tozEffect.Play();
            _yerlesecegiNokta.GetComponent<YerlesmeNoktasiScript>()._kupDogru = false;
        }
    }
}
