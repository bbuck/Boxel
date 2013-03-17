using UnityEngine;
using System;
using System.Text;
using System.Collections;

public enum UVPoints {
	TopRight,
	BottomRight,
	TopLeft,
	BottomLeft
}

[Serializable]
public class UVCoordinates {
	
	[SerializeField]
	private Vector2 _topRight;
	public Vector2 TopRight {
		get {
			return _topRight;
		}
		set {
			_topRight = value;
			isDirty = true;
		}
	}
	
	[SerializeField]
	private Vector2 _bottomRight;
	public Vector2 BottomRight {
		get {
			return _bottomRight;
		}
		set {
			_bottomRight = value;
			isDirty = true;
		}
	}
	
	[SerializeField]
	private Vector2 _topLeft;
	public Vector2 TopLeft {
		get {
			return _topLeft;
		}
		set {
			_topLeft = value;
			isDirty = true;
		}
	}
	
	[SerializeField]
	private Vector2 _bottomLeft;
	public Vector2 BottomLeft {
		get {
			return _bottomLeft;
		}
		set {
			_bottomLeft = value;
			isDirty = true;
		}
	}
	
	[SerializeField]
	private bool _flip;
	public bool Flip { 
		get { return _flip; } 
		set { 
			_flip = value;
			isDirty = true;
		}
	}
	
	private bool isDirty = true;
	private Vector2[] cleanArray;
	private UVPoints[] defaultOrder = new UVPoints[] {UVPoints.BottomLeft, UVPoints.TopLeft, UVPoints.TopRight, UVPoints.BottomRight};
	
	public Vector2[] ToArray() {
		return ToArray(defaultOrder);
	}
	
	public Vector2[] ToArray(UVPoints[] order) {
		if (isDirty) {
			cleanArray = new Vector2[4];
			for (int i = 0, len = order.Length; i < len; i++) {
				UVPoints point = order[i];
				switch (point) {
				case UVPoints.BottomLeft:
					cleanArray[i] = IfFlip(BottomRight, BottomLeft);
					break;
				case UVPoints.TopLeft:
					cleanArray[i] = IfFlip(TopRight, TopLeft);
					break;
				case UVPoints.BottomRight:
					cleanArray[i] = IfFlip(BottomLeft, BottomRight);
					break;
				case UVPoints.TopRight:
					cleanArray[i] = IfFlip(TopLeft, TopRight);
					break;
				}
			}
			isDirty = false;
		}
		
		return cleanArray;
	}
	
	public override string ToString() {
		StringBuilder str = new StringBuilder();
		str.Append("Top Left: (").Append(TopLeft.x).Append(", ").Append(TopLeft.y).Append(")\n");
		str.Append("Bottom Left: (").Append(BottomLeft.x).Append(", ").Append(BottomLeft.y).Append(")\n");
		str.Append("Bottom Right: (").Append(BottomRight.x).Append(", ").Append(BottomRight.y).Append(")\n");
		str.Append("Top Right: (").Append(TopRight.x).Append(", ").Append(TopRight.y).Append(")");
		return str.ToString();
	}
	
	Vector2 IfFlip(Vector2 trueValue, Vector2 falseValue) {
		if (Flip)
			return trueValue;
		else
			return falseValue;
	}
}
