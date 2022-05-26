     using UnityEngine;
     using System.Collections;
     
     public class RandomAudio  : MonoBehaviour {
     
         public AudioSource randomSound;
     
         public AudioClip[] audioSources;
     
         // Use this for initialization
         void Start () {
     
             CallAudio ();
         }
     
     
         void CallAudio()
         {
             Invoke ("RandomSoundness", 12);
         }
     
         void RandomSoundness()
         {
             randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];
             randomSound.Play ();
             CallAudio ();
         }
     }

