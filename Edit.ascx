<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="WROX.Modules.Guestbook.Edit" %>
<h3>
 <asp:Label runat="server" ID="lblMessage" ResourceKey="lblMessage" />
</h3>
<div>
 <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="5" Width="50%" />
</div>
<div runat="server" id="divApproveWarning" class="dnnFormMessage dnnFormWarning">
 <asp:label runat="server" ID="lblApproveWarning" ResourceKey="lblApproveWarning" />
</div>
<p>
 <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" resourcekey="btnSubmit" CssClass="dnnPrimaryAction" />
 <asp:LinkButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" resourcekey="btnCancel" CssClass="dnnSecondaryAction" />
</p>
