using UnityEngine;

namespace CW.Scripts
{
	public interface IPathTracker
	{
		void OnCompletePath(Node lastNode);
	}
}
