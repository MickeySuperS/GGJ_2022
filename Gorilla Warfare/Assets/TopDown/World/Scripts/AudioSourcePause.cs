using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{


    public class AudioSourcePause : MonoBehaviour
    {
        AudioSource souce;

        private void Start()
        {
            souce = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            if (!souce) souce = GetComponent<AudioSource>();
            souce.Play();
        }

        // Update is called once per frame
        void Update()
        {
            if (GamePause.gameIsPaused)
            {
                if (souce.isPlaying)
                    souce.Pause();
            }
            // else if (!souce.isPlaying)
            // {
            //     // souce.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
            //     souce.Play();
            // }
        }
    }
}