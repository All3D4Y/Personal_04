[System.Serializable]
public struct NoteData
{
    public NoteHeight height;
    public float bar;                   // 노트가 떨어지는 시간 (1마디) = (240/bpm)sec
    public NoteSide side;              // 노트의 레인
}
public enum NoteHeight : int
{
    Up = 0,
    Down,
    Toggle
}
public enum NoteSide : int
{
    Right = 0,
    Left,
    Both
}