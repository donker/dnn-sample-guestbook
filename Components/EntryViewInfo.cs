using DotNetNuke.ComponentModel.DataAnnotations;

namespace WROX.Modules.Guestbook.Components
{
	[Scope("ModuleId")]
	[TableName("vw_WROX_Guestbook_Entries")]
	public class EntryViewInfo : EntryInfo
	{
		public string CreatedByUserDisplayName { get; set; }
		public string LastModifiedByUserDisplayName { get; set; }
	}
}