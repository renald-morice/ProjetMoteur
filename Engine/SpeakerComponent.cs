using System;
using System.Collections.Generic;

namespace Engine {

    public class SpeakerComponent : GameComponent {

        private FMOD.Channel _channel;

        public SpeakerComponent() {

        }


        public void Play(FMOD.Sound sound, bool loop = false, int loopCount = -1) {

            Stop();

            if (loop) {
                sound.setMode(FMOD.MODE.LOOP_NORMAL);
                sound.setLoopCount(loopCount-1);
            }
            else sound.setMode(FMOD.MODE.LOOP_OFF);

            FMOD.RESULT result;

            result = AudioMaster.Instance.GetFmodSystem().playSound(sound, null, false, out _channel);
            if (result != FMOD.RESULT.OK) Console.WriteLine("[SpeakerComponent Play] FMOD playSound failed : " + result);

        }

        public void Stop() {
            if (IsPlaying()) _channel.stop();
        }

        //Use this to pause or resume sound
        public void Pause() {
            bool isPaused;
            _channel.getPaused(out isPaused);
            _channel.setPaused(!isPaused);
        }

        private bool IsPlaying() {

            bool isPlaying = false;
            if (_channel != null) _channel.isPlaying(out isPlaying);
            return isPlaying;
        }

    }

}
