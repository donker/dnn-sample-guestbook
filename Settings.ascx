<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="WROX.Modules.Guestbook.Settings" %>
<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

<fieldset>
 <div class="dnnFormItem">
  <dnn:Label ID="lblAutoApprove" runat="server" ResourceKey="lblAutoApprove" ControlName="chkAutoApprove" />
  <asp:CheckBox runat="server" ID="chkAutoApprove" />
 </div>
</fieldset>

