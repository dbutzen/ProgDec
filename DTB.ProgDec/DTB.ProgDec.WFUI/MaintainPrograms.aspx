<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaintainPrograms.aspx.cs" Inherits="DTB.ProgDec.WFUI.MaintainPrograms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="header rounded-top">
        <h3>Programs</h3>
    </div>

    <asp:Label runat="server" ID="message"></asp:Label>

    <div class="form-row ml-2 mt-2">
        <div class="control-label col-md-2">
            <asp:Label ID="lblProgramPick" runat="server" Text="Program:" >

            </asp:Label>
        </div>
        <div class="control-label col-md-3">
            <asp:DropDownList ID="ddlPrograms" 
                runat="server" 
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlPrograms_SelectedIndexChanged"
                CssClass="form-control">

            </asp:DropDownList>
        </div>
    </div>

    <div class="form-row ml-2 mt-2">
        <div class="control-label col-md-2">
            <asp:Label ID="Label1" runat="server" Text="Description:">

            </asp:Label>
        </div>
        <div class="control-label col-md-3">
            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" >

            </asp:TextBox>
        </div>
    </div>
    
    <div class="form-row ml-2 mt-2">
        <div class="control-label col-md-2">
            <asp:Label ID="Label2" runat="server" Text="Degree Types:" >

            </asp:Label>
        </div>
        <div class="control-label col-md-3">
            <asp:DropDownList ID="ddlDegreeTypes" 
                runat="server" 
                AutoPostBack="true" 
                OnSelectedIndexChanged="ddlDegreeTypes_SelectedIndexChanged" 
                CssClass="form-control">

            </asp:DropDownList>
        </div>
    </div>

    <div class="form-group mt-2 ml-5">
        <asp:Button ID="btnInsert" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Insert" OnClick="btnInsert_Click"/>
        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Update" OnClick="btnUpdate_Click"/>
        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Delete" OnClick="btnDelete_Click"/>
    </div>
</asp:Content>
