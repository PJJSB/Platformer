using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private void PlayFootstep()
    {
        AudioManager.GetInstance().Play(AudioManager.SoundType.footstep);
    }
}
