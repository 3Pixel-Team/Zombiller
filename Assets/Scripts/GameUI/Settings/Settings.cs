using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GameUI.Settings
{
    public class Settings : MonoBehaviour
    {
        //Reference to classes 
        [SerializeField] private GameVolume gameVolume = new GameVolume();
        [SerializeField] private GameDifficulty gameDifficulty = new GameDifficulty();
        [SerializeField] private Gore gameGore = new Gore();
        
        void Update() => gameVolume.ScrollVolume();
        
        //Change the volume through the Slider Volume
        public void Volume(float volume) => gameVolume.SetvolumeScroller(volume);
        
        //On/Off Mute
        public void MuteGameSound() => gameVolume.MuteGame();
        
        //Select difficulty size through DropDown
        public void GameDifficulty(int difficulty) => gameDifficulty.SetGameDifficulty(difficulty);
        
        //Toggles gore mode
        public void Gore(bool toggle) => gameGore.GoreToggle(toggle);
    }
    
    
    [Serializable]
    class GameVolume
    {
        [Header("References for AudioMixer and Mute Toggle")]
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Toggle muteGameToggle;

        //Calculation Variables
        private float _sliderVolumeValueBeforeMute;
        private bool _onlyRunOnce = true;

        private void Update() => ScrollVolume();
        
        internal void ScrollVolume()
        {
            if (muteGameToggle.isOn == false && !_onlyRunOnce) //assign the scroll volume before the mute serToggle was activated 
            {
                audioMixer.SetFloat("MasterVolume", _sliderVolumeValueBeforeMute);
                _onlyRunOnce = true;
            }
        }
        
        internal void MuteGame()
        {
            int muted = -80;
            audioMixer.SetFloat("MasterVolume", muted);
            _onlyRunOnce = false;
        }

        internal void SetvolumeScroller(float setVolume)
        {
            audioMixer.SetFloat("MasterVolume", setVolume);
            _sliderVolumeValueBeforeMute = setVolume;
        }
    }
    
    [Serializable]
    class GameDifficulty
    {
        [Header("Reference to DropDown List")]
        [SerializeField] private TMP_Dropdown difficultyDropDown;
        
        public void SetGameDifficulty(int difficultyIndex)
        {
           
        }
    }
    
    [Serializable]
    class Gore
    {
        public void GoreToggle(bool setToggle) => setToggle = !setToggle;
    }
}