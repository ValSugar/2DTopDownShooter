using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public interface IInteractable
    {
        void Interact(PlayerMechanics playerMechanics);
    }
}
