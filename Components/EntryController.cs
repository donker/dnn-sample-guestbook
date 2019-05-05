using System;
using System.Collections.Generic;
using System.Linq;
using DotNetNuke.Data;

namespace WROX.Modules.Guestbook.Components
{
	public class EntryController
	{
		public static IEnumerable<EntryViewInfo> GetEntries(int moduleId, bool includeNonApproved)
		{
			IEnumerable<EntryViewInfo> entries;

			using (IDataContext ctx = DataContext.Instance())
			{
				var rep = ctx.GetRepository<EntryViewInfo>();
				entries = rep.Get(moduleId);
			}
			if (!includeNonApproved)
			{
				entries = entries.Where(e => e.Approved);
			}


			return entries.OrderByDescending(e => e.CreatedOnDate);
		}

		public static EntryInfo GetEntry(int entryId, int moduleId)
		{
			EntryInfo entry;
			using (IDataContext ctx = DataContext.Instance())
			{
				var rep = ctx.GetRepository<EntryInfo>();
				entry = rep.GetById(entryId, moduleId);
			}
			return entry;
		}

		public static void UpdateEntry(EntryInfo entry, int userId)
		{
			entry.LastModifiedByUserID = userId;
			entry.LastModifiedOnDate = DateTime.Now;
			using (IDataContext ctx = DataContext.Instance())
			{
				var rep = ctx.GetRepository<EntryInfo>();
				rep.Update(entry);
			}
		}

		public static void AddEntry(EntryInfo entry, int userId)
		{
			entry.CreatedByUserID = userId;
			entry.CreatedOnDate = DateTime.Now;
			entry.LastModifiedByUserID = userId;
			entry.LastModifiedOnDate = DateTime.Now;
			using (IDataContext ctx = DataContext.Instance())
			{
				var rep = ctx.GetRepository<EntryInfo>();
				rep.Insert(entry);
			}
		}

		public static void DeleteEntry(EntryInfo entry)
		{
			using (IDataContext ctx = DataContext.Instance())
			{
				var rep = ctx.GetRepository<EntryInfo>();
				rep.Delete(entry);
			}
		}

		public static void Approve(EntryInfo entry)
		{
			entry.Approved = true;
			using (IDataContext ctx = DataContext.Instance())
			{
				var rep = ctx.GetRepository<EntryInfo>();
				rep.Update(entry);
			}
		}
	}
}