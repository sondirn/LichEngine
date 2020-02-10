// common storage location for generic enums

namespace Nez
{
	public enum HorizontalAlign
	{
		Left,
		Center,
		Right
	}


	public enum VerticalAlign
	{
		Top,
		Center,
		Bottom
	}


	public enum Edge
	{
		Top,
		Bottom,
		Left,
		Right
	}


	public enum Direction
	{
		Up,
		Down,
		Left,
		Right
	}

	public enum EntityType
	{
		STATIC_OBJECT,
		STATIC_BREAKABLE,
		ENEMY,
		PROJECTILE,
		RAYCAST,
		SWING_COLLIDER,
		PLAYER
	}
}