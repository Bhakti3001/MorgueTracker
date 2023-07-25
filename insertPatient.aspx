<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="insertPatient.aspx.cs" Inherits="Morgue_Tracker.insertPatient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <br />
        <h1 class=" text-center" style=" font-size: 30px">Insert Patient</h1>
        <br />
        <br />
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-6 px-2 d-flex flex-column align-items-center">
                    <div class="form-group">
                        <asp:Label ID="lblPatientName" runat="server" Text="Patient Name" CssClass="left-align-label"></asp:Label>
                        <asp:TextBox ID="txtPatientName" runat="server" class="form-control responsive-textbox"></asp:TextBox>
                    </div>
                    <br />
                    <div class="form-group">
                        <asp:Label ID="lblPatientID" runat="server" Text="Patient ID" CssClass="left-align-label"></asp:Label>
                        <asp:TextBox ID="txtPatientID" runat="server" class="form-control responsive-textbox"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 px-2 d-flex flex-column align-items-center">
                    <div class="form-group">
                        <asp:Label ID="lblEmpName" runat="server" Text="Employee Name" CssClass="left-align-label"></asp:Label>
                        <asp:TextBox ID="txtEmpName" runat="server" class="form-control responsive-textbox"></asp:TextBox>
                    </div>
                    <br />
                    <div class="form-group">
                        <asp:Label ID="lblEmpID" runat="server" Text="Employee ID" CssClass="left-align-label"></asp:Label>
                        <asp:TextBox ID="txtEmpID" runat="server" class="form-control responsive-textbox"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12 px-2 d-flex flex-column align-items-center">
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Upload" OnClick="btnSubmit_Click" class="btn btn-custom" />
        </div>
    </main>
</asp:Content>
