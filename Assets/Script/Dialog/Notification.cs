using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NongTrai
{
    public class Notification : Singleton<Notification>
    {
        private bool _checkOn, _checkBetween, _checkBelow, _checkTower, _checkDepot;
        [SerializeField] Text txtOn, txtBetween, txtBelow, txtDialogTower, txtDialogDepot, txtDialog;


        public void dialogTower()
        {
            /*string textShow;
            
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                textShow = "Sức Chứa Nông Sản " + ManagerMarket.instance.quantityItemTower + "/" +
                           ManagerMarket.instance.QuantityTotalItemTower;
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                textShow = "Penyimpanan Silo " + ManagerMarket.instance.quantityItemTower + "/" +
                           ManagerMarket.instance.QuantityTotalItemTower;
            else
                textShow = "Capacity Farm " + ManagerMarket.instance.quantityItemTower + "/" +
                           ManagerMarket.instance.QuantityTotalItemTower;

            ToastManager.Instance.Show(textShow);*/
        }

        public void dialogComingSoon()
        {
            dialog("This feature is coming soon!");
        }

        public void dialog(string text)
        {
            ToastManager.Instance.Show(text);
            
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
            string textShow;

            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                textShow = "Sức Chứa Vật Phẩm " + ManagerMarket.instance.quantityItemDepot + "/" +
                           ManagerMarket.instance.quantityTotalItemDepot;
            else if (Application.systemLanguage == SystemLanguage.Vietnamese)
                textShow = "Penyimpanan Lumbung " + ManagerMarket.instance.quantityItemDepot + "/" +
                           ManagerMarket.instance.quantityTotalItemDepot;
            else
                textShow = "Capacity Depot Item " + ManagerMarket.instance.quantityItemDepot + "/" +
                           ManagerMarket.instance.quantityTotalItemDepot;

            ToastManager.Instance.Show(textShow);
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
            ToastManager.Instance.Show(textShow);
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
            ToastManager.Instance.Show(textShow);
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
            ToastManager.Instance.Show(textShow);
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