using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.SDK3.Video.Components.Base;
using VRC.SDK3.Components;
using VRC.Udon;
using VRC.SDK3.Components.Video;


public class JukeboxNew : UdonSharpBehaviour
{
    private BaseVRCVideoPlayer _videoplayer;
    public AudioSource audioSource;
    public Slider slider;
    [UdonSynced] VRCUrl _synchedURL;
    private VRCUrl _localURL;
    private bool _canSkip = true;
    public VRCUrlInputField inputField;
    public JukeboxQueue queue;

    public void PlayPause()
    {
        if(_videoplayer.IsPlaying) _videoplayer.Pause();
        else _videoplayer.Play();
    }
    public void AddToQueue()
    {
        queue.AddToQueue(inputField.GetUrl());
    }
    public void Skip()
    {
        if (_canSkip)
        {
            Debug.Log("Jukebox New: Skipping song");
            _canSkip = false;
            queue.Skip();
            _synchedURL = queue.GetURL(0);
            _localURL = _synchedURL;
            _videoplayer.LoadURL(_localURL);


        }
        else Debug.Log("Jukebox New: Can not skip while loading");
    }
    public override void OnVideoEnd()
    {
        Skip();
    }
    public override void OnVideoReady()
    {
        _canSkip = true;
        _videoplayer.Play();
    }

    private void Start()
    {
        _videoplayer = (BaseVRCVideoPlayer)GetComponent(typeof(BaseVRCVideoPlayer));
        if (_videoplayer == null) Debug.LogError("VideoPlayer is missing");
    }


    public void SetOwner()
    {
        Debug.Log("Current Owner :" + Networking.GetOwner(gameObject).GetHashCode());
        if (!Networking.IsOwner(gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            Debug.Log("Changeing Owner To " + Networking.GetOwner(gameObject).GetHashCode());
        }
    }
}
