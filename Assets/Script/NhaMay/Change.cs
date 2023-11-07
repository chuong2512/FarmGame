using UnityEngine;
using UnityEngine.EventSystems;

namespace NongTrai
{
    public class Change : MonoBehaviour
    {
        [SerializeField] int dem;
        [SerializeField] bool checkClick;
        [SerializeField] Sprite DotYellow;
        [SerializeField] Sprite DotBlack;
        [SerializeField] SpriteRenderer sprRenderer;
        [SerializeField] SpriteRenderer[] Dot;
        [SerializeField] GameObject[] objChange;

        // Use this for initialization
        void Start()
        {
            sprRenderer = this.GetComponent<SpriteRenderer>();
        }

        void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            checkClick = true;
            sprRenderer.color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }

        void OnMouseUp()
        {
            if (checkClick != true) return;
            checkClick = false;
            sprRenderer.color = Color.white;
            if (dem < objChange.Length - 1)
            {
                Dot[dem].sprite = DotBlack;
                objChange[dem].SetActive(false);
                dem += 1;
                Dot[dem].sprite = DotYellow;
                objChange[dem].SetActive(true);
            }
            else if (dem == objChange.Length - 1)
            {
                Dot[dem].sprite = DotBlack;
                objChange[dem].SetActive(false);
                dem = 0;
                Dot[dem].sprite = DotYellow;
                objChange[dem].SetActive(true);
            }
        }
    }
}