namespace Zaggoware.BugTracker.Data
{
	using System;
	using System.Data.Entity;
	using System.Threading.Tasks;

	using Microsoft.AspNet.Identity.EntityFramework;

	using Zaggoware.BugTracker.Data.Entities;

	/// <summary>
	/// The db context.
	/// </summary>
	public class DbContext : IdentityDbContext<User>, IDbContext
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
		/// Gets or sets the bug reports.
		/// </summary>
		public IDbSet<BugReport> BugReports { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
	    public IDbSet<Comment> Comments { get; set; }

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

		#endregion

        #region Constructors

        public DbContext()
            : base("DefaultConnection", false)
        {
        }

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

			var numberOfStateEntries = base.SaveChanges();

			if (this.ChangesSaved != null)
			{
				this.ChangesSaved(this, new ChangesSavedEventArgs(numberOfStateEntries));
			}

			return numberOfStateEntries;
		}

        /// <summary>
        /// The save changes async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
	    public override async Task<int> SaveChangesAsync()
	    {
            if (this.SavingChanges != null)
            {
                this.SavingChanges(this, EventArgs.Empty);
            }

	        var numberOfStateEntries = await base.SaveChangesAsync();

            if (this.ChangesSaved != null)
            {
                this.ChangesSaved(this, new ChangesSavedEventArgs(numberOfStateEntries));
            }

	        return numberOfStateEntries;
	    }

	    #endregion
	}
}