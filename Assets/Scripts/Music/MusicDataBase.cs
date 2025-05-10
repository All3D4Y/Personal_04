using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMusicDataBase", menuName = "Scriptable Objects/MusicDataBase")]
public class MusicDataBase : ScriptableObject
{
    public List<MusicMetaData> musicList;
}
