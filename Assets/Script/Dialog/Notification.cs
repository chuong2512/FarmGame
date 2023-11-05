using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Notification : MonoBehaviour
{
    public static Notification instance;
    private bool checkOn, checkBetween, checkBelow, checkTower, checkDepot;
    [SerializeField] Text txtOn, txtBetween, txtBelow, txtDialogTower, txtDialogDepot, txtDialog;
    IEnumerator IEBelow, IEBetween, IEOn, IETower, IEDepot;

    //--------------------------------------------------------------

    void Awake()
    {
        instance = this;
    }

    public void dialogTower()
    {
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
            txtDialogTower.text = "Sức Chứa Nông Sản " + ManagerMarket.instance.QuantityItemTower + "/" +
                                  ManagerMarket.instance.QuantityTotalItemTower;
        else if (Application.systemLanguage == SystemLanguage.Indonesian)
            txtDialogTower.text = "Penyimpanan Silo " + ManagerMarket.instance.QuantityItemTower + "/" +
                                  ManagerMarket.instance.QuantityTotalItemTower;
        else
            txtDialogTower.text = "Capacity Farm " + ManagerMarket.instance.QuantityItemTower + "/" +
                                  ManagerMarket.instance.QuantityTotalItemTower;

        if (checkTower == false)
        {
            IETower = showDialogTower();
            StartCoroutine(IETower);
        }
        else if (checkTower == true)
        {
            checkTower = false;
            txtDialogTower.gameObject.SetActive(false);
            StopCoroutine(IETower);
            IETower = showDialogTower();
            StartCoroutine(IETower);
        }
    }

    public void dialogComingSoon()
    {
        dialog("This feature is coming soon!");
    }

    public void dialog(string text)
    {
        txtDialog.text = text;

        txtDialog.gameObject.SetActive(false);
        StartCoroutine(showDialog());

        IEnumerator showDialog()
        {
            txtDialog.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            txtDialog.gameObject.SetActive(false);
        }
    }

    IEnumerator showDialogTower()
    {
        checkTower = true;
        txtDialogTower.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        txtDialogTower.gameObject.SetActive(false);
        checkTower = false;
    }

    public void dialogDepot()
    {
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
            txtDialogDepot.text = "Sức Chứa Vật Phẩm " + ManagerMarket.instance.QuantityItemDepot + "/" +
                                  ManagerMarket.instance.QuantityTotalItemDepot;
        else if (Application.systemLanguage == SystemLanguage.Vietnamese)
            txtDialogDepot.text = "Penyimpanan Lumbung " + ManagerMarket.instance.QuantityItemDepot + "/" +
                                  ManagerMarket.instance.QuantityTotalItemDepot;
        else
            txtDialogDepot.text = "Capacity Depot Item " + ManagerMarket.instance.QuantityItemDepot + "/" +
                                  ManagerMarket.instance.QuantityTotalItemDepot;

        if (checkDepot == false)
        {
            IEDepot = showDialogDepot();
            StartCoroutine(IEDepot);
        }
        else if (checkDepot == true)
        {
            checkDepot = false;
            txtDialogDepot.gameObject.SetActive(false);
            StopCoroutine(IEDepot);
            IEDepot = showDialogDepot();
            StartCoroutine(IEDepot);
        }
    }

    IEnumerator showDialogDepot()
    {
        checkDepot = true;
        txtDialogDepot.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        txtDialogDepot.gameObject.SetActive(false);
        checkDepot = false;
    }

    public void dialogOn(string textShow)
    {
        txtOn.text = textShow;
        if (checkOn == false)
        {
            IEOn = showDialogOn();
            StartCoroutine(IEOn);
        }
        else if (checkOn == true)
        {
            checkOn = false;
            txtOn.gameObject.SetActive(false);
            StopCoroutine(IEOn);
            IEOn = showDialogOn();
            StartCoroutine(IEOn);
        }
    }

    IEnumerator showDialogOn()
    {
        checkOn = true;
        txtOn.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        txtOn.gameObject.SetActive(false);
        checkOn = false;
    }

    public void dialogBetween(string textShow)
    {
        txtBetween.text = textShow;
        if (checkBetween == false)
        {
            IEBetween = showDialogBetween();
            StartCoroutine(IEBetween);
        }
        else if (checkBetween == true)
        {
            checkBetween = false;
            txtBetween.gameObject.SetActive(false);
            StopCoroutine(IEBetween);
            IEBetween = showDialogBetween();
            StartCoroutine(IEBetween);
        }
    }

    IEnumerator showDialogBetween()
    {
        checkBetween = true;
        txtBetween.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        txtBetween.gameObject.SetActive(false);
        checkBetween = false;
    }

    public void dialogBelow(string textShow)
    {
        txtBelow.text = textShow;
        if (checkBelow == false)
        {
            IEBelow = showDialogBelow();
            StartCoroutine(IEBelow);
        }
        else if (checkBelow == true)
        {
            checkBelow = false;
            txtBelow.gameObject.SetActive(false);
            StopCoroutine(IEBelow);
            IEBelow = showDialogBelow();
            StartCoroutine(IEBelow);
        }
    }

    IEnumerator showDialogBelow()
    {
        checkBelow = true;
        txtBelow.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        txtBelow.gameObject.SetActive(false);
        checkBelow = false;
    }
}