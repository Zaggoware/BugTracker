namespace Zaggoware.BugTracker.Data
{
	using System;
	using System.Data.Entity;

	using Zaggoware.BugTracker.Data.Entities;

	/// <summary>
	/// The db context.
	/// </summary>
	public class DbContext : System.Data.Entity.DbContext, IDbContext
	{
		#region Public Events

		/// <summary>
		/// The changes saved.
		/// </summary>
		public event ChangesSavedEventHandler ChangesSaved;

		/// <summary>
		/// The saving changes.
		/// </summary>
		public event EventHandler SavingChanges;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the bugs.
		/// </summary>
		public IDbSet<Bug> Bugs { get; set; }

		/// <summary>
		/// Gets or sets the organizations.
		/// </summary>
		public IDbSet<Organization> Organizations { get; set; }

		/// <summary>
		/// Gets or sets the privileges.
		/// </summary>
		public IDbSet<Privilege> Privileges { get; set; }

		/// <summary>
		/// Gets or sets the projects.
		/// </summary>
		public IDbSet<Project> Projects { get; set; }

		/// <summary>
		/// Gets or sets the user groups.
		/// </summary>
		public IDbSet<UserGroup> UserGroups { get; set; }

		/// <summary>
		/// Gets or sets the users.
		/// </summary>
		public IDbSet<User> Users { get; set; }

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The save changes.
		/// </summary>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		public override int SaveChanges()
		{
			if (this.SavingChanges != null)
			{
				this.SavingChanges(this, EventArgs.Empty);
			}

			int numberOfStateEntries = base.SaveChanges();

			if (this.ChangesSaved != null)
			{
				this.ChangesSaved(this, new ChangesSavedEventArgs(numberOfStateEntries));
			}

			return numberOfStateEntries;
		}

		#endregion
	}
}