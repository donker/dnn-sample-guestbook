<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="WROX.Modules.Guestbook.View" %>
<%@ Import Namespace="WROX.Modules.Guestbook.Components" %>

<div>
 <asp:Repeater runat="server" ID="rpGuestbook" OnItemDataBound="rpGuestbook_ItemDataBound" OnItemCommand="rpGuestbook_ItemCommand">
  <ItemTemplate>
   <div class="row-fluid messageRow">
    <div class="span3">
     <h3>
      <%# Eval("CreatedByUserDisplayName") %>
     </h3>
     <p><%# ((DateTime)Eval("CreatedOnDate")).ToString("D") %></p>
    </div>
    <div class="span9 message">
     <div>
      <%# Eval("Message") %>
     </div>
     <div class="messageButtons">
      <asp:HyperLink ID="cmdEdit" runat="server" ResourceKey="cmdEdit" Visible="false" Enabled="false" CssClass="btnMessage" />
      <asp:LinkButton ID="cmdApprove" runat="server" ResourceKey="cmdApprove" Visible="false" Enabled="false" CommandName="Approve" CssClass="btnMessage" />
      <asp:LinkButton ID="cmdDelete" runat="server" ResourceKey="cmdDelete" Visible="false" Enabled="false" CommandName="Delete" CssClass="btnMessage" />
     </div>
    </div>
    <div class="editNote" style="display:<%#((DateTime)Eval("CreatedOnDate") == (DateTime)Eval("LastModifiedOnDate") ? "none" : "block") %>">
     <%# EditString((EntryViewInfo)Container.DataItem) %>
    </div>
   </div>
  </ItemTemplate>
 </asp:Repeater>
</div>

<asp:HyperLink runat="server" ID="cmdAdd" CssClass="dnnPrimaryAction">
 <%=LocalizeString("cmdAdd") %>
</asp:HyperLink>
