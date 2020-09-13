using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameUI.Settings
{
    public class Settings : MonoBehaviour
    {
        //Reference to classes 
        [FormerlySerializedAs("_gameVolume")] [SerializeField] private GameVolume gameVolume = new GameVolume();
        [FormerlySerializedAs("_gameScreenSize")] [SerializeField] private GameScreenSize gameScreenSize = new GameScreenSize();
        private readonly GameResolution _gameResolution = new GameResolution();


        void Start()
        {
            gameScreenSize.DisplayCurrentScreenSize();
        }

        void Update()
        {
            gameVolume.ScrollVolume();
        }

        public void Volume(float volume) //Change the volume through the Slider Volume
        {
            gameVolume.SetvolumeScroller(volume);
        }

        public void MuteGameSound() //On/Off Mute
        {
            gameVolume.MuteGame();
        }

        public void GameScreenSize(int screen) //Select screen size through DropDown
        {
            gameScreenSize.SetScreenSize(screen);
        }

        public void FullScreenSize(bool fullScreen) //On/Off FullScreen
        {
            gameScreenSize.SetFullScreen(fullScreen);
        }

        public void Resolution(int resolution)// Select game resolution through DropDown
        {
            _gameResolution.SetResolution(resolution);
        }
    }

    class GameResolution
    {
        public void SetResolution(int resolutionIndex)
        {
            QualitySettings.SetQualityLevel(resolutionIndex);
        }
    }


    [Serializable]
    class GameVolume
    {
        [FormerlySerializedAs("_audioMixer")]
        [Header("References for AudioMixer and Mute Toggle")]
        [SerializeField] [Tooltip("The audio mixer is the music folder")] private AudioMixer audioMixer;
        [FormerlySerializedAs("_muteGameToggle")] [SerializeField] private Toggle muteGameToggle;

        //Calculation Variables
        private float _sliderVolumeValueBeforeMute;
        private bool _onlyRunOnce = true;


        private void Update()
        {
            ScrollVolume();
        }

        public void ScrollVolume()
        {
            if (muteGameToggle.isOn == false && !_onlyRunOnce) //assign the scroll volume before the mute toggle was activated 
            {
                audioMixer.SetFloat("MasterVolume", _sliderVolumeValueBeforeMute);
                _onlyRunOnce = true;
            }
        }

        public void MuteGame()
        {
            int muted = -80;
            audioMixer.SetFloat("MasterVolume", muted);
            _onlyRunOnce = false;
        }


        public void SetvolumeScroller(float setVolume)
        {
            audioMixer.SetFloat("MasterVolume", setVolume);
            _sliderVolumeValueBeforeMute = setVolume;
        }
    }


    [Serializable]
    class GameScreenSize
    {
        [FormerlySerializedAs("_screenSizeDropDown")]
        [Header("Reference to DropDown List")]
        [SerializeField] private TMP_Dropdown screenSizeDropDown;

        //Resolutions avaible will be hold here
        private Resolution[] _screenSizesAvailable;

        private void Start()
        {
            DisplayCurrentScreenSize();
        }

        public void DisplayCurrentScreenSize()
        {
            _screenSizesAvailable = Screen.resolutions; // Reference to all Screen Resolution supported

            screenSizeDropDown.ClearOptions(); // Delete all options at the Drop Down

            List<string> screenSizes = new List<string>();

            int currentScreenSize = 0; // Reference to assign the current Screen Size to the Drop Down
            for (int i = 0; i < _screenSizesAvailable.Length; i++)
            {
                string option = _screenSizesAvailable[i].width + " x " + _screenSizesAvailable[i].height; //Giving a string format to the array values 
                screenSizes.Add(option);

                if (_screenSizesAvailable[i].width == Screen.currentResolution.width &&
                    _screenSizesAvailable[i].height == Screen.currentResolution.height)
                {
                    currentScreenSize = i;
                }
            }

            screenSizeDropDown.AddOptions(screenSizes); //Add the List values to the Drop Down   
            screenSizeDropDown.value = currentScreenSize; //Set the current Screen Size
            screenSizeDropDown.RefreshShownValue(); // Display the current Screen Size
        }


        public void SetScreenSize(int resolutionIndex)
        {
            Resolution resolution = _screenSizesAvailable[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void SetFullScreen(bool fullScreen)
        {
            Screen.fullScreen = fullScreen;
        }
    }
}