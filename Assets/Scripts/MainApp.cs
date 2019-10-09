using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainApp : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private List<AudioClip> _audioList;
    [SerializeField]
    private Animator _wolf;
    [SerializeField]
    private BoxCollider _wolfCollider;
    private bool _isBusy;

    private void Awake()
    {
        _isBusy = false;
    }

    private void Update() 
    {
        if (!_isBusy)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit);
                    if (hit.collider == _wolfCollider)
                    {
                        RunWolf();
                    }
                }
            }
        }

        if(!_audioSource.isPlaying)
        {
            IdleWolf();
        }
    }

    private void RunWolf()
    {
        _audioSource.Stop();
        _audioSource.clip = _audioList[1];
        _audioSource.loop = false;
        _audioSource.Play();
        _wolf.SetTrigger("Run");
        _isBusy = true;
    }

    private void IdleWolf()
    {
        _audioSource.Stop();
        _audioSource.clip = _audioList[0];
        _audioSource.loop = true;
        _audioSource.Play();
        _wolf.SetTrigger("Idle");
        _isBusy = false;
    }

}
