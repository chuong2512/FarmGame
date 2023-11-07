using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NongTrai
{
    public enum Audio
    {
        Click = 0,
        ClickOpen,
        ClickExit,
        MinusGold,
        ReciveGold,
        TapPoint,
        Uplevel,
        CarRun,
        Cheep,
        NewItem,
        Chiken,
        Cow,
        Pig,
        Sheep,
        Goat,
        Horse,
        Duck
    }


    public class ManagerAudio : PersistentSingleton<ManagerAudio>
    {
        private int SourceMusic;
        private bool live;
        [SerializeField] AudioSource Music;
        [SerializeField] AudioSource[] Sound;

        [SerializeField] AudioClip[] MusicBackground;

        private void Start()
        {
            StartCoroutine(Cheep());
        }

        private IEnumerator Cheep()
        {
            while (!live)
            {
                int randomTime = Random.Range(30, 60);
                yield return new WaitForSeconds(randomTime);
                PlayAudio(Audio.Cheep);
            }
        }

        public void PlayAudio(Audio audio)
        {
            Sound[(int) audio].Play();
        }

        public void ChangeValueMusic(float value)
        {
            Music.volume = value;
        }

        public void ChangeValueSound(float value)
        {
            for (int i = 0; i < Sound.Length; i++)
            {
                Sound[i].volume = value;
            }
        }
    }
}