using UnityEngine;

public class SyncSFX : MonoBehaviour
{
    public SystemSFX _SFX = new SystemSFX();
    public AudioSource _AudioSFX;
    public AudioSource _moverAudio;
    private void Start()
    {

    }
   public void fireBallAttack()
    {
        _AudioSFX.clip = _SFX.fireBall;
        _AudioSFX.Play();
    }
    public void fireFlame()
    {
        _AudioSFX.clip = _SFX.fireFlame_Aud;
        _AudioSFX.Play();
    }
    public void Jumping()
    {
        _AudioSFX.clip = _SFX.Jumping;
        _AudioSFX.Play();
    }
    public void FootStep()
    {
        if(_moverAudio.clip == null)
        {
            _moverAudio.clip = _SFX.footStep;
        }
       
        _moverAudio.Play();
    }
}
