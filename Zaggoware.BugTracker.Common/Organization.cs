﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Common
{
	public class Organization
	{
		public int Id { get; set; }

		public string Name { get; set; }

        public IEnumerable<Project> Projects { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
	}
}
