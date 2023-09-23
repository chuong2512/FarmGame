using UnityEngine;
using System.Collections;

public class ManagerGame : MonoBehaviour
{
    public static ManagerGame instance;
    private bool live = true;
    public float DistaneX;
    public float DistaneY;
    private const int TimeOneGem = 300;
    [SerializeField] Transform MinX;
    [SerializeField] Transform MaxX;
    [SerializeField] Transform MinY;
    [SerializeField] Transform MaxY;
    [SerializeField] GameObject EffectInGame;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(CreateEffectInGame());
    }

    IEnumerator CreateEffectInGame()
    {
        while (live)
        {
            int randomTime = Random.Range(5, 10);
            yield return new WaitForSeconds(randomTime);
            float randomX = Random.Range(MinX.position.x, MaxX.position.x);
            float randomY = Random.Range(MinY.position.y, MaxY.position.y);
            Instantiate(EffectInGame, new Vector3(randomX, randomY, 0), Quaternion.identity);
        }
    }

    public int RealTime()
    {
        int secsInAMin = 60;
        int secsInAnHour = 60 * 60;
        int secsInADay = 24 * 3600;
        int secsMonth = 30 * 24 * 3600;
        int secsYear = 12 * 30 * 24 * 3600;
        System.DateTime timeNow = System.DateTime.Now;
        int sec = timeNow.Second;
        int min = timeNow.Minute * secsInAMin;
        int hour = timeNow.Hour * secsInAnHour;
        int day = timeNow.Day * secsInADay;
        int month = timeNow.Month * secsMonth;
        int year= timeNow.Year * secsYear;
        int realTime = sec + min + hour + day + month + year;
        return realTime;
    }

    public string TimeText(int time)
    {
        string timetext = "";
        int day = time / 86400;
        int hour = (time - day * 86400) / 3600;
        int min = (time - day * 86400 - hour * 3600) / 60;
        int sec = time - day * 86400 - hour * 3600 - min * 60;
        if (time >= 86400)
        {
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                timetext = "" + day + " ngày " + hour + " giờ " + min + " phút " + sec + " giây";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                timetext = "" + day + " hari " + hour + " jam " + min + " mnt " + sec + " detik";
            else timetext = "" + day + " day " + hour + " hour " + min + " min " + sec + " sec";
        }
        else if (time >= 3600 && time < 86400)
        {
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                timetext = hour + " giờ " + min + " phút " + sec + " giây";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                timetext = hour + " jam " + min + " mnt " + sec + " detik";
            else timetext = hour + " hour " + min + " min " + sec + " sec";
        }
        else if (time >= 60 && time < 3600)
        {
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                timetext = min + " phút " + sec + " giây";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                timetext = min + " mnt " + sec + " detik";
            else timetext = min + " min " + sec + " sec";
        }
        else if (time >= 0 && time < 60)
        {
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                timetext = sec + " giây";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                timetext = sec + " detik";
            else timetext = sec + " sec";
        }
        return timetext;
    }

    public bool RandomItem()
    {
        bool DropItem = false;
        int constantOne = Random.Range(0, 8);
        int constantTwo = Random.Range(0, 8);
        if (constantOne == constantTwo) DropItem = true;
        return DropItem;
    }

    public int CalcalutorGemCrop(int time)
    {
        int ValueGem = time / 600 + 1;
        return ValueGem;
    }

    public int CalcalutorGem(int time)
    {
        int ValueGem = time / 600 + 1;
        return ValueGem;
    }
}
