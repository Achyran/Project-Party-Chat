
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.SDK3.Video.Components.Base;
using VRC.SDK3.Components;
using VRC.Udon;
using VRC.SDK3.Components.Video;

public class Jukebox : UdonSharpBehaviour
{
    private BaseVRCVideoPlayer _videoPlayer;
    public AudioSource Speker;
    public Slider slider;
    public VRCUrlInputField inputField;
    [UdonSynced] VRCUrl _syncedURL;
    private VRCUrl _currentUrl;
    private bool _canSkip = true;
    public JukeboxQueue _queue;
    public Text _debuggText ;
    

    void Start()
    {
        _videoPlayer = ( BaseVRCVideoPlayer) GetComponent(typeof(BaseVRCVideoPlayer));
    }

    public void PlayPause()
    {
        if (_videoPlayer.IsPlaying) _videoPlayer.Pause();
        else _videoPlayer.Play();
    }

    public void SetURL()
    {
        _queue.AddToQueue(inputField.GetUrl());
    }

    public void SetVolume()
    {
        Speker.volume = slider.value;
    }
    public void Skip()
    {
        if (_canSkip)
        {
            Debug.Log("Skiping Song");
            _queue.Skip();
            _canSkip = false;
            _syncedURL = _queue.GetURL(0);
            _currentUrl = _syncedURL;
            _videoPlayer.LoadURL(_currentUrl);
        }
        else Debug.Log("Skip was disabled");
    }

   
    public override void OnVideoEnd()
    {
        Skip();
    }

    public override void OnVideoReady()
    {
        _canSkip = true;
        _videoPlayer.Play();
    }

    public void SetOwner()
    {
        Debug.Log("Set Owner is Called");
        if (!Networking.IsOwner(this.gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
            Debug.Log("OwnerChanged");
        }
    }
    public override void OnDeserialization()
    {
        Debug.Log("Deserialisation is called ");
        if(_currentUrl.Get() != _syncedURL.Get() && _syncedURL.Get() != "" && _syncedURL.Get() != null)
        {
            Debug.Log("Current URl = " + _currentUrl.Get());
            Debug.Log("Synced URl = " + _syncedURL.Get());
            _currentUrl = _syncedURL;
            _videoPlayer.LoadURL(_currentUrl);
        }
    }
    public override void OnVideoError(VideoError videoError)
    {
        if(VideoError.RateLimited == videoError) { }
    }

}
