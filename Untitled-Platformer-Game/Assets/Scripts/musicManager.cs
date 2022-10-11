using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Audio;


public class musicManager : MonoBehaviour {
//CreatesListOfAudioTracks
[System.Serializable]
public class Song
{
    public List<AudioClip> audioTrack;
}
//CreatesListOfSongs(ListofMusicTracks)
[System.Serializable]
public class SongList
{
    public List<Song> songs;
}
public SongList songList = new SongList();
public bool muteMusic;
public AudioMixer musicMixer;

//temp until triggers for music
public int currentSongNumber = 0;
//list of audioTracks
private List<AudioSource> audioSourceList = new List<AudioSource>();


void Start(){  
    populateAudioSourceList();

    changeSong(1);
}


void Update(){
    if(muteMusic){
      
musicMixer.SetFloat("musicVolume", -80f);
    }else
    {
      musicMixer.SetFloat("musicVolume", 0f);  
    }
}

//populates list with audiosources belonging to children
private void populateAudioSourceList(){
audioSourceList.Add(transform.Find("TrackOne").GetComponent<AudioSource>());
audioSourceList.Add(transform.Find("TrackTwo").GetComponent<AudioSource>());
audioSourceList.Add(transform.Find("TrackThree").GetComponent<AudioSource>());
audioSourceList.Add(transform.Find("TrackFour").GetComponent<AudioSource>());
audioSourceList.Add(transform.Find("TrackFive").GetComponent<AudioSource>());

}
//changes song and resigns audio sources
public void changeSong(int songNumber){
    if(currentSongNumber != songNumber){
        
        
        currentSongNumber = songNumber;
        Song currentSong = songList.songs[currentSongNumber];
        int i = 0;
        foreach(AudioClip audioTrack in currentSong.audioTrack){
            trackOff(i);
            audioSourceList[i].clip = audioTrack;
            audioSourceList[i].Play();
            i = i + 1;
        }
        
        trackOn(1);
        trackOn(2);
        trackOn(3);
    }

}

public void transitionTracks(int track1, int track2, float seconds){
    while(audioSourceList[track1].volume > 0){
    
    audioSourceList[track1].volume -= 1*Time.deltaTime;
    audioSourceList[track2].volume += 1*Time.deltaTime;
    }
    
}
//sets specified audiosources volume to 100
   public void trackOn(int track){
       audioSourceList[track].volume = 100;
    
}
//sets specified audiosources volume to 0
   public void trackOff(int track){
       audioSourceList[track].volume = 0;
    
}
}
