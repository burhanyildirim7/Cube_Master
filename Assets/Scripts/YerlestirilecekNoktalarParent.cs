using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Cinemachine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class YerlestirilecekNoktalarParent : MonoBehaviour
{
    public int _yerlesecekKupSayisi;

    public Slider _gokkusagiSlider;

    public GameObject _cemberObje;

    private GameObject _camera;

    private CinemachineVirtualCamera _cinemachine;

    public List<ParticleSystem> _confettiList = new List<ParticleSystem>();

    public List<GameObject> _yerlestirilecekNoktalarList = new List<GameObject>();

    private int _dogruYerlesenKupSayisi;

    private bool _camerayiHareketEttir;

    private PlayerController _playerController;

    private void Start()
    {
        _gokkusagiSlider.value = 0;
        _camera = GameObject.FindGameObjectWithTag("Cinemachine");
        _cinemachine = _camera.GetComponent<CinemachineVirtualCamera>();

        _cinemachine.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0f, 15f, -10f);

        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        _camerayiHareketEttir = false;

        _cemberObje.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (_camerayiHareketEttir)
        {
            _cinemachine.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(_cinemachine.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, new Vector3(0f, 70f, -50f), 0.01f);
        }
        else
        {

        }
    }

    public void DogrulukSorgula()
    {
        _dogruYerlesenKupSayisi = 0;

        for (int i = 0; i < _yerlestirilecekNoktalarList.Count; i++)
        {
            if (_yerlestirilecekNoktalarList[i].GetComponent<YerlesmeNoktasiScript>()._kupDogru)
            {
                _dogruYerlesenKupSayisi++;

            }
            else
            {

            }
        }

        if (_dogruYerlesenKupSayisi == _yerlesecekKupSayisi)
        {

            StartCoroutine(FinishEvent());
            Debug.Log("Oyunu Kazandi!!!!");
        }
        else
        {

        }
    }

    private IEnumerator FinishEvent()
    {
        GameController.instance.isContinue = false;

        _playerController.PlayerCoinGuncelle(_yerlesecekKupSayisi);

        _playerController.PlayerDansEt();

        for (int i = 0; i < _confettiList.Count; i++)
        {
            _confettiList[i].Play();
        }

        yield return new WaitForSeconds(1f);

        _cemberObje.SetActive(true);

        _camerayiHareketEttir = true;

        _gokkusagiSlider.value = 0;
        _gokkusagiSlider.DOValue(1, 3f);

        yield return new WaitForSeconds(7f);

        _playerController.PlayerKazandi();
    }


}
