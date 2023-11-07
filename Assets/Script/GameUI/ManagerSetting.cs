using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NongTrai
{
    public class ManagerSetting : Singleton<ManagerSetting>
    {
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
                ManagerAudio.Instance.ChangeValueMusic(Music.value);
            }

            if (PlayerPrefs.HasKey("ValueSound") == false)
                PlayerPrefs.SetFloat("ValueSound", 1);
            else
            {
                Sound.value = PlayerPrefs.GetFloat("ValueSound");
                ManagerAudio.Instance.ChangeValueSound(Sound.value);
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
            ManagerAudio.Instance.PlayAudio(Audio.ClickOpen);
            OpenSetting();
        }

        public void ButtonSave()
        {
            ManagerAudio.Instance.PlayAudio(Audio.ClickExit);
            CloseSetting();
        }

        public void MusicChangeValue()
        {
            ManagerAudio.Instance.ChangeValueMusic(Music.value);
            PlayerPrefs.SetFloat("ValueMusic", Music.value);
        }

        public void SoundChangeValue()
        {
            ManagerAudio.Instance.ChangeValueSound(Sound.value);
            PlayerPrefs.SetFloat("ValueSound", Sound.value);
        }

        public void ButtonExitGame()
        {
            ManagerAudio.Instance.PlayAudio(Audio.Click);
            Setting.SetActive(false);
            ExitGame.SetActive(true);
        }

        public void ButtonYesExitGame()
        {
            ManagerAudio.Instance.PlayAudio(Audio.Click);
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
            ManagerAudio.Instance.PlayAudio(Audio.Click);
            ExitGame.SetActive(false);
            Setting.SetActive(true);
        }
    }
}