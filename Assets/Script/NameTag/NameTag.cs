using Script.GameUI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace NongTrai
{
    public class NameTag : BaseBuild
    {
        [SerializeField] private Text _nameText;
        [SerializeField] private Canvas _canvas;

        private void OnEnable()
        {
            SetName();
        }

        protected override void Start()
        {
            base.Start();
            _canvas.sortingOrder = (int) (Order + 1);
        }

        

        public void SetName()
        {
            _nameText.text = PlayerPrefs.GetString("NameTag", "NameTag");
        }
        
        
        [Button]
        protected override void OpenPopup()
        {
            NameTagManager.Instance.OpenPopup();
        }
    }
}