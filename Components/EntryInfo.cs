using System;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace WROX.Modules.Guestbook.Components
{
	[Scope("ModuleId")]
	[TableName("WROX_Guestbook_Entries")]
	[PrimaryKey("EntryId")]
	public class EntryInfo
	{
		public int EntryId { get; set; }
		public int ModuleId { get; set; }
		public string Message { get; set; }
		public bool Approved { get; set; }
		public int CreatedByUserID { get; set; }
		public DateTime CreatedOnDate { get; set; }
		public int LastModifiedByUserID { get; set; }
		public DateTime LastModifiedOnDate { get; set; }

	}
}