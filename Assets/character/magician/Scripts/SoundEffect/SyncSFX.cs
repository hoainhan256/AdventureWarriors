using UnityEngine;

public class SyncSFX : MonoBehaviour
{
    public SystemSFX _SFX = new SystemSFX();
    public AudioSource _AudioSFX;

    private void Start()
    {
        _AudioSFX = transform.GetChild(5).gameObject.GetComponent<AudioSource>();
    }
    public void Landing()
    {
        _AudioSFX.clip = _SFX.Landing;
        _AudioSFX.Play();
    }
    public void Jumping()
    {
        _AudioSFX.clip = _SFX.Jumping;
        _AudioSFX.Play();
    }
    public void FootStep()
    {
        _AudioSFX.clip = _SFX.footStep;
        _AudioSFX.Play();
    }
}
