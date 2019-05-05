/*
' Copyright (c) 2014  WROX.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Web.UI.WebControls;
using WROX.Modules.Guestbook.Components;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;

namespace WROX.Modules.Guestbook
{
	public partial class View : GuestbookModuleBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
			    cmdAdd.NavigateUrl = EditUrl("Edit");
				var showAll = Settings.AutoApprove;
				if (!showAll)
				{
					showAll = CanEdit;
				}
				rpGuestbook.DataSource = EntryController.GetEntries(ModuleId, showAll);
				rpGuestbook.DataBind();
			}
			catch (Exception exc) //Module failed to load
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

		protected void rpGuestbook_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				var cmdEdit = e.Item.FindControl("cmdEdit") as HyperLink;
				var cmdDelete = e.Item.FindControl("cmdDelete") as LinkButton;
				var cmdApprove = e.Item.FindControl("cmdApprove") as LinkButton;

				var entry = (EntryViewInfo)e.Item.DataItem;

				if (cmdDelete != null && cmdApprove != null && cmdEdit != null)
				{
					cmdDelete.CommandArgument = entry.EntryId.ToString();
					cmdApprove.CommandArgument = entry.EntryId.ToString();
					cmdEdit.NavigateUrl = EditUrl(string.Empty, string.Empty, "Edit", "EntryId=" + entry.EntryId);
					ClientAPI.AddButtonConfirm(cmdDelete, Localization.GetString("ConfirmDelete", LocalResourceFile));
					cmdApprove.Enabled = cmdApprove.Visible = !Settings.AutoApprove && !entry.Approved && CanEdit;
					if (Settings.AutoApprove)
					{
						cmdDelete.Enabled = cmdDelete.Visible = cmdEdit.Enabled = cmdEdit.Visible = (CanEdit || (entry.CreatedByUserID == UserId && UserId != -1));
					}
					else
					{
						cmdDelete.Enabled = cmdDelete.Visible = cmdEdit.Enabled = cmdEdit.Visible = CanEdit;
					}
				}
			}
		}

		protected void rpGuestbook_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			if (e.CommandName == "Delete")
			{
				var entry = EntryController.GetEntry(Convert.ToInt32(e.CommandArgument), ModuleId);
				if (entry != null)
				{
					EntryController.DeleteEntry(entry);
				}
			}
			if (e.CommandName == "Approve")
			{
				var entry = EntryController.GetEntry(Convert.ToInt32(e.CommandArgument), ModuleId);
				if (entry != null)
				{
					EntryController.Approve(entry);
				}
			}
			Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
		}

		public string EditString(EntryViewInfo entry)
		{
			return string.Format(LocalizeString("Edited"), entry.LastModifiedByUserDisplayName, entry.LastModifiedOnDate);
		}

	}
}