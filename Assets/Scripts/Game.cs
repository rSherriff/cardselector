using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Game
{
    public string title;
    public string picture;
    public string credit;
    public string description;
    public string executable;
}

[System.Serializable]
public class Games
{
    public string title;
    public string subtitle;
    public float radius;
    public float distance;
    public Game[] games;
}