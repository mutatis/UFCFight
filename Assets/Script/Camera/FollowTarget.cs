// FollowPlayer.cs
//
// Author:
//       Yves J. Albuquerque <yves.albuquerque@gmail.com>
//
// Copyright (c) 2014 Yves J. Albuquerque
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using UnityEngine;

/// <summary>
/// Follow player.
/// </summary>
/// <remarks>
/// Follow player in X axis
/// </remarks>
public class FollowTarget : MonoBehaviour
{
    public Transform target; // Reference to the player.
	public Vector3 offset;   // The offset at which the Health Bar follows the player.
    public float smoothTime = 0.3f; //Makes this behaviour smooth
    private float xPosition; //wanted X position
    private float yPosition; //wanted Y position
    private Vector3 velocity = Vector3.zero; //A reference value used by SmoothDamp that tracks this object velocity
	
    /// <summary>
    /// SmoothDamp is used in FixedUpdate to avoid glitchs caused by non-linear equation
    /// </summary>
	void FixedUpdate ()
	{
        xPosition = target.position.x + offset.x;
        yPosition = offset.y;
	    transform.position = Vector3.SmoothDamp(transform.position, new Vector3(xPosition, yPosition, transform.position.z), ref velocity, smoothTime);
	}
}