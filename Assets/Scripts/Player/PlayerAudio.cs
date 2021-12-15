using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private void PlayFootstep()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundType.footstep);
    }
}
