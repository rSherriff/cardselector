using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Launcher : Singleton<Launcher>
{
    public void LaunchGame(string filename)
    {
        Process foo = new Process();
        UnityEngine.Debug.Log("Launching " + filename);
        foo.StartInfo.FileName = filename;
        foo.Start();
    }
}
