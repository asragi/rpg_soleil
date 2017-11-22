using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soleil
{
    /// <summary>
    /// 音の再生を管理するクラス
    /// </summary>
    static class Audio
    {
        /*
        public static AudioFileReader music;
        static WaveOut wave;
        static long[] loopByte;
        static Audio()
        {
            loopByte = new long[2];
        }

        public static void Update()
        {
            if (music.Position >= loopByte[1]) music.Position = loopByte[0];
            //Debug();
        }

        public static void PlayMusic(MusicID id)
        {
            if(wave!=null)StopMusic();
            loopByte[0] = Resources.MusicDataSet[(int)id].LoopInitByte;
            loopByte[1] = Resources.MusicDataSet[(int)id].LoopEndByte;
            music = new AudioFileReader(Resources.MusicPass(id) + ".mp3");
            wave = new WaveOut();
            wave.Init(music);
            wave.Play();
        }

        public static void StopMusic()
        {
            wave.Stop();
        }

        public static void PlaySound(SoundID id)
        {
            Resources.GetSound(id).Play();
        }

        public static void Debug()
        {
            System.Diagnostics.Debug.WriteLine(music.Position + ":" + music.Position/music.CurrentTime.TotalMilliseconds + ":" + music.TotalTime.TotalMilliseconds);
        }
        */
    }

    struct MusicData
    {
        public long LoopInitByte;
        public long LoopEndByte;
        public bool Repeat;
    }
}
