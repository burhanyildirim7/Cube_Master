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

    [SerializeField] private GameObject _karakter;

    public List<Material> _materials = new List<Material>();

    [SerializeField] private Animator _karakterAnimator;

    [SerializeField] private GameObject _silah;

    [SerializeField] private GameObject _bulletSpawnPoint;

    [SerializeField] private GameObject _bulletObject;

    [SerializeField] private TextMeshProUGUI _levelText;

    public static bool _onumdeDusmanVar;

    [SerializeField] private float _fireRate;

    private float _timer;

    public int _renkKodu;

    public int _playerLevel;

    [SerializeField] private GameObject _askerParent;

    public int _incomeDegeri;

    private float _scriptFireRate;

    public float _oyunSonuCarpimDegeri;

    private bool _atesEt;

    private bool _siniriGecti;

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
        if (GameController.instance.isContinue && _atesEt)
        {
            _timer += Time.deltaTime;


            if (_timer > _scriptFireRate)
            {
                _timer = 0;
                GameObject bullet = Instantiate(_bulletObject, _bulletSpawnPoint.transform.position, Quaternion.identity);
                bullet.GetComponent<BulletScript>()._bulletRenkKodu = _renkKodu;
                bullet.GetComponent<Renderer>().material = _materials[_renkKodu];

                if (_askerParent.transform.childCount > 0 && _siniriGecti == false)
                {
                    for (int i = 0; i < _askerParent.transform.childCount; i++)
                    {
                        if (_askerParent.transform.GetChild(i).gameObject.GetComponent<EnemyScript>()._yerineGecti == true)
                        {
                            _askerParent.transform.GetChild(i).gameObject.GetComponent<EnemyScript>().AtesEt();
                        }
                        else
                        {

                        }
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

        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Kapi")
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            if (other.GetComponent<KapiRengi>()._renk1)
            {
                _karakter.GetComponent<Renderer>().material = _materials[1];
                _renkKodu = 1;
                for (int i = 0; i < _askerParent.transform.childCount; i++)
                {
                    _askerParent.transform.GetChild(i).gameObject.GetComponent<EnemyScript>().EnemyRenkGuncelle(_renkKodu);
                }
            }
            else if (other.GetComponent<KapiRengi>()._renk2)
            {
                _karakter.GetComponent<Renderer>().material = _materials[2];
                _renkKodu = 2;
                for (int i = 0; i < _askerParent.transform.childCount; i++)
                {
                    _askerParent.transform.GetChild(i).gameObject.GetComponent<EnemyScript>().EnemyRenkGuncelle(_renkKodu);
                }
            }
            else if (other.GetComponent<KapiRengi>()._renk3)
            {
                _karakter.GetComponent<Renderer>().material = _materials[3];
                _renkKodu = 3;
                for (int i = 0; i < _askerParent.transform.childCount; i++)
                {
                    _askerParent.transform.GetChild(i).gameObject.GetComponent<EnemyScript>().EnemyRenkGuncelle(_renkKodu);
                }
            }
            else if (other.GetComponent<KapiRengi>()._renk4)
            {
                _karakter.GetComponent<Renderer>().material = _materials[4];
                _renkKodu = 4;
                for (int i = 0; i < _askerParent.transform.childCount; i++)
                {
                    _askerParent.transform.GetChild(i).gameObject.GetComponent<EnemyScript>().EnemyRenkGuncelle(_renkKodu);
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
            _atesEt = false;
            _siniriGecti = true;
            YanindakileriYokEt();
            //_scriptFireRate = _fireRate / 2;

        }
        else if (other.gameObject.tag == "FinishBasliyor")
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            _scriptFireRate = _fireRate / 2;
            _atesEt = true;


        }
        else
        {

        }

    }

    public void PlayerKossun()
    {
        //_karakterAnimator.SetBool("Attack", false);
        //_karakterAnimator.SetBool("Run", true);
        PlayerAtesEtsin();
    }

    public void PlayerDursun()
    {
        _silah.SetActive(false);
        _karakterAnimator.SetBool("Run", false);
        _karakterAnimator.SetBool("Attack", false);

        for (int i = 0; i < _askerParent.transform.childCount; i++)
        {
            _askerParent.transform.GetChild(i).gameObject.GetComponent<EnemyScript>().EnemyOyunBitti();
            //Destroy(_askerParent.transform.GetChild(i).gameObject);
        }

        Invoke("KaybettiEkrani", 1f);
    }

    private void YanindakileriYokEt()
    {
        if (_askerParent.transform.childCount > 0)
        {
            for (int i = 0; i < _askerParent.transform.childCount; i++)
            {
                //_askerParent.transform.GetChild(i).gameObject.transform.DOLocalMove(Vector3.zero, 1f).OnComplete(() => PlayerLevelGuncelle(1));
                _askerParent.transform.GetChild(i).gameObject.transform.DOLocalMove(Vector3.zero, 1f);
                //_playerLevel++;
                //_levelText.text = "Lv " + _playerLevel.ToString();
            }
        }
        else
        {

        }
    }

    public void PlayerKazandi()
    {
        _silah.SetActive(false);
        _karakterAnimator.SetBool("Run", false);
        _karakterAnimator.SetBool("Attack", false);
        _karakterAnimator.SetBool("Victory", true);

        Invoke("KazandiEkrani", 3f);
    }

    private void KazandiEkrani()
    {
        GameController.instance.ScoreCarp(_oyunSonuCarpimDegeri);
        UIController.instance.ActivateWinScreen();
    }

    private void KaybettiEkrani()
    {
        UIController.instance.ActivateLooseScreen();
    }

    public void PlayerAtesEtsin()
    {
        _silah.SetActive(true);
        _karakterAnimator.SetBool("Run", false);
        _karakterAnimator.SetBool("Attack", true);
    }

    public void PlayerLevelGuncelle(int deger)
    {
        _playerLevel = _playerLevel + deger;
        _levelText.text = "Lv " + _playerLevel.ToString();
        _levelText.gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.2f).OnComplete(() => _levelText.gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));

    }

    public void PlayerCoinGuncelle()
    {

        GameController.instance.SetScore(_incomeDegeri);
        //Debug.Log("INCOME --- " + _incomeDegeri);
        //Debug.Log("SCORE ---- " + GameController.instance.score);
    }

    public void CarpimDegeriGuncelle(float deger)
    {
        _oyunSonuCarpimDegeri = deger;
    }

    public void PlayerLevelButtonGuncelle()
    {
        _playerLevel = PlayerPrefs.GetInt("PowerLevelDegeri");
        _levelText.text = "Lv " + _playerLevel.ToString();
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

        _incomeDegeri = (1 + PlayerPrefs.GetInt("IncomeLevelDegeri"));
        _oyunSonuCarpimDegeri = 1;

        _onumdeDusmanVar = false;
        _karakterAnimator.SetBool("Run", false);
        _karakterAnimator.SetBool("Attack", false);
        _karakterAnimator.SetBool("Victory", false);

        _scriptFireRate = _fireRate;

        _atesEt = true;
        _siniriGecti = false;

        _renkKodu = 0;
        _karakter.GetComponent<Renderer>().material = _materials[_renkKodu];

        if (PlayerPrefs.GetInt("PowerLevelDegeri") < 1)
        {
            _playerLevel = 1;
        }
        else
        {
            _playerLevel = PlayerPrefs.GetInt("PowerLevelDegeri");
        }

        for (int i = 0; i < _askerParent.transform.childCount; i++)
        {
            Destroy(_askerParent.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < GameObject.FindGameObjectWithTag("AskerKonumlari").GetComponent<AskerKonumlariList>()._askerKonumlariList.Count; i++)
        {
            GameObject.FindGameObjectWithTag("AskerKonumlari").GetComponent<AskerKonumlariList>()._askerKonumlariList[i].GetComponent<AskerToplamaKonumlar>()._doluMu = false;
        }

        _levelText.text = "Lv " + _playerLevel.ToString();

        _silah.SetActive(false);

    }

}
