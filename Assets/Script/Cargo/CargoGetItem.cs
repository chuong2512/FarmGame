using UnityEngine.Serialization;

namespace NongTrai
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;

    public class CargoGetItem : MonoBehaviour
    {
        readonly float _timeDestroy;
        [FormerlySerializedAs("ItemImage")] public Image itemImage;

        [FormerlySerializedAs("QuantityItemText")]
        public Text quantityItemText;

        public CargoGetItem(float timeDestroy)
        {
            this._timeDestroy = timeDestroy;
        }

        void Start()
        {
            StartCoroutine(DestroyMySeft());
        }

        private IEnumerator DestroyMySeft()
        {
            yield return new WaitForSeconds(_timeDestroy);
            Destroy(gameObject);
        }
    }
}