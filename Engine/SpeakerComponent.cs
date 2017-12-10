using System;
using System.Collections.Generic;

namespace Engine {

    public class SpeakerComponent : GameComponent, ILogicComponent {

        private FMOD.Channel _channel;
         AudioMaster _audioMaster;

        public SpeakerComponent() {
            _audioMaster = AudioMaster.Instance;
        }


        public void Play(string audioPath, bool loop = false, int loopCount = -1) {

            Stop();

            FMOD.Sound sound = _audioMaster.LoadSound(audioPath);

            if (loop) {
                sound.setMode(FMOD.MODE.LOOP_NORMAL);
                sound.setLoopCount(loopCount-1);
            }
            else sound.setMode(FMOD.MODE.LOOP_OFF);

            FMOD.RESULT result;

            result = _audioMaster.GetFmodSystem().playSound(sound, null, true, out _channel); // 3rd parameter : paused
            if (result != FMOD.RESULT.OK) Console.WriteLine("[SpeakerComponent Play] FMOD playSound failed : " + result);

            UpdateSpeakerAttributes();
            setVolume(100.0f);
            PauseResume();

        }

        public void setVolume(float vol) {

            if (_channel != null) {
                FMOD.RESULT result;
                result = _channel.setVolume(vol / 100.0f);
                if (result != FMOD.RESULT.OK) Console.WriteLine("[SpeakerComponent setVolume] FMOD setVolume failed : " + result);
            }

        }

        public void Stop() {
            if (IsPlaying()) _channel.stop();
        }

        //Use this to pause or resume sound
        public void PauseResume() {
            bool isPaused;
            _channel.getPaused(out isPaused);
            _channel.setPaused(!isPaused);
        }

        private bool IsPlaying() {

            bool isPlaying = false;
            if (_channel != null) _channel.isPlaying(out isPlaying);
            return isPlaying;
        }

        public void Start() {
        }

        public void Update() {

            if (IsPlaying()) UpdateSpeakerAttributes();
        }


        public void UpdateSpeakerAttributes() {

            FMOD.RESULT result;

            FMOD.VECTOR positionFmodVect;
            positionFmodVect.x = gameObject.transform.position.X;
            positionFmodVect.y = gameObject.transform.position.Y;
            positionFmodVect.z = gameObject.transform.position.Z;

            // TODO: add true velocity of speaker
            FMOD.VECTOR velocityFmodVect;
            velocityFmodVect.x = 0.0f;
            velocityFmodVect.y = 0.0f;
            velocityFmodVect.z = 0.0f;

            FMOD.VECTOR panFmodVect;
            panFmodVect.x = 0.0f;
            panFmodVect.y = 0.0f;
            panFmodVect.z = 0.0f;

            result = _channel.set3DAttributes(ref positionFmodVect, ref velocityFmodVect, ref panFmodVect);
            if (result != FMOD.RESULT.OK) Console.WriteLine("[SpeakerComponent Update] FMOD set3DAttributes failed : " + result);
        }

    }

}
