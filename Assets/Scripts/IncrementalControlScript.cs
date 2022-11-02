using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class IncrementalControlScript : MonoBehaviour
{

    public static IncrementalControlScript instance;



    [SerializeField] private Button _powerButton, _incomeButton;
    [SerializeField] TextMeshProUGUI _powerIncLevelText, _incomeIncLevelText, _powerIncBedelText, _incomeIncBedelText;
    [SerializeField] List<int> _incrementalBedel = new List<int>();

    public ParticleSystem _upgradeParticle;

    private void Awake()
    {
        if (instance == null) instance = this;
        //else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("ButtonlarIcinIlkSefer") == 0)
        {
            PlayerPrefs.SetInt("PowerLevelDegeri", 1);
            PlayerPrefs.SetInt("StaminaLevelDegeri", 1);
            PlayerPrefs.SetInt("IncomeLevelDegeri", 1);

            //_powerIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("PowerLevelDegeri").ToString();
            //_staminaIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("StaminaLevelDegeri").ToString();
            //_incomeIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("IncomeLevelDegeri").ToString();

            _powerIncLevelText.text = "LEVEL 1";

            _incomeIncLevelText.text = "LEVEL 1";

            _powerIncBedelText.text = "$50";

            _incomeIncBedelText.text = "$50";

            PlayerPrefs.SetInt("ButtonlarIcinIlkSefer", 1);
            PlayerPrefs.SetInt("KarakterDegisimSayaci", 1);
            _powerButton.interactable = true;

            _incomeButton.interactable = true;

        }
        else
        {
            if (PlayerPrefs.GetInt("PowerLevelDegeri") == 75)
            {
                _powerIncLevelText.text = "MAX";
                _powerIncBedelText.text = "MAX";
                _powerButton.interactable = false;

            }
            else
            {
                _powerIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("PowerLevelDegeri").ToString();
                _powerIncBedelText.text = "$" + _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")];
                _powerButton.interactable = true;
            }



            if (PlayerPrefs.GetInt("IncomeLevelDegeri") == 75)
            {
                _incomeIncLevelText.text = "MAX";
                _incomeIncBedelText.text = "MAX";
                _incomeButton.interactable = false;
            }
            else
            {
                _incomeIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("IncomeLevelDegeri").ToString();
                _incomeIncBedelText.text = "$" + _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")];
                _incomeButton.interactable = true;
            }
        }


        BaslangicButonAyarlari();

        ButonKontrol();

        //PlayerPrefs.SetInt("totalScore", 99999);

        Application.targetFrameRate = 60;

    }

    private void BaslangicButonAyarlari()
    {
        if (PlayerPrefs.GetInt("totalScore") < _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")])
        {
            _powerButton.interactable = false;
        }
        else
        {
            _powerButton.interactable = true;
        }



        if (PlayerPrefs.GetInt("totalScore") < _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")])
        {
            _incomeButton.interactable = false;
        }
        else
        {
            _incomeButton.interactable = true;
        }

    }


    public void PowerButonu()
    {
        if (PlayerPrefs.GetInt("PowerLevelDegeri") < 75 && PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")])
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            _upgradeParticle.Play();

            PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") - _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")]);
            PlayerPrefs.SetInt("PowerLevelDegeri", PlayerPrefs.GetInt("PowerLevelDegeri") + 1);
            PlayerPrefs.SetInt("PowerCostDegeri", PlayerPrefs.GetInt("PowerCostDegeri") + 1);
            PlayerPrefs.SetInt("KarakterDegisimSayaci", PlayerPrefs.GetInt("KarakterDegisimSayaci") + 1);
            _powerIncBedelText.text = "$" + _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")];
            UIController.instance.SetTapToStartScoreText();


            BaslangicButonAyarlari();

            if (PlayerPrefs.GetInt("PowerLevelDegeri") == 75)
            {
                _powerIncLevelText.text = "MAX";
                _powerIncBedelText.text = "MAX";
                _powerButton.interactable = false;
            }
            else
            {
                _powerIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("PowerLevelDegeri").ToString();
                //_powerButonPasifPaneli.SetActive(false);

            }

            //PlayerController.instance.PlayerLevelButtonGuncelle();

        }
        else
        {
            _powerButton.interactable = false;
        }

        if (PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")])
        {
            _powerButton.interactable = true;
        }
        else
        {
            _powerButton.interactable = false;
        }
    }


    public void IncomeButonu()
    {
        if (PlayerPrefs.GetInt("IncomeLevelDegeri") < 75 && PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")])
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            _upgradeParticle.Play();

            PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") - _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")]);
            PlayerPrefs.SetInt("IncomeLevelDegeri", PlayerPrefs.GetInt("IncomeLevelDegeri") + 1);
            PlayerPrefs.SetInt("IncomeCostDegeri", PlayerPrefs.GetInt("IncomeCostDegeri") + 1);
            _incomeIncBedelText.text = "$" + _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")];
            UIController.instance.SetTapToStartScoreText();


            BaslangicButonAyarlari();

            if (PlayerPrefs.GetInt("IncomeLevelDegeri") == 75)
            {
                _incomeIncLevelText.text = "MAX";
                _incomeIncBedelText.text = "MAX";
                _incomeButton.interactable = false;
            }
            else
            {
                _incomeIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("IncomeLevelDegeri").ToString();
                //_incomeButonPasifPaneli.SetActive(false);
            }

            PlayerController.instance._incomeDegeri = (1 + PlayerPrefs.GetInt("IncomeLevelDegeri"));
        }
        else
        {
            _incomeButton.interactable = false;
        }

        if (PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")])
        {
            _incomeButton.interactable = true;
        }
        else
        {
            _incomeButton.interactable = false;
        }
    }




    public void ButonKontrol()
    {
        if (PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")])
        {
            _powerButton.interactable = true;
        }
        else
        {
            _powerButton.interactable = false;
        }


        if (PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")])
        {
            _incomeButton.interactable = true;
        }
        else
        {
            _incomeButton.interactable = false;
        }
    }



}
