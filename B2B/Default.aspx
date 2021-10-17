<%@ Page Title="Page 1" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="B2B._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<div>
	<p style="font-weight: bold;">
		Page 1
	</p>
	<hr />
			<table class="form-table-1">
				<tr>
					<td class="td-1">Title</td>
					<td class="td-2"><asp:TextBox ID="txtTitle" runat="server"></asp:TextBox></td>
					<td class="td-1">Address</td>
					<td class="td-3"><asp:TextBox ID="txtAddress"  runat="server" TextMode="MultiLine"></asp:TextBox></td>
				</tr>
             </table>
    <table class="form-table-2">
				<tr>
					<td class="td-1">Type</td>
					<td class="td-4">
<%--						<asp:DropDownList ID="dd" CssClass="custom" runat="server"  class="td-4"></asp:DropDownList>--%>

   <asp:TextBox ID="types" runat="server" CssClass="custom" clientidmode="Static"></asp:TextBox>

					</td>
				</tr>
        </table>
    <table class="form-table-3">

				<tr>
					<td class="td-1">Date</td>
					<td class="td-5" style="position:relative">
                        <asp:TextBox ID="txtDate" runat="server" autocomplete="off"></asp:TextBox>
                        <asp:CustomValidator runat="server" ControlToValidate="txtDate" ErrorMessage="yyyy-MM-dd" ForeColor="Red" OnServerValidate="CustomValidatorDate_ServerValidate" />
                       </td>
					<td class="td-1">Time</td>
					<td class="td-5">
                        <asp:TextBox ID="txtTime" runat="server" autocomplete="off"></asp:TextBox>
                        <asp:CustomValidator runat="server" ControlToValidate="txtTime" ErrorMessage="HH:mm|H:mm" ForeColor="Red" OnServerValidate="CustomValidatorTime_ServerValidate" />
					</td>
					<td class="td-1">Remarks</td>
					<td class="td-6">
						<asp:TextBox ID="txtRemarks" runat="server"></asp:TextBox></td>
					<td class="td-pre-submit-button">
						<asp:Button ID="btnFormSubmit" CssClass="button" runat="server" Text="Submit" OnClick="btnFormSubmit_Click" /></td>

				</tr>
			</table>
		<span id='alrt'>This is a tooltip! <span id="clsTips" onclick="close_tips()">X</span></span>
			<table class="form-table-4">
				<tr style="height:20px;background-color:#c7d5de"><td style="padding:12px 50px 10px 100px;position:relative;">
                   <div class="tips-div"><span onclick="tips()">?</span></div>
                    <asp:TextBox ID="txtSearch" placeholder="Search....." clientidmode="Static" runat="server" OnTextChanged="txtSearch_TextChanged" autocomplete="off" AutoPostBack="True"></asp:TextBox>
                    <%--onkeypress="return EnterEvent(event)"--%>
                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" style="display:none;" runat="server"  Text="Button" />
                </td></tr>
                </table>
                <table class="form-table-5">
				<tr>
					<td>
						<asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"   
        DataKeyNames="id" BorderWidth="1px" CellPadding="2" ForeColor="Black"
                             OnRowDeleting="gvResult_RowDeleting">  
        <AlternatingRowStyle BackColor="White" />  
        <Columns>  
             <asp:TemplateField HeaderStyle-Width="0px">  
                <ItemTemplate>  
                    <asp:Label ID="gv_lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>  
                </ItemTemplate>  
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Title" HeaderStyle-Width="65px"> 
                <ItemTemplate>  
                    <asp:Label ID="gv_lblTitle" style="width: 10px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis" runat="server" Text='<%# Bind("title") %>'></asp:Label>  
                </ItemTemplate>  
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="Address" HeaderStyle-Width="250px">
                <ItemTemplate>  
                    <asp:Label Width="250px" ID="gv_lblAddress" style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis" runat="server" Text='<%# Bind("Address") %>'></asp:Label>  
                </ItemTemplate>  
            </asp:TemplateField>  
			<asp:TemplateField HeaderText="Type" HeaderStyle-Width="65px">  
                <ItemTemplate>  
                    <asp:Label ID="gv_lblTypes" runat="server" Text='<%# Bind("Types") %>'></asp:Label>  
                </ItemTemplate>  
            </asp:TemplateField> 
			<asp:TemplateField HeaderText="Date" HeaderStyle-Width="140px" >  
                <ItemTemplate>  
                    <asp:Label ID="gv_lblDate" runat="server" Text=' <%# Eval("Date") == DBNull.Value ? "" : Convert.ToDateTime(Eval("Date")).ToShortDateString() %>'></asp:Label>  
                </ItemTemplate>  
            </asp:TemplateField> 
			<asp:TemplateField HeaderText="Time" HeaderStyle-Width="65px">  
                <ItemTemplate>  
                    <asp:Label ID="gv_lblTime" runat="server" Text='<%# Bind("Time") %>'></asp:Label>  
                </ItemTemplate>  
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="265px">  
                <ItemTemplate>  
                    <asp:Label ID="gv_lblRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>  
                </ItemTemplate>  
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:Button CssClass="gv_button" Text="Delete" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />
            </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>  
        <FooterStyle BackColor="White" />  
        <HeaderStyle BackColor="White" Font-Bold="True" />  
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue"   
            HorizontalAlign="Center" />  
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />  
        <SortedAscendingCellStyle BackColor="#FAFAE7" />  
        <SortedAscendingHeaderStyle BackColor="#DAC09E" />  
        <SortedDescendingCellStyle BackColor="#E1DB9C" />  
        <SortedDescendingHeaderStyle BackColor="#C2A47B" />  
    </asp:GridView>  
					</td>
				</tr>
			</table>
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="button"  />
		</div>
   
</asp:Content>
