using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Loading : MonoBehaviour
{
    public static Loading instance;
    [SerializeField] Image showLoad;
    [SerializeField] Text showText;

    [SerializeField] float time;

    // Use this for initialization
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScene(1);
    }

    public void LoadScene(int id)
    {
        showLoad.fillAmount = 0;
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(SuperLoading(id));
    }

    IEnumerator SuperLoading(int idScene)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(idScene);
        asyncOperation.allowSceneActivation = false;
        
        for (int i = 0; i < 100; i++)
        {
            temp++;
            showText.text = temp + "%";
            showLoad.fillAmount = temp / 100f;
            yield return new WaitForSeconds(time / 100);
        }

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
                asyncOperation.allowSceneActivation = true;
            yield return null;
        }

        transform.GetChild(0).gameObject.SetActive(false);
    }

    void FirstLoad(int id)
    {
        showLoad.fillAmount = 0;
        showLoad.DOFillAmount(1, time).SetEase(Ease.Linear);
        StartCoroutine(DelayTime(id));
    }

    int temp = 0;

    IEnumerator DelayTime(int idScene)
    {
        for (int i = 0; i < 100; i++)
        {
            temp++;
            showText.text = temp + "%";
            yield return new WaitForSeconds(time / 100);
        }

        LoadScene(idScene);
    }
}