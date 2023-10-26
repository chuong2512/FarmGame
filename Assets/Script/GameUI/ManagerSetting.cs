using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManagerSetting : MonoBehaviour
{
    public static ManagerSetting instance;

    [SerializeField] Text NameSettingText;
    [SerializeField] Text MusicText;
    [SerializeField] Text SoundText;
    [SerializeField] Text ExitButtonText;
    [SerializeField] Slider Music;
    [SerializeField] Slider Sound;
    [SerializeField] Text TitleExitGameText;
    [SerializeField] Text ExitText;
    [SerializeField] GameObject Setting;
    [SerializeField] GameObject ExitGame;
    [SerializeField] GameObject ThankYou;
    //-------------------------------------------------
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    void Start()
    {
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
        {
            NameSettingText.text = "Cài Đặt";
            TitleExitGameText.text = "Xác nhận";
            MusicText.text = "Nhạc nền";
            SoundText.text = "Âm thanh";
            ExitText.text = "Bạn muốn thoát trò chơi đúng không?";
            ExitButtonText.text = "Thoát Trò Chơi";
        }
        else if (Application.systemLanguage == SystemLanguage.Indonesian)
        {
            NameSettingText.text = "Setelan";
            TitleExitGameText.text = "Konfirmasi";
            MusicText.text = "Musik";
            SoundText.text = "Suara";
            ExitText.text = "Apakah Anda ingin keluar dari permainan?";
            ExitButtonText.text = "Keluar permainan";
        }
        else
        {
            NameSettingText.text = "Setting";
            TitleExitGameText.text = "Confirm";
            MusicText.text = "Sound";
            SoundText.text = "Music";
            ExitText.text = "Do you want to exit game?";
            ExitButtonText.text = "Exit Game";
        }

        if (PlayerPrefs.HasKey("ValueMusic") == false)
            PlayerPrefs.SetFloat("ValueMusic", 1);
        else
        {
            Music.value = PlayerPrefs.GetFloat("ValueMusic");
            ManagerAudio.instance.ChangeValueMusic(Music.value);
        }
        if (PlayerPrefs.HasKey("ValueSound") == false)
            PlayerPrefs.SetFloat("ValueSound", 1);
        else
        {
            Sound.value = PlayerPrefs.GetFloat("ValueSound");
            ManagerAudio.instance.ChangeValueSound(Sound.value);
        }
    }

    public void OpenSetting()
    {
        MainCamera.instance.DisableAll();
        MainCamera.instance.lockCam();
        Setting.SetActive(true);
    }

    public void CloseSetting()
    {
        MainCamera.instance.unLockCam();
        Setting.SetActive(false);
    }

    public void ButtonSetting()
    {
        ManagerAudio.instance.PlayAudio(Audio.ClickOpen);
        OpenSetting();
    }

    public void ButtonSave()
    {
        ManagerAudio.instance.PlayAudio(Audio.ClickExit);
        CloseSetting();
    }

    public void MusicChangeValue()
    {
        ManagerAudio.instance.ChangeValueMusic(Music.value);
        PlayerPrefs.SetFloat("ValueMusic", Music.value);
    }

    public void SoundChangeValue()
    {
        ManagerAudio.instance.ChangeValueSound(Sound.value);
        PlayerPrefs.SetFloat("ValueSound", Sound.value);
    }

    public void ButtonExitGame()
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        Setting.SetActive(false);
        ExitGame.SetActive(true);
    }

    public void ButtonYesExitGame()
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        ExitGame.SetActive(false);
        StartCoroutine(WaitExit());
    }

    IEnumerator WaitExit()
    {
        ThankYou.SetActive(true);
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }

    public void ButtonNoExitGame()
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        ExitGame.SetActive(false);
        Setting.SetActive(true);
    }
}
