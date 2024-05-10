#if FIREBASE_DEFINED
using Firebase.Analytics;
#endif

namespace Infrastructure.Services.SoundsService
{
    public class SoundService : ISoundService
    {
        /*public SoundService(IProgressService progressService, 
            IAssetsAddressablesProvider assetsAddressablesProvider, )
        {
            _progressService = progressService;
            _assetsAddressablesProvider = assetsAddressablesProvider;
        }

        public bool IsAudioSourcesSetUp { get; private set; }

        public bool IsSoundsPlay { get; private set; }

        private readonly IProgressService _progressService;
        private readonly IAssetsAddressablesProvider _assetsAddressablesProvider;

        private AudioSourceContainer _audioSourceContainer;


        public void SetUpAudioSources(AudioSourceContainer audioSourceContainer)
        {
            _audioSourceContainer = audioSourceContainer;

            IsSoundsPlay = _progressService.PlayerProgress.PlayerSettings.IsSoundEnabled;
            
            _progressService.PlayerProgress.PlayerSettings.OnMusicStateChanged += OnMusicStateChanged;
            _progressService.PlayerProgress.PlayerSettings.OnSoundStateChanged += OnSoundStateChanged;
            
            IsAudioSourcesSetUp = true;
        }
        
        public void PlayAudioClip(AudioClip audioClip, float volume = 1f, float minPitch = 1f, float maxPitch = 1f)
        {
            if (_progressService.PlayerProgress.PlayerSettings.IsSoundEnabled == false)
            {
                Debug.Log($"PlayAudioClip: {audioClip.name}. Sounds is disabled");
                return;
            }
            
            _audioSourceContainer.SoundsAudioSource.pitch = Random.Range(minPitch, maxPitch);
            _audioSourceContainer.SoundsAudioSource.PlayOneShot(audioClip, volume);
            
            Debug.Log($"PlayAudioClip: {audioClip.name}");
        }
        
        public void StopAudioClip()
        {
            _audioSourceContainer.SoundsAudioSource.Stop();
            //_audioSourceContainer.SoundsAudioSource.Play();
        }

        public async void PlayAudioClip(SoundId id, float volume = 1f, float minPitch = 1f, float maxPitch = 1f)
        {
            if (_progressService.PlayerProgress.PlayerSettings.IsSoundEnabled == false)
            {
                Debug.Log($"PlayAudioClip: {id}. Sounds is disabled");
                return;
            }
            
            var soundID = id.ToString();

            var sound = await _assetsAddressablesProvider.GetAsset<AudioClip>(soundID);
            
            _audioSourceContainer.SoundsAudioSource.pitch = Random.Range(minPitch, maxPitch);
            _audioSourceContainer.SoundsAudioSource.PlayOneShot(sound, volume);
            
            Debug.Log($"PlayAudioClip: {id}");
        }
        
        public async void PlayMusic(MusicId id, float volume = 1f)
        {
            if (_progressService.PlayerProgress.PlayerSettings.IsMusicEnabled == false)
            {
                Debug.Log($"PlayMusic: {id}. Music is disabled");

                _audioSourceContainer.MusicAudioSource.volume = 0;
            }
            else
            {
                Debug.Log($"PlayMusic: {id}. Music is enabled");
                
                _audioSourceContainer.MusicAudioSource.volume = 1;
            }
            
            var musicID = id.ToString();
            var music = await _assetsAddressablesProvider.GetAsset<AudioClip>(musicID);
            
            _audioSourceContainer.MusicAudioSource.clip = music;
            _audioSourceContainer.MusicAudioSource.Play();
        }
        

        public void PauseMusic()
        {
            _audioSourceContainer.MusicAudioSource.Pause();

            Debug.Log($"PauseMusic");
        }

        public void StopMusic()
        {
            _audioSourceContainer.MusicAudioSource.Stop();
            _audioSourceContainer.MusicAudioSource.clip = null;

            Debug.Log($"StopMusic");
        }
        
        public void StopSounds()
        {
            _audioSourceContainer.SoundsAudioSource.Stop();
            _audioSourceContainer.SoundsAudioSource.clip = null;
            IsSoundsPlay = false;
            
            Debug.Log($"StopSounds");
        }
        
        private void OnMusicStateChanged()
        {
            if (_progressService.PlayerProgress.PlayerSettings.IsMusicEnabled == false)
            {
                PauseMusic();
                
                #if FIREBASE_DEFINED
                _firebaseEventsService.LogEvent("music_state", new Parameter("enabled", 0));
                #endif
            }
            else
            {
                _audioSourceContainer.MusicAudioSource.volume = 1;
                
                _audioSourceContainer.MusicAudioSource.UnPause();
                
                #if FIREBASE_DEFINED
                _firebaseEventsService.LogEvent("music_state", new Parameter("enabled", 1));
                #endif
            }
        }
        

        private void OnSoundStateChanged()
        {
            if (_progressService.PlayerProgress.PlayerSettings.IsSoundEnabled == false)
            {
                StopSounds();
                
                #if FIREBASE_DEFINED
                _firebaseEventsService.LogEvent("sound_state", new Parameter("enabled", 0));
                #endif
                return;
            }
            
            #if FIREBASE_DEFINED
            _firebaseEventsService.LogEvent("sound_state", new Parameter("enabled", 1));
            #endif
        }

        ~SoundService()
        {
            if (_progressService.PlayerProgress == null)
            {
                return;
            }
            
            _progressService.PlayerProgress.PlayerSettings.OnMusicStateChanged -= OnMusicStateChanged;
            _progressService.PlayerProgress.PlayerSettings.OnSoundStateChanged -= OnSoundStateChanged;
            
            IsAudioSourcesSetUp = false;
        }*/
    }
}