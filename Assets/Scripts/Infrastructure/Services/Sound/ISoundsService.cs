namespace Internal.Scripts.Infrastructure.Services.Sound
{
    public interface ISoundsService
    {
        void PlaySound(SoundType soundType);
        void EnableAudio(bool isEnabled);
        bool IsSoundEnabled { get; }
    }
}