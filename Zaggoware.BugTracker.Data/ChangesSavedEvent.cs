using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Data
{
	public delegate void ChangesSavedEventHandler(IDbContext sender, ChangesSavedEventArgs args);

	public class ChangesSavedEventArgs : EventArgs
	{
		public int NumberOfStateEntries { get; private set; }

		public ChangesSavedEventArgs(int numberOfStateEntries)
		{
			this.NumberOfStateEntries = numberOfStateEntries;
		}
	}
}
