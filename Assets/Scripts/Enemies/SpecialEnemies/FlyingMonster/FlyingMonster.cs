using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingMonster : MonoBehaviour
{
   [SerializeField] private AIPath aiPath;
   private SpriteRenderer _sprite;

   private void Awake()
   {
      _sprite = GetComponent<SpriteRenderer>();
   }

   private void Update()
   {
      _sprite.flipX = aiPath.desiredVelocity.x <= 0.01f;
   }
}
