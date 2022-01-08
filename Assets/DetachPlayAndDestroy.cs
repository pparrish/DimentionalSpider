using UnityEngine;

public class DetachPlayAndDestroy : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private void Start()
    {
        if (!TryGetComponent(out _particleSystem)) Debug.LogError("ParticleSystemMissing", this);
        var particleSystemMain = _particleSystem.main;
        particleSystemMain.stopAction = ParticleSystemStopAction.Destroy;
    }
    
    public void Execute()
    {
        if (_particleSystem)
        {
            _particleSystem.Play();
        }

        transform.parent = null;
    }
}
