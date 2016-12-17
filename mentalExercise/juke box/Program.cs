using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Design a musical juke box (Qn: 15.2)
namespace juke_box
{
    class Song
    {
        string SongName {get;set;}
        string AlbumName {get;set;}
        string ArtistName {get;set;}
        public Song()
        {
        }
    }

    class Playlist
    {
        string PlayListName {get;set;}
        List<Song> _ListofSong;
        public Playlist()
        {
            _ListofSong = new List<Song>();
        }
    }

    //Need classes for indexes for faster search? //What is the best data structure? dictionary?
    //able to create playlist
    //able to play playlist
    //able to select individual songs to play
    //able to select all songs of a given artist or album
    //able to go back, start at begin, go forward or select a particular song in the playlist while playing
    //Stop or Pause playing

    
    class MusicJukeBox
    {
        List<Playlist> _ListofPlayList;
        enum JukeBoxFunctions { PLAY, PAUSE, STOP, GO_BACK, GO_BEGIN, GO_FORWARD, GO_END }
        enum PlayListFunctions { INPUT_ListOfSongs, INPUT_Album, INPUT_Artist };

        public MusicJukeBox()
        {
            _ListofPlayList = new List<Playlist>();
        }
        public void CreatePlayList(string pPlayListFunction, List<Song> pListofSong = null, string pAlbumOrArtistName = null)
        {

        }
        public void RunJukeBox(string pJukeBoxFunction, Playlist pPlayList=null, Song pSong=null)
        {

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
