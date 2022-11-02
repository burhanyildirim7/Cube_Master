using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using TMPro;
using System.Data;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public int collectibleDegeri;
    public bool xVarMi = true;
    public bool collectibleVarMi = true;

    [SerializeField] private Animator _animator;

    public bool _elindeParcaVarMi;

    [SerializeField] private GameObject _karakter;

    [SerializeField] private GameObject _tasimaNoktasi;
    [SerializeField] private GameObject _birakmaNoktasi;

    private float _timer;

    public int _incomeDegeri;

    public float _oyunSonuCarpimDegeri;

    public JoystickController _joystickController;

    [HideInInspector] public int _playerLevel;

    private GameObject _tasinanKup;

    public bool _yerBelirlendi;

    public GameObject _yerlestirilmisKupSecildi;

    public Slider _timerSlider;

    private void Awake()
    {
        if (instance == null) instance = this;
        //else Destroy(this);
    }

    void Start()
    {
        StartingEvents();
    }

    private void FixedUpdate()
    {
        if (_elindeParcaVarMi == true)
        {
            _timer += Time.deltaTime;

            if (_joystickController._velocityX == 0 && _joystickController._velocityZ == 0 && _tasinanKup.GetComponent<KupScript>()._yerlesecegiNokta == null)
            {
                if (_timer > 1f)
                {
                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

                    _tasinanKup.transform.parent = null;
                    _tasinanKup.transform.DOJump(_birakmaNoktasi.transform.position, 2, 1, 0.5f).OnComplete(() => _tasinanKup.GetComponent<KupScript>()._tozEffect.Play());
                    _tasinanKup.GetComponent<KupScript>()._tasiniyorMu = false;
                    _elindeParcaVarMi = false;
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

        if (_joystickController._velocityX != 0 || _joystickController._velocityZ != 0)
        {
            _timerSlider.value = 0;
        }
        else
        {

        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "TasinacakKup")
        {
            if (other.gameObject.GetComponent<KupScript>()._kupYerlesti == false)
            {
                if (_elindeParcaVarMi == false)
                {

                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

                    _elindeParcaVarMi = true;
                    other.gameObject.transform.parent = _tasimaNoktasi.transform;
                    other.gameObject.transform.DOLocalJump(Vector3.zero, 2, 1, 0.5f);
                    other.gameObject.transform.DOLocalRotate(Vector3.zero, 0.5f);
                    other.gameObject.GetComponent<KupScript>()._tasiniyorMu = true;
                    _tasinanKup = other.gameObject;

                    _timer = 0;

                    Debug.Log("Tasinmaya Calisiliyor");
                }
                else
                {

                }
            }
            else
            {

            }

            // COLLECTIBLE CARPINCA YAPILACAKLAR...
            //GameController.instance.SetScore(collectibleDegeri); // ORNEK KULLANIM detaylar icin ctrl+click yapip fonksiyon aciklamasini oku

        }
        else if (other.CompareTag("engel"))
        {
            // ENGELELRE CARPINCA YAPILACAKLAR....
            GameController.instance.SetScore(-collectibleDegeri); // ORNEK KULLANIM detaylar icin ctrl+click yapip fonksiyon aciklamasini oku
            if (GameController.instance.score < 0) // SKOR SIFIRIN ALTINA DUSTUYSE
            {
                // FAİL EVENTLERİ BURAYA YAZILACAK..
                GameController.instance.isContinue = false; // çarptığı anda oyuncunun yerinde durması ilerlememesi için
                UIController.instance.ActivateLooseScreen(); // Bu fonksiyon direk çağrılada bilir veya herhangi bir effect veya animasyon bitiminde de çağrılabilir..
                                                             // oyuncu fail durumunda bu fonksiyon çağrılacak.. 
            }


        }
        else if (other.CompareTag("finish"))
        {
            // finishe collider eklenecek levellerde...
            // FINISH NOKTASINA GELINCE YAPILACAKLAR... Totalscore artırma, x işlemleri, efektler v.s. v.s.
            GameController.instance.isContinue = false;
            GameController.instance.ScoreCarp(7);  // Bu fonksiyon normalde x ler hesaplandıktan sonra çağrılacak. Parametre olarak x i alıyor. 
            // x değerine göre oyuncunun total scoreunu hesaplıyor.. x li olmayan oyunlarda parametre olarak 1 gönderilecek.
            UIController.instance.ActivateWinScreen(); // finish noktasına gelebildiyse her türlü win screen aktif edilecek.. ama burada değil..
                                                       // normal de bu kodu x ler hesaplandıktan sonra çağıracağız. Ve bu kod çağrıldığında da kazanılan puanlar animasyonlu şekilde artacak..


        }
        else if (other.gameObject.tag == "FinishCizgisi")
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            transform.localPosition = new Vector3(0, 1, 0);
            GameController.instance._hareketiDurdur = true;

            YanindakileriYokEt();
            //_scriptFireRate = _fireRate / 2;

        }
        else if (other.gameObject.tag == "FinishBasliyor")
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);



        }
        else
        {

        }

    }

    public void PlayerDursun()
    {
        Invoke("KaybettiEkrani", 1f);
    }

    public void PlayerDansEt()
    {
        _animator.SetBool("carry", false);
        _animator.SetBool("carryidle", false);
        _animator.SetBool("walk", false);
        _animator.SetBool("dance", true);
    }

    public void PlayerIdleDon()
    {
        _animator.SetBool("carry", false);
        _animator.SetBool("carryidle", false);
        _animator.SetBool("walk", false);
        _animator.SetBool("dance", false);
    }

    private void YanindakileriYokEt()
    {

    }

    public void PlayerKazandi()
    {
        //Invoke("KazandiEkrani", 3f);
        KazandiEkrani();
    }

    private void KazandiEkrani()
    {
        GameController.instance.ScoreCarp(1);
        UIController.instance.ActivateWinScreen();
    }

    private void KaybettiEkrani()
    {
        UIController.instance.ActivateLooseScreen();
    }


    public void PlayerCoinGuncelle(int deger)
    {

        GameController.instance.SetScore(collectibleDegeri * deger);
        //Debug.Log("INCOME --- " + _incomeDegeri);
        //Debug.Log("SCORE ---- " + GameController.instance.score);
    }

    public void CarpimDegeriGuncelle(float deger)
    {
        _oyunSonuCarpimDegeri = deger;
    }

    public void KupYerlestirPlayer()
    {

        if (_elindeParcaVarMi == false)
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            _elindeParcaVarMi = true;
            _yerlestirilmisKupSecildi.gameObject.transform.parent = _tasimaNoktasi.transform;
            gameObject.transform.DOKill();
            _yerlestirilmisKupSecildi.gameObject.transform.DOLocalJump(Vector3.zero, 2, 1, 0.5f);
            _yerlestirilmisKupSecildi.gameObject.transform.DOLocalRotate(Vector3.zero, 0.5f);
            _yerlestirilmisKupSecildi.gameObject.GetComponent<KupScript>()._tasiniyorMu = true;
            _tasinanKup = _yerlestirilmisKupSecildi;
            _yerlestirilmisKupSecildi.GetComponent<KupScript>()._kupYerlesti = false;
            _yerlestirilmisKupSecildi.GetComponent<KupScript>()._yerlesecegiNokta.GetComponent<YerlesmeNoktasiScript>()._yerlesmeNoktasiDoluMu = false;
            _yerlestirilmisKupSecildi.GetComponent<KupScript>()._yerlesecegiNokta.GetComponent<YerlesmeNoktasiScript>().YerBosalt();
            _yerlestirilmisKupSecildi.GetComponent<KupScript>()._yerlesecegiNokta.GetComponent<YerlesmeNoktasiScript>()._yerlesenKup = null;
            _yerlestirilmisKupSecildi.GetComponent<KupScript>()._yerlesecegiNokta = null;
            _yerlestirilmisKupSecildi.GetComponent<KupScript>()._tekrarYerlesebilir = false;
            _yerBelirlendi = false;
            _yerlestirilmisKupSecildi.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = true;

            _timerSlider.value = 0;

            _timer = 0;
        }
        else
        {

        }

    }



    /// <summary>
    /// Bu fonksiyon her level baslarken cagrilir. 
    /// </summary>
    public void StartingEvents()
    {

        transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.parent.transform.position = Vector3.zero;
        GameController.instance.isContinue = false;
        GameController.instance._hareketiDurdur = false;
        GameController.instance.score = 0;
        transform.position = new Vector3(0, transform.position.y, 0);

        PlayerIdleDon();

        //_incomeDegeri = (1 + PlayerPrefs.GetInt("IncomeLevelDegeri"));
        _oyunSonuCarpimDegeri = 1;

        _elindeParcaVarMi = false;
        _yerBelirlendi = false;

        _timerSlider.value = 0;

    }

}
