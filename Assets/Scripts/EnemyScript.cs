using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;



public class EnemyScript : MonoBehaviour
{
    public int _enemyRenkKodu;

    public int _enemyLevel;

    private int _yazacakLevel;

    [SerializeField] private Animator _karakterAnimator;

    [SerializeField] private TextMeshProUGUI _levelTexti;

    [SerializeField] private GameObject _enemyKarakter;

    [SerializeField] private GameObject _silah;

    [SerializeField] private GameObject _bullet;

    [SerializeField] private GameObject _bulletSpawnPoint;

    [SerializeField] private float _fireRate;

    [SerializeField] private List<GameObject> _splashList = new List<GameObject>();

    [SerializeField] private ParticleSystem _coinSplash;

    [SerializeField] private List<ParticleSystem> _bulletEfektList = new List<ParticleSystem>();

    private float _timer;

    public bool _yerineGecti;

    private bool _yasiyor;

    private int _konumNumber;

    // Start is called before the first frame update
    void Start()
    {
        _yazacakLevel = _enemyLevel;
        _levelTexti.text = "Lv " + _yazacakLevel.ToString();

        _enemyKarakter.GetComponent<Renderer>().material = PlayerController.instance._materials[_enemyRenkKodu];


        _silah.SetActive(false);
        _yasiyor = true;
    }

