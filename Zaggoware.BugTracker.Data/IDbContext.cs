namespace Zaggoware.BugTracker.Data
{
	using System.Data.Entity;
	using System.Threading.Tasks;

	using Zaggoware.BugTracker.Data.Entities;

	/// <summary>
	/// The DbContext interface.
	/// </summary>
	public interface IDbContext
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the bug reports.
		/// </summary>
		IDbSet<BugReport> BugReports { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        IDbSet<Comment> Comments { get; set; }

		/// <summary>
		/// Gets or sets the organizations.
		/// </summary>
		IDbSet<Organization> Organizations { get; set; }

		/// <summary>
		/// Gets or sets the privileges.
		/// </summary>
		IDbSet<Privilege> Privileges { get; set; }

		/// <summary>
		/// Gets or sets the projects.
		/// </summary>
		IDbSet<Project> Projects { get; set; }

		/// <summary>
		/// Gets or sets the user groups.
		/// </summary>
		IDbSet<UserGroup> UserGroups { get; set; }

		/// <summary>
		/// Gets or sets the users.
		/// </summary>
		IDbSet<User> Users { get; set; }

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// The save changes.
		/// </summary>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int SaveChanges();

		/// <summary>
		/// The save changes async.
		/// </summary>
		/// <returns>
		/// The <see cref="Task"/>.
		/// </returns>
		Task<int> SaveChangesAsync();

		#endregion
	}
}