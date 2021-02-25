
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Inputtrigg : UdonSharpBehaviour
{
    public GameObject jukeboxgob;
    public Jukebox jukebox;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entert Trigger area");
        if (!Networking.IsOwner(jukeboxgob))
        {
            jukebox.SetOwner();
            
        }
    }
}
