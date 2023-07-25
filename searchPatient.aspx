<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="searchPatient.aspx.cs" Inherits="Morgue_Tracker.searchPatient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container mt-5">
            <h1 class=" text-center" style="font-size: 30px">Search Patient</h1>
            <br />
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="input-group mb-3">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control responsive-textbox" placeholder="Scan Patient ID" Style="border-radius: 5px"></asp:TextBox>
                        <div class="row">
                            <div class="col-md-12 px-2 d-flex flex-column align-items-center">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-custom" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-4 px-2 d-flex flex-column align-items-center">
                    <div class="form-group">
                        <asp:Label ID="lblPatientName" runat="server" Text="Patient Name" CssClass="left-align-label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtPatientName" runat="server" class="form-control responsive-textbox" Visible="false"></asp:TextBox>
                    </div>
                    <br />
                    <div class="form-group">
                        <asp:Label ID="lblInEmpName" runat="server" Text="In Employee Name" CssClass="left-align-label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtInEmpName" runat="server" class="form-control responsive-textbox" Visible="false"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4 px-2 d-flex flex-column align-items-center">
                    <div class="form-group">
                        <asp:Label ID="lblMorgueLoc" runat="server" Text="Morgue Location" CssClass="left-align-label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtMorgueLoc" runat="server" class="form-control responsive-textbox" Visible="false"></asp:TextBox>
                    </div>
                    <br />
                    <div class="form-group">
                        <asp:Label ID="lblInEmpID" runat="server" Text="In Employee ID" CssClass="left-align-label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtInEmpID" runat="server" class="form-control responsive-textbox" Visible="false"></asp:TextBox>
                        <br />
                        <br />
                    </div>
                </div>
                <div class="col-md-4 px-2 d-flex flex-column align-items-center">
                    <div class="form-group">
                        <asp:Label ID="lblCreateDate" runat="server" Text="Created Date" CssClass="left-align-label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtCreateDate" runat="server" class="form-control responsive-textbox" Visible="false" ReadOnly ="true"></asp:TextBox>
                    </div>
                    <br />
                    <div class="form-group">
                        <asp:Button ID="Button1" runat="server" Text="Update" class=" btn btn-custom" Style="margin-top: 22.5px; width: 200px" OnClick="btnUpdate_Click" Visible="false" />
                        <asp:Button ID="releaseButton" runat="server" Text="Release" class=" btn btn-custom button-container responsive-button" Style="margin-top: 22.5px; width: 200px" OnClick="btnRelease_Click" Visible="false" />
                        <br />
                        <br />
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <div class="row justify-content-center">
                <div class="col-md-4 px-2 d-flex flex-column align-items-center">
                    <div class="form-group">
                        <asp:Label ID="lblFunEmpName" runat="server" Text="Funeral Home Employee Name" CssClass="left-align-label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtFunEmpName" runat="server" class="form-control responsive-textbox" Visible="false"></asp:TextBox>
                    </div>
                    <br />
                    <div class="form-group">
                        <asp:Label ID="lblOutEmpName" runat="server" Text="Out Employee Name" CssClass="left-align-label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtOutEmpName" runat="server" class="form-control responsive-textbox" Visible="false"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4 px-2 d-flex flex-column align-items-center">
                    <div class="form-group">
                        <asp:Label ID="lblFunHome" runat="server" Text="Funeral Home" CssClass="left-align-label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtFunHome" runat="server" class="form-control responsive-textbox" Visible="false"></asp:TextBox>
                    </div>
                    <br />
                    <div class="form-group">
                        <asp:Label ID="lblOutEmpID" runat="server" Text="Out Employee ID" CssClass="left-align-label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtOutEmpID" runat="server" class="form-control responsive-textbox" Visible="false"></asp:TextBox>
                        <br />
                        <br />
                    </div>
                </div>
                <div class="col-md-4 px-2 d-flex flex-column align-items-center">
                    <div class="form-group">
                        <asp:Label ID="lblReleaseDate" runat="server" Text="Release Date" CssClass="left-align-label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtReleaseDate" runat="server" class="form-control responsive-textbox" Visible="false" ReadOnly ="true"></asp:TextBox>
                    </div>
                    <br />
                    <div class="form-group">
                        <asp:Button ID="updateButton" runat="server" Text="Update" class=" btn btn-custom" Style="margin-top: 22.5px; width: 400px" OnClick="btnUpdate_Click" Visible="false" />
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>

