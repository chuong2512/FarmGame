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

        FirstLoad(1);
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
        while (!asyncOperation.isDone)
        {
            showText.text = (asyncOperation.progress * 100).ToString() + "%";
            showLoad.fillAmount = /*(float)i / 100*/asyncOperation.progress;
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
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(idScene);
        asyncOperation.allowSceneActivation = false;
        for (int i = 0; i < 100; i++)
        {
            temp++;
            showText.text = temp + "%";
            yield return new WaitForSeconds(time / 100);
        }

        asyncOperation.allowSceneActivation = true;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}