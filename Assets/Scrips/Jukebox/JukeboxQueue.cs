
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components.Video;

public class JukeboxQueue : UdonSharpBehaviour
{
    [UdonSynced] private VRCUrl _queue0;
    [UdonSynced] private VRCUrl _queue1;
    [UdonSynced] private VRCUrl _queue2;
    [UdonSynced] private VRCUrl _queue3;
    [UdonSynced] private VRCUrl _queue4;
    [UdonSynced] private VRCUrl _queue5;
    [UdonSynced] private VRCUrl _queue6;
    [UdonSynced] private VRCUrl _queue7;
    [UdonSynced] private VRCUrl _queue8;
    [UdonSynced] private VRCUrl _queue9;
    private VRCUrl[] _queue;
    [UdonSynced] public int _queuePointer;
    private int _lastQueuePointerpos;
    private int _maxQueueSize = 10;
    void Start()
    {
        _queue = new VRCUrl[_maxQueueSize++];
    }

    public void AddToQueue(VRCUrl pUrl)
    {
        if (_queuePointer <= _maxQueueSize)
        {
            _queue[_queuePointer] = pUrl;
            _queuePointer++;
            PushArray();
        }
        Debug.Log("Songs in que = " + _queuePointer);
    }
    public void Skip()
    {
   
        if (_queuePointer > 1)
        {
            for (int i = 0; i < _queue.Length - 1; i++)
            {
                _queue[i] = _queue[i+1];
            }
            _queuePointer--;
            PushArray();
            Debug.Log("Queue: Skiped");
        }
        Debug.Log("Songs in que = " + _queuePointer);
        if (_queue[0] != null) Debug.Log("0: " + _queue[0].Get());
        if (_queue[1] != null) Debug.Log("1: " + _queue[1].Get());
        if (_queue[2] != null) Debug.Log("2: " + _queue[2].Get());
    }
    public VRCUrl GetURL(int pIndex)
    {
        Debug.Log("Url is Callde with index : " + pIndex);
        if(pIndex < _maxQueueSize)
        return _queue[pIndex];
        return null;
    }
    public override void OnDeserialization()
    {
        if(_queuePointer != _lastQueuePointerpos)
        {
            Debug.Log("Queue: Updaded The Local Aray");
            _lastQueuePointerpos = _queuePointer;
            UpdateArray();
        }
    }
    private void PushArray()
    {
        Debug.Log("Pushing Local Array to synced");
        _queue0 = _queue[0];
        _queue1 = _queue[1];
        _queue2 = _queue[2];
        _queue3 = _queue[3];
        _queue4 = _queue[4];
        _queue5 = _queue[5];
        _queue6 = _queue[6];
        _queue7 = _queue[7];
        _queue8 = _queue[8];
        _queue9 = _queue[9];
    }
    private void UpdateArray()
    {
        _queue[0] = _queue0;
        _queue[1] = _queue1;
        _queue[2] = _queue2;
        _queue[3] = _queue3;
        _queue[4] = _queue4;
        _queue[5] = _queue5;
        _queue[6] = _queue6;
        _queue[7] = _queue7;
        _queue[8] = _queue8;
        _queue[9] = _queue9;
    }

}
