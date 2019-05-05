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
using DotNetNuke.Collections;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Security.Permissions;
using WROX.Modules.Guestbook.Components;

namespace WROX.Modules.Guestbook
{
	public class GuestbookModuleBase : PortalModuleBase
	{

		protected override void OnInit(EventArgs e)
		{
			_entryId = Request.Params.GetValueOrDefault("EntryId", -1);
		}

		private int _entryId;
		public int EntryId
		{
			get
			{
				return _entryId;
			}

		}

		public bool CanEdit
		{
			get
			{
				return ModulePermissionController.HasModulePermission(ModuleConfiguration.ModulePermissions, "EDIT");
			}
		}

		public new GuestbookSettings Settings
		{
			get
			{
				return GuestbookSettings.GetGuestbookSettings(ModuleId);
			}
		}
	}
}