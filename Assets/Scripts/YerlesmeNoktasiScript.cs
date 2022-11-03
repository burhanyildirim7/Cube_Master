using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YerlesmeNoktasiScript : MonoBehaviour
{
    [Header("Elle Girilecek")]
    public int _yerlesmeNoktasiNumber;
    [SerializeField] private Material _standartMaterial;
    [SerializeField] private Material _aktifMaterial;
    public Material _bosMaterial;
    [Header("Kod Ayarliyor")]
    public bool _yerlesmeNoktasiDoluMu;

    public GameObject _yerlesenKup;

    public bool _kupDogru;



    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().material = _standartMaterial;
        _yerlesmeNoktasiDoluMu = false;
        gameObject.GetComponent<Outline>().enabled = false;
        _kupDogru = false;
    }

    private void FixedUpdate()
    {
        /*
        if (_yerlesenKup != null)
        {
            _yerlesmeNoktasiDoluMu = true;
        }
        else
        {

        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KontrolObjesi")
        {
            if (_yerlesmeNoktasiDoluMu == false)
            {
                if (PlayerController.instance._yerBelirlendi == false)
                {
                    if (_yerlesmeNoktasiDoluMu == false)
                    {
                        gameObject.GetComponent<MeshRenderer>().material = _aktifMaterial;
                    }
                    else
                    {

                    }

                    PlayerController.instance._yerBelirlendi = true;
                    //_yerlesmeNoktasiDoluMu = true;

                    if (other.gameObject.transform.parent.gameObject.GetComponent<KupScript>()._kupYerlesti == false)
                    {
                        other.gameObject.transform.parent.gameObject.GetComponent<KupScript>()._yerlesecegiNokta = gameObject;

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
                // Burada Yer Degistirme Kodlari Olacak
                /*
                
                if (other.gameObject != _yerlesenKup.transform.GetChild(0).gameObject)
                {
                    if (_yerlesmeNoktasiNumber > 0)
                    {
                        if (gameObject.transform.parent.gameObject.GetComponent<YerlestirilecekNoktalarParent>()._yerlestirilecekNoktalarList[_yerlesmeNoktasiNumber - 1].GetComponent<YerlesmeNoktasiScript>()._yerlesmeNoktasiDoluMu == false)
                        {
                            _yerlesenKup.GetComponent<KupScript>()._yerlesecegiNokta = gameObject.transform.parent.gameObject.GetComponent<YerlestirilecekNoktalarParent>()._yerlestirilecekNoktalarList[_yerlesmeNoktasiNumber - 1];

                            _yerlesenKup.GetComponent<KupScript>().KupYeriniDegistir(gameObject.transform.parent.gameObject.GetComponent<YerlestirilecekNoktalarParent>()._yerlestirilecekNoktalarList[_yerlesmeNoktasiNumber - 1].transform);

                            _yerlesenKup = null;

                            _yerlesmeNoktasiDoluMu = false;

                            gameObject.GetComponent<MeshRenderer>().material = _standartMaterial;

                            //_yerlesmeNoktasiDoluMu = true;

                            if (other.gameObject.transform.parent.gameObject.GetComponent<KupScript>()._kupYerlesti == false)
                            {
                                other.gameObject.transform.parent.gameObject.GetComponent<KupScript>()._yerlesecegiNokta = gameObject;

                            }
                            else
                            {

                            }


                        }
                        else
                        {
                            if (_yerlesmeNoktasiNumber < gameObject.transform.parent.gameObject.GetComponent<YerlestirilecekNoktalarParent>()._yerlestirilecekNoktalarList.Count - 1)
                            {
                                if (gameObject.transform.parent.gameObject.GetComponent<YerlestirilecekNoktalarParent>()._yerlestirilecekNoktalarList[_yerlesmeNoktasiNumber + 1].GetComponent<YerlesmeNoktasiScript>()._yerlesmeNoktasiDoluMu == false)
                                {
                                    _yerlesenKup.GetComponent<KupScript>()._yerlesecegiNokta = gameObject.transform.parent.gameObject.GetComponent<YerlestirilecekNoktalarParent>()._yerlestirilecekNoktalarList[_yerlesmeNoktasiNumber + 1];

                                    _yerlesenKup.GetComponent<KupScript>().KupYeriniDegistir(gameObject.transform.parent.gameObject.GetComponent<YerlestirilecekNoktalarParent>()._yerlestirilecekNoktalarList[_yerlesmeNoktasiNumber + 1].transform);

                                    _yerlesenKup = null;

                                    _yerlesmeNoktasiDoluMu = false;

                                    gameObject.GetComponent<MeshRenderer>().material = _standartMaterial;

                                    //_yerlesmeNoktasiDoluMu = true;

                                    if (other.gameObject.transform.parent.gameObject.GetComponent<KupScript>()._kupYerlesti == false)
                                    {
                                        other.gameObject.transform.parent.gameObject.GetComponent<KupScript>()._yerlesecegiNokta = gameObject;

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
                    }
                    else
                    {
                        if (_yerlesmeNoktasiNumber < gameObject.transform.parent.gameObject.GetComponent<YerlestirilecekNoktalarParent>()._yerlestirilecekNoktalarList.Count - 1)
                        {
                            if (gameObject.transform.parent.gameObject.GetComponent<YerlestirilecekNoktalarParent>()._yerlestirilecekNoktalarList[_yerlesmeNoktasiNumber + 1].GetComponent<YerlesmeNoktasiScript>()._yerlesmeNoktasiDoluMu == false)
                            {
                                _yerlesenKup.GetComponent<KupScript>()._yerlesecegiNokta = gameObject.transform.parent.gameObject.GetComponent<YerlestirilecekNoktalarParent>()._yerlestirilecekNoktalarList[_yerlesmeNoktasiNumber + 1];

                                _yerlesenKup.GetComponent<KupScript>().KupYeriniDegistir(gameObject.transform.parent.gameObject.GetComponent<YerlestirilecekNoktalarParent>()._yerlestirilecekNoktalarList[_yerlesmeNoktasiNumber + 1].transform);

                                _yerlesenKup = null;

                                _yerlesmeNoktasiDoluMu = false;

                                gameObject.GetComponent<MeshRenderer>().material = _standartMaterial;

                                //_yerlesmeNoktasiDoluMu = true;

                                if (other.gameObject.transform.parent.gameObject.GetComponent<KupScript>()._kupYerlesti == false)
                                {
                                    other.gameObject.transform.parent.gameObject.GetComponent<KupScript>()._yerlesecegiNokta = gameObject;

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
                }
                else
                {

                }

                */


                Debug.Log("Bu Nokta Dolu");
            }


        }
        else
        {

        }
    }

    private void OnTriggerStay(Collider other)
    {
        /*
        if (_yerlesmeNoktasiDoluMu == true)
        {
            if (other.gameObject.tag == "KontrolObjesi")
            {
                if (other.gameObject.transform.parent.gameObject.GetComponent<KupScript>()._kupNumber == _yerlesmeNoktasiNumber)
                {
                    gameObject.GetComponent<Outline>().enabled = true;
                    gameObject.GetComponent<MeshRenderer>().material = _bosMaterial;
                }
                else
                {
                    gameObject.GetComponent<Outline>().enabled = false;
                    gameObject.GetComponent<MeshRenderer>().material = _bosMaterial;
                }
            }
            else
            {
                //gameObject.GetComponent<Outline>().enabled = false;
            }
        }
        else
        {
            gameObject.GetComponent<Outline>().enabled = false;
        }
        */
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "KontrolObjesi")
        {
            if (_yerlesmeNoktasiDoluMu == false)
            {
                gameObject.GetComponent<MeshRenderer>().material = _standartMaterial;
            }
            else
            {

            }

            PlayerController.instance._yerBelirlendi = false;
            other.gameObject.transform.parent.gameObject.GetComponent<KupScript>()._tekrarYerlesebilir = true;

            if (other.gameObject.transform.parent.gameObject.GetComponent<KupScript>()._kupYerlesti == false)
            {
                other.gameObject.transform.parent.gameObject.GetComponent<KupScript>()._yerlesecegiNokta = null;

                // Burada Eski Yerine Geri Donme Kodlari Yazilacak
                /*
                if (other.gameObject != _yerlesenKup.transform.GetChild(0).gameObject)
                {
                    _yerlesenKup.GetComponent<KupScript>()._yerlesecegiNokta = gameObject.transform.parent.gameObject.GetComponent<YerlestirilecekNoktalarParent>()._yerlestirilecekNoktalarList[_yerlesmeNoktasiNumber];

                    _yerlesenKup.GetComponent<KupScript>().KupYeriniDegistir(gameObject.transform.parent.gameObject.GetComponent<YerlestirilecekNoktalarParent>()._yerlestirilecekNoktalarList[_yerlesmeNoktasiNumber].transform);

                    _yerlesmeNoktasiDoluMu = true;
                }
                else
                {

                }
                */

            }
            else
            {

            }

        }
        else
        {

        }
    }

    public void YerBosalt()
    {
        gameObject.GetComponent<MeshRenderer>().material = _bosMaterial;
        gameObject.GetComponent<Outline>().enabled = false;
    }
}
