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
using DotNetNuke.Entities.Users;
using DotNetNuke.Security;
using WROX.Modules.Guestbook.Components;
using DotNetNuke.Services.Exceptions;

namespace WROX.Modules.Guestbook
{
	public partial class Edit : GuestbookModuleBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				if (!Page.IsPostBack)
				{
					if (EntryId > 0)
					{
						if (UserId == -1)
						{
							throw new Exception("Anonymous users cannot edit messages");
						}
						var entry = EntryController.GetEntry(EntryId, ModuleId);
						if (!CanEdit)
						{
							if (entry.CreatedByUserID != UserId)
							{
								throw new Exception("You cannot edit someone else's message");
							}
						}
						txtMessage.Text = entry.Message;
					}
					divApproveWarning.Visible = !Settings.AutoApprove;
				}
			}
			catch (Exception exc) //Module failed to load
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}


		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			var entry = new EntryInfo();
			if (EntryId > 0)
			{
				if (UserId == -1)
				{
					throw new Exception("Anonymous users cannot edit messages");
				}
				entry = EntryController.GetEntry(EntryId, ModuleId);
				if (!CanEdit)
				{
					if (entry.CreatedByUserID != UserId)
					{
						throw new Exception("You cannot edit someone else's message");
					}
				}
			}
			else
			{
				entry.Approved = Settings.AutoApprove;
				entry.ModuleId = ModuleId;
			}

			entry.Message = (new PortalSecurity()).InputFilter(txtMessage.Text, PortalSecurity.FilterFlag.NoMarkup | PortalSecurity.FilterFlag.NoSQL | PortalSecurity.FilterFlag.NoScripting | PortalSecurity.FilterFlag.NoAngleBrackets);

			if (EntryId > 0)
			{
				EntryController.UpdateEntry(entry, UserId);
			}
			else
			{
				EntryController.AddEntry(entry, UserId);
			}
			Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
		}
	}
}