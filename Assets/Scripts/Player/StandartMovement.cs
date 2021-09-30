using UnityEngine;

namespace Player
{
    public class StandartMovement : MovementBase
    {
		public StandartMovement(Transform transform, float speed) : base(transform, speed)	{}

		public override void Move()
		{
			var horizontalInput = Input.GetAxis("Horizontal") * _speed * Time.fixedDeltaTime;
			var verticalInput = Input.GetAxis("Vertical") * _speed * Time.fixedDeltaTime;

			_transform.position += new Vector3(horizontalInput, verticalInput, 0f);
		}
    }
}
