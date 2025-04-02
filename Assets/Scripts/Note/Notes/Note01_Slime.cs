using UnityEngine;

public class Note01_Slime : NoteBase
{
    public override void Attack()
    {
        base.Attack();
        transform.LookAt(Vector3.zero);
    }
}
