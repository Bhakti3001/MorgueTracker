<%@ Page Title="Insert Patient" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InsertPatient.aspx.cs" Inherits="MorgueTracker3.InsertPatient" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <section class="row justify-content-center" aria-labelledby="insertPatientTitle">
            <div class="col-lg-9 text-center">
                <h1 id="insertPatientTitle">Insert Patient</h1>
                <div class="container">
                    <hr />
                    <br />
                    <br />
                    <div class="row d-flex flex-wrap text-start ">
                        <div class="col ">
                            <div class="form-group ">
                                <asp:Label ID="lbPatientID" runat="server">Patient ID</asp:Label>
                                <asp:TextBox ID="txtPatientID" runat="server" class="form-control form-control-lg justify-content-center shadow-none mb-3"></asp:TextBox>
                                <br />
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbPatientName" runat="server">Patient Name</asp:Label>
                                <asp:TextBox ID="txtPatientName" runat="server" class="form-control form-control-lg shadow-none "></asp:TextBox>
                            </div>
                            <br />
                        </div>
                        <div class="col">
                            <div class="form-group">
                                <asp:Label ID="lblEmployeeID" runat="server">Employee ID</asp:Label>
                                <asp:TextBox ID="txtEmployeeID" runat="server" class="form-control form-control-lg justify-content-center shadow-none mb-3"></asp:TextBox>
                                <br />
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblEmployeeName" CssClass="label" runat="server">Employee Name</asp:Label>
                                <asp:TextBox ID="txtEmployeeName" runat="server" class="form-control form-control-lg shadow-none "></asp:TextBox>
                            </div>
                            <br />
                        </div>
                    </div>
                    <div class="row justify-content-center">
                        <div class="col text-end">
                            <asp:Button ID="Submit" runat="server" Text="Upload" OnClick="Submit_OnClick" class="btn-media-lg btn btn-primary btn-lg col-md-3 my-5"></asp:Button>
                        </div>
                    </div>
                    <br />
                    <div class="row justify-content-center">
                        <div class="col ">
                            <asp:Label ID="lbStatus" Visible="false" class="form-control form-control-lg mb-4 col-md-3 p-5" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
    

            </div>
        </section>
    </main>
</asp:Content>