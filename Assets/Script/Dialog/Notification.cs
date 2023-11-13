using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NongTrai
{
    public class Notification : Singleton<Notification>
    {
        private bool _checkOn, _checkBetween, _checkBelow, _checkTower, _checkDepot;
        [SerializeField] Text txtOn, txtBetween, txtBelow, txtDialogTower, txtDialogDepot, txtDialog;
        IEnumerator _ieBelow, _ieBetween, _ieOn, _ieTower, _ieDepot;


        public void dialogTower()
        {
            /*if (Application.systemLanguage == SystemLanguage.Vietnamese)
                txtDialogTower.text = "Sức Chứa Nông Sản " + ManagerMarket.instance.quantityItemTower + "/" +
                                      ManagerMarket.instance.QuantityTotalItemTower;
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                txtDialogTower.text = "Penyimpanan Silo " + ManagerMarket.instance.quantityItemTower + "/" +
                                      ManagerMarket.instance.QuantityTotalItemTower;
            else
                txtDialogTower.text = "Capacity Farm " + ManagerMarket.instance.quantityItemTower + "/" +
                                      ManagerMarket.instance.QuantityTotalItemTower;

            if (_checkTower == false)
            {
                _ieTower = showDialogTower();
                StartCoroutine(_ieTower);
            }
            else if (_checkTower == true)
            {
                _checkTower = false;
                txtDialogTower.gameObject.SetActive(false);
                StopCoroutine(_ieTower);
                _ieTower = showDialogTower();
                StartCoroutine(_ieTower);
            }*/
        }

        public void dialogComingSoon()
        {
            dialog("This feature is coming soon!");
        }

        public void dialog(string text)
        {
            /*txtDialog.text = text;

            txtDialog.gameObject.SetActive(false);
            StartCoroutine(showDialog());

            IEnumerator showDialog()
            {
                txtDialog.gameObject.SetActive(true);
                yield return new WaitForSeconds(2f);
                txtDialog.gameObject.SetActive(false);
            }*/
        }

        /*IEnumerator showDialogTower()
        {
            _checkTower = true;
            txtDialogTower.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            txtDialogTower.gameObject.SetActive(false);
            _checkTower = false;
        }*/

        public void dialogDepot()
        {
            /*if (Application.systemLanguage == SystemLanguage.Vietnamese)
                txtDialogDepot.text = "Sức Chứa Vật Phẩm " + ManagerMarket.instance.quantityItemDepot + "/" +
                                      ManagerMarket.instance.quantityTotalItemDepot;
            else if (Application.systemLanguage == SystemLanguage.Vietnamese)
                txtDialogDepot.text = "Penyimpanan Lumbung " + ManagerMarket.instance.quantityItemDepot + "/" +
                                      ManagerMarket.instance.quantityTotalItemDepot;
            else
                txtDialogDepot.text = "Capacity Depot Item " + ManagerMarket.instance.quantityItemDepot + "/" +
                                      ManagerMarket.instance.quantityTotalItemDepot;

            if (_checkDepot == false)
            {
                _ieDepot = showDialogDepot();
                StartCoroutine(_ieDepot);
            }
            else if (_checkDepot == true)
            {
                _checkDepot = false;
                txtDialogDepot.gameObject.SetActive(false);
                StopCoroutine(_ieDepot);
                _ieDepot = showDialogDepot();
                StartCoroutine(_ieDepot);
            }*/
        }

        /*IEnumerator showDialogDepot()
        {
            _checkDepot = true;
            txtDialogDepot.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            txtDialogDepot.gameObject.SetActive(false);
            _checkDepot = false;
        }*/

        public void dialogOn(string textShow)
        {
            /*txtOn.text = textShow;
            if (_checkOn == false)
            {
                _ieOn = showDialogOn();
                StartCoroutine(_ieOn);
            }
            else if (_checkOn == true)
            {
                _checkOn = false;
                txtOn.gameObject.SetActive(false);
                StopCoroutine(_ieOn);
                _ieOn = showDialogOn();
                StartCoroutine(_ieOn);
            }*/
        }

        /*IEnumerator showDialogOn()
        {
            _checkOn = true;
            txtOn.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            txtOn.gameObject.SetActive(false);
            _checkOn = false;
        }*/

        public void dialogBetween(string textShow)
        {
            /*txtBetween.text = textShow;
            if (_checkBetween == false)
            {
                _ieBetween = showDialogBetween();
                StartCoroutine(_ieBetween);
            }
            else if (_checkBetween == true)
            {
                _checkBetween = false;
                txtBetween.gameObject.SetActive(false);
                StopCoroutine(_ieBetween);
                _ieBetween = showDialogBetween();
                StartCoroutine(_ieBetween);
            }*/
        }

        /*IEnumerator showDialogBetween()
        {
            _checkBetween = true;
            txtBetween.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            txtBetween.gameObject.SetActive(false);
            _checkBetween = false;
        }*/

        public void dialogBelow(string textShow)
        {
            /*txtBelow.text = textShow;
            if (_checkBelow == false)
            {
                _ieBelow = ShowDialogBelow();
                StartCoroutine(_ieBelow);
            }
            else if (_checkBelow == true)
            {
                _checkBelow = false;
                txtBelow.gameObject.SetActive(false);
                StopCoroutine(_ieBelow);
                _ieBelow = ShowDialogBelow();
                StartCoroutine(_ieBelow);
            }*/
        }

        /*IEnumerator ShowDialogBelow()
        {
            _checkBelow = true;
            txtBelow.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            txtBelow.gameObject.SetActive(false);
            _checkBelow = false;
        }*/
    }
}