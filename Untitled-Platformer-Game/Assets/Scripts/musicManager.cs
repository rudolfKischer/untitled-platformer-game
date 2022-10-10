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

public AudioMixer mixer;
public int currentSongNumber = 0;
private List<AudioSource> audioSourceList = new List<AudioSource>();


void Start(){  
audioSourceList.Add(transform.Find("TrackOne").GetComponent<AudioSource>());
audioSourceList.Add(transform.Find("TrackTwo").GetComponent<AudioSource>());
audioSourceList.Add(transform.Find("TrackThree").GetComponent<AudioSource>());
audioSourceList.Add(transform.Find("TrackFour").GetComponent<AudioSource>());
audioSourceList.Add(transform.Find("TrackFive").GetComponent<AudioSource>());
changeSong(1);
}

public void changeSong(int songNumber){
    if(currentSongNumber != songNumber){
        
        
        currentSongNumber = songNumber;
        Song currentSong = songList.songs[currentSongNumber];
        int i = 0;
        foreach(AudioClip audioTrack in currentSong.audioTrack){
            
            audioSourceList[i].clip = audioTrack;
            audioSourceList[i].Play();
            i = i + 1;
        }
    }

}

//public void changeTrack(int trackNumber){


//}


   public void trackOn(int track){
       audioSourceList[track].volume = 100;
    
}
   public void trackOff(int track){
       audioSourceList[track].volume = 0;
    
}
}
