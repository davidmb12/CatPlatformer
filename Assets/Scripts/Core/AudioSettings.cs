namespace NarvalDev.Core
{
    public class AudioSettings
    {
        public bool EnableMusic;
        public bool EnableSfx;
        public float MasterVolume;

        public AudioSettings()
        {
            EnableMusic = true;
            EnableSfx = true;
            MasterVolume = .25f;
        }
    }
}
