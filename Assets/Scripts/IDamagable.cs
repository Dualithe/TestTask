using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    Rigidbody2D body { get; }
    void takeDamage(int damage);
}
