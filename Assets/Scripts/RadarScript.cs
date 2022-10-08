using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (GameController.instance.isContinue)
            {
                PlayerController.instance.PlayerAtesEtsin();
                PlayerController._onumdeDusmanVar = true;
                Debug.Log("Dusman Var");
            }
            else
            {

            }

        }
        else if (other.gameObject.tag == "Kapi")
        {

        }
        else if (other.gameObject.tag == "Bullet")
        {

        }
        else if (other.gameObject.tag == "Player")
        {

        }
        else
        {
            if (GameController.instance.isContinue)
            {
                PlayerController.instance.PlayerKossun();
                PlayerController._onumdeDusmanVar = false;
                Debug.Log("Dusman Yok");
            }
            else
            {

            }

        }
    }
}