    private void FixedUpdate()
    {
        if (_yasiyor)
        {
            if (_enemyRenkKodu == PlayerController.instance._renkKodu)
            {
                _levelTexti.gameObject.SetActive(false);
            }
            else
            {
                _levelTexti.gameObject.SetActive(true);
            }
        }
        else
        {
            _levelTexti.gameObject.SetActive(false);
        }


        if (gameObject.transform.parent == GameObject.FindGameObjectWithTag("AskerParent"))
        {
            _timer += Time.deltaTime;


            if (_timer > _fireRate)
            {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (other.GetComponent<BulletScript>()._bulletRenkKodu != _enemyRenkKodu)
            {
                if (other.GetComponent<BulletScript>()._bulletRenkKodu == 1)
                {
                    _bulletEfektList[0].Play();
                }
                else if (other.GetComponent<BulletScript>()._bulletRenkKodu == 2)
                {
                    _bulletEfektList[1].Play();
                }
                else if (other.GetComponent<BulletScript>()._bulletRenkKodu == 3)
                {
                    _bulletEfektList[2].Play();
                }
                else if (other.GetComponent<BulletScript>()._bulletRenkKodu == 4)
                {
                    _bulletEfektList[3].Play();
                }
                else
                {

                }

                if (_yazacakLevel > PlayerController.instance._playerLevel)
                {
                    _yazacakLevel = _yazacakLevel - 1;
                    _levelTexti.text = "Lv " + _yazacakLevel.ToString();
                }
                else
                {
                    PlayerController.instance.PlayerLevelGuncelle(_enemyLevel);

                    PlayerController.instance.PlayerCoinGuncelle();

                    _enemyKarakter.SetActive(false);

                    gameObject.GetComponent<Collider>().enabled = false;

                    _yasiyor = false;

                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

                    _coinSplash.Play();

                    _silah.SetActive(false);

                    if (_enemyRenkKodu == 1)
                    {
                        _splashList[0].SetActive(true);
                    }
                    else if (_enemyRenkKodu == 2)
                    {
                        _splashList[1].SetActive(true);
                    }
                    else if (_enemyRenkKodu == 3)
                    {
                        _splashList[2].SetActive(true);
                    }
                    else if (_enemyRenkKodu == 4)
                    {
                        _splashList[3].SetActive(true);
                    }
                    else
                    {

                    }
                }

                Destroy(other.gameObject);
            }
            else
            {

            }
        }
        else if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<PlayerController>()._renkKodu == _enemyRenkKodu)
            {

                for (int i = 0; i < GameObject.FindGameObjectWithTag("AskerKonumlari").GetComponent<AskerKonumlariList>()._askerKonumlariList.Count; i++)
                {
                    if (gameObject.transform.parent != GameObject.FindGameObjectWithTag("AskerParent").gameObject.transform)
                    {
                        if (GameObject.FindGameObjectWithTag("AskerKonumlari").GetComponent<AskerKonumlariList>()._askerKonumlariList[i].GetComponent<AskerToplamaKonumlar>()._doluMu == false)
                        {
                            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

                            _konumNumber = i;
                            _karakterAnimator.SetBool("Run", true);
                            GameObject.FindGameObjectWithTag("AskerKonumlari").GetComponent<AskerKonumlariList>()._askerKonumlariList[_konumNumber].GetComponent<AskerToplamaKonumlar>()._doluMu = true;
                            gameObject.transform.parent = GameObject.FindGameObjectWithTag("AskerParent").transform;
                            gameObject.transform.DOLocalMove(GameObject.FindGameObjectWithTag("AskerKonumlari").GetComponent<AskerKonumlariList>()._askerKonumlariList[_konumNumber].gameObject.transform.localPosition, 0.5f).OnComplete(() => gameObject.transform.DORotate(Vector3.zero, 0.5f));
                            Invoke("YerineGecti", 1f);
                            break;
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
                if (PlayerController.instance._playerLevel > _yazacakLevel)
                {
                    PlayerController.instance.PlayerLevelGuncelle(-_yazacakLevel);



                    _enemyKarakter.SetActive(false);

                    gameObject.GetComponent<Collider>().enabled = false;

                    _yasiyor = false;

                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

                    _silah.SetActive(false);

                    //_coinSplash.Play();

                    if (PlayerController.instance._renkKodu == 1)
                    {
                        _bulletEfektList[0].Play();
                    }
                    else if (PlayerController.instance._renkKodu == 2)
                    {
                        _bulletEfektList[1].Play();
                    }
                    else if (PlayerController.instance._renkKodu == 3)
                    {
                        _bulletEfektList[2].Play();
                    }
                    else if (PlayerController.instance._renkKodu == 4)
                    {
                        _bulletEfektList[3].Play();
                    }
                    else
                    {

                    }

                    if (_enemyRenkKodu == 1)
                    {
                        _splashList[0].SetActive(true);
                    }
                    else if (_enemyRenkKodu == 2)
                    {
                        _splashList[1].SetActive(true);
                    }
                    else if (_enemyRenkKodu == 3)
                    {
                        _splashList[2].SetActive(true);
                    }
                    else if (_enemyRenkKodu == 4)
                    {
                        _splashList[3].SetActive(true);
                    }
                    else
                    {

                    }
                    //Destroy(gameObject);
                }
                else
                {
                    if (PlayerController.instance._playerLevel < 11)
                    {
                        GameController.instance.isContinue = false;
                        PlayerController.instance.PlayerDursun();
                    }
                    else
                    {
                        PlayerController.instance.PlayerLevelGuncelle(-10);


                        _enemyKarakter.SetActive(false);

                        gameObject.GetComponent<Collider>().enabled = false;

                        _yasiyor = false;

                        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

                        _silah.SetActive(false);

                        _karakterAnimator.SetBool("Run", false);
                        //_karakterAnimator.SetBool("Attack", false);

                        //_coinSplash.Play();

                        if (PlayerController.instance._renkKodu == 1)
                        {
                            _bulletEfektList[0].Play();
                        }
                        else if (PlayerController.instance._renkKodu == 2)
                        {
                            _bulletEfektList[1].Play();
                        }
                        else if (PlayerController.instance._renkKodu == 3)
                        {
                            _bulletEfektList[2].Play();
                        }
                        else if (PlayerController.instance._renkKodu == 4)
                        {
                            _bulletEfektList[3].Play();
                        }
                        else
                        {

                        }

                        if (_enemyRenkKodu == 1)
                        {
                            _splashList[0].SetActive(true);
                        }
                        else if (_enemyRenkKodu == 2)
                        {
                            _splashList[1].SetActive(true);
                        }
                        else if (_enemyRenkKodu == 3)
                        {
                            _splashList[2].SetActive(true);
                        }
                        else if (_enemyRenkKodu == 4)
                        {
                            _splashList[3].SetActive(true);
                        }
                        else
                        {

                        }
                    }



                    //Destroy(gameObject);
                }

            }
        }
        else if (other.gameObject.tag == "Enemy")
        {
            if (gameObject.transform.parent != GameObject.FindGameObjectWithTag("AskerParent").gameObject.transform)
            {
                if (other.GetComponent<EnemyScript>()._enemyRenkKodu == _enemyRenkKodu)
                {
                    //Debug.Log("ALMASI LAZIM -- " + gameObject);
                    for (int i = 0; i < GameObject.FindGameObjectWithTag("AskerKonumlari").GetComponent<AskerKonumlariList>()._askerKonumlariList.Count; i++)
                    {
                        if (GameObject.FindGameObjectWithTag("AskerKonumlari").GetComponent<AskerKonumlariList>()._askerKonumlariList[i].GetComponent<AskerToplamaKonumlar>()._doluMu == false)
                        {
                            //Debug.Log("ALMASI LAZIM - if --" + gameObject);
                            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

                            _konumNumber = i;
                            _karakterAnimator.SetBool("Run", true);
                            GameObject.FindGameObjectWithTag("AskerKonumlari").GetComponent<AskerKonumlariList>()._askerKonumlariList[_konumNumber].GetComponent<AskerToplamaKonumlar>()._doluMu = true;
                            gameObject.transform.parent = GameObject.FindGameObjectWithTag("AskerParent").transform;
                            gameObject.transform.DOLocalMove(GameObject.FindGameObjectWithTag("AskerKonumlari").GetComponent<AskerKonumlariList>()._askerKonumlariList[_konumNumber].gameObject.transform.localPosition, 0.5f).OnComplete(() => gameObject.transform.DORotate(Vector3.zero, 0.5f));
                            Invoke("YerineGecti", 1f);
                            break;
                        }
                        else
                        {

                        }



                    }


                }
                else
                {
                    if (PlayerController.instance._playerLevel > _yazacakLevel)
                    {
                        PlayerController.instance.PlayerLevelGuncelle(-_yazacakLevel);

                        PlayerController.instance.PlayerCoinGuncelle();

                        _enemyKarakter.SetActive(false);

                        gameObject.GetComponent<Collider>().enabled = false;

                        _yasiyor = false;

                        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

                        _coinSplash.Play();

                        _silah.SetActive(false);

                        if (other.GetComponent<EnemyScript>()._enemyRenkKodu == 1)
                        {
                            _bulletEfektList[0].Play();
                        }
                        else if (other.GetComponent<EnemyScript>()._enemyRenkKodu == 2)
                        {
                            _bulletEfektList[1].Play();
                        }
                        else if (other.GetComponent<EnemyScript>()._enemyRenkKodu == 3)
                        {
                            _bulletEfektList[2].Play();
                        }
                        else if (other.GetComponent<EnemyScript>()._enemyRenkKodu == 4)
                        {
                            _bulletEfektList[3].Play();
                        }
                        else
                        {

                        }

                        if (_enemyRenkKodu == 1)
                        {
                            _splashList[0].SetActive(true);
                        }
                        else if (_enemyRenkKodu == 2)
                        {
                            _splashList[1].SetActive(true);
                        }
                        else if (_enemyRenkKodu == 3)
                        {
                            _splashList[2].SetActive(true);
                        }
                        else if (_enemyRenkKodu == 4)
                        {
                            _splashList[3].SetActive(true);
                        }
                        else
                        {

                        }
                        //Destroy(gameObject);
                    }
                    else
                    {
                        //GameController.instance.isContinue = false;

                        //PlayerController.instance.PlayerDursun();
                        //PlayerController.instance.PlayerLevelGuncelle(-10);

                        PlayerController.instance.PlayerCoinGuncelle();

                        _enemyKarakter.SetActive(false);

                        gameObject.GetComponent<Collider>().enabled = false;

                        _yasiyor = false;

                        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

                        _silah.SetActive(false);

                        //_coinSplash.Play();

                        if (other.GetComponent<EnemyScript>()._enemyRenkKodu == 1)
                        {
                            _bulletEfektList[0].Play();
                        }
                        else if (other.GetComponent<EnemyScript>()._enemyRenkKodu == 2)
                        {
                            _bulletEfektList[1].Play();
                        }
                        else if (other.GetComponent<EnemyScript>()._enemyRenkKodu == 3)
                        {
                            _bulletEfektList[2].Play();
                        }
                        else if (other.GetComponent<EnemyScript>()._enemyRenkKodu == 4)
                        {
                            _bulletEfektList[3].Play();
                        }
                        else
                        {

                        }

                        if (_enemyRenkKodu == 1)
                        {
                            _splashList[0].SetActive(true);
                        }
                        else if (_enemyRenkKodu == 2)
                        {
                            _splashList[1].SetActive(true);
                        }
                        else if (_enemyRenkKodu == 3)
                        {
                            _splashList[2].SetActive(true);
                        }
                        else if (_enemyRenkKodu == 4)
                        {
                            _splashList[3].SetActive(true);
                        }
                        else
                        {

                        }
                        //Destroy(gameObject);
                    }

                }
            }
            else
            {
                if (other.GetComponent<EnemyScript>()._enemyRenkKodu == _enemyRenkKodu)
                {

                }
                else
                {
                    gameObject.transform.parent = null;

                    _enemyKarakter.SetActive(false);

                    gameObject.GetComponent<Collider>().enabled = false;

                    _yasiyor = false;

                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

                    _silah.SetActive(false);

                    //_coinSplash.Play();

                    if (other.GetComponent<EnemyScript>()._enemyRenkKodu == 1)
                    {
                        _bulletEfektList[0].Play();
                    }
                    else if (other.GetComponent<EnemyScript>()._enemyRenkKodu == 2)
                    {
                        _bulletEfektList[1].Play();
                    }
                    else if (other.GetComponent<EnemyScript>()._enemyRenkKodu == 3)
                    {
                        _bulletEfektList[2].Play();
                    }
                    else if (other.GetComponent<EnemyScript>()._enemyRenkKodu == 4)
                    {
                        _bulletEfektList[3].Play();
                    }
                    else
                    {

                    }

                    if (_enemyRenkKodu == 1)
                    {
                        _splashList[0].SetActive(true);
                    }
                    else if (_enemyRenkKodu == 2)
                    {
                        _splashList[1].SetActive(true);
                    }
                    else if (_enemyRenkKodu == 3)
                    {
                        _splashList[2].SetActive(true);
                    }
                    else if (_enemyRenkKodu == 4)
                    {
                        _splashList[3].SetActive(true);
                    }
                    else
                    {

                    }
                    GameObject.FindGameObjectWithTag("AskerKonumlari").GetComponent<AskerKonumlariList>()._askerKonumlariList[_konumNumber].GetComponent<AskerToplamaKonumlar>()._doluMu = false;
                    Invoke("YokEt", 1f);
                }

            }

        }
        else if (other.gameObject.tag == "FinishCizgisi")
        {
            //Destroy(gameObject);
            EnemyKossun();
            Invoke("YokEt2", 1.1f);
        }
        else
        {

        }
    }

    private void YokEt()
    {
        Destroy(gameObject);
    }

    private void YokEt2()
    {
        Destroy(gameObject);
        PlayerController.instance.PlayerLevelGuncelle(1);
    }

    public void AtesEt()
    {
        GameObject bullet = Instantiate(_bullet, _bulletSpawnPoint.transform.position, Quaternion.identity);
        bullet.GetComponent<BulletScript>()._bulletRenkKodu = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>()._renkKodu;
        bullet.GetComponent<Renderer>().material = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>()._materials[GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>()._renkKodu];
    }

    private void YerineGecti()
    {
        _karakterAnimator.SetBool("Run", false);
        _karakterAnimator.SetBool("Attack", true);
        _yerineGecti = true;
        _silah.SetActive(true);
    }

    private void EnemyKossun()
    {
        _silah.SetActive(false);
        _karakterAnimator.SetBool("Attack", false);
        _karakterAnimator.SetBool("Run", true);
    }

    public void EnemyOyunBitti()
    {
        _silah.SetActive(false);

        _karakterAnimator.SetBool("Run", false);
        _karakterAnimator.SetBool("Attack", false);
    }

    public void EnemyRenkGuncelle(int deger)
    {
        _enemyKarakter.GetComponent<Renderer>().material = PlayerController.instance._materials[deger];
        _enemyRenkKodu = deger;
    }
}
