using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "SO/Player/PlayerData", fileName = "PlayerData")]
public class PlayerSO : ScriptableObject
{
    public float MoveSpeed;
    public float RayLength;
    public LayerMask WhatIsObstacle;
}
