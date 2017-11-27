using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Engine {

    public class AudioMaster {

        private FMOD.System _fmodSystem;
        private Dictionary<string, FMOD.Sound> _sounds;

        [DllImport("kernel32.dll")]
        public static extern IntPtr _loadLibrary(string dllToLoad);

        private static AudioMaster _instance;

        public static AudioMaster Instance {
            get {
                if (_instance == null) {
                    _instance = new AudioMaster();
                }
                return _instance;
            }
        }

        /*---------------------------------------------*/
        /* Private AudioMaster constructor (Singleton) */
        /*---------------------------------------------*/
        private AudioMaster() {

            //In order to be able to use FMOD in both x86 and x64 architecture

            string pathToFMODLib = System.IO.Path.GetFullPath("Engine\\FMOD");

            if (Environment.Is64BitProcess) pathToFMODLib += "\\64\\fmod.dll";
            else pathToFMODLib += "\\32\\fmod.dll";

            _loadLibrary(pathToFMODLib);


            _sounds = new Dictionary<string, FMOD.Sound>();

            //Initialize FMOD
            //---------------

            FMOD.RESULT result;

            result = FMOD.Factory.System_Create(out _fmodSystem);
            if(result != FMOD.RESULT.OK) Console.WriteLine("[AudioMaster constructor] FMOD System_Create failed : " + result);

            result = _fmodSystem.setDSPBufferSize(1024, 10);
            if (result != FMOD.RESULT.OK) Console.WriteLine("[AudioMaster constructor] FMOD setDSPBufferSize failed : " + result);

            result = _fmodSystem.init(32, FMOD.INITFLAGS.NORMAL, (IntPtr)0);
            if (result != FMOD.RESULT.OK) Console.WriteLine("[AudioMaster constructor] FMOD init failed : " + result);

        }

        /*------------------------*/
        /* Unload all FMOD things */
        /*------------------------*/
        public void Unload() {

            _fmodSystem.release();
            foreach (KeyValuePair<string, FMOD.Sound> sound in _sounds) sound.Value.release();

        }

        /*-----------------------------------------------*/
        /* Load Sound from audio file path and return it */
        /*-----------------------------------------------*/
        public FMOD.Sound LoadSound(string soundPath) {

            string soundName = Path.GetFileName(soundPath);

            //If sound is already loaded, returns it
            if (_sounds.ContainsKey(soundName)) return _sounds[soundName];
            //Otherwise
            else {

                FMOD.Sound sound;

                FMOD.RESULT result;
                result = _fmodSystem.createSound(soundPath, FMOD.MODE._3D, out sound);

                if (result != FMOD.RESULT.OK) Console.WriteLine("[AudioMaster LoadSong] FMOD createStream failed : " + result);
                else {
                    _sounds.Add(soundName, sound);
                    return sound;
                }

            }

            return null;

        }


        public void Play(FMOD.Sound sound, FMOD.Channel channel) {

            FMOD.RESULT result;

            result = _fmodSystem.playSound(sound, null, false, out channel);
            if (result != FMOD.RESULT.OK) Console.WriteLine("[AudioMaster Play] FMOD playSound failed : " + result);

        }

    }

}
