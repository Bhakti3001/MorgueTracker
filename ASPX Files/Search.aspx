<%@ Page Title="Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="MorgueTracker3.Search" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <! TODO implement HR for form>
    <main>
        <section class="row justify-content-center text-center" aria-labelledby="searchPatientTitle">
            <div class="col-md-10 text-center">
                <h1 id="searchPatientTitle">Search Patient</h1>
                <div class="row justify-content-center">
                    <div class="col-md-10">
                        <hr />

                        <div class="form-group row mt-5 mb-3">
                            <div class="col-md-9 text-start">
                                <asp:TextBox ID="txtPatientID" runat="server" class="form-control form-control-lg justify-content-center shadow-none" Placeholder="Scan Patient ID"></asp:TextBox>
                            </div>
                            <div class="col-md-3 text-end media-search">
                                <asp:Button ID="btnSearch" class="btn-media btn btn-primary btn-lg col-md-12" runat="server" Text="Search" OnClick="Search_Click"></asp:Button>
                            </div>
                        </div>

                        <asp:Panel ID="hrResult" runat="server" CssClass="horizontal-line" Visible="true"></asp:Panel>

                        <div class="row">
                            <div class="col text-start mt-2">
                                <div class="form-group">
                                    <asp:Label ID="lblPatientName" CssClass="label" runat="server">Patient Name:</asp:Label>
                                    <asp:TextBox ID="txtPatientName" class="form-control form-control-lg mb-4 shadow-none" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblEmployeeName" CssClass="label" runat="server">Onboarding Employee Name:</asp:Label>
                                    <asp:TextBox ID="txtEmployeeName" class="form-control form-control-lg mb-4 shadow-none" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class=" col text-start mt-2">
                                <div class="form-group">
                                    <asp:Label ID="lblEmployeeID" CssClass="label" runat="server">Onboarding Employee ID:</asp:Label>
                                    <asp:TextBox ID="txtEmployeeID" class="form-control form-control-lg mb-4 shadow-none" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblCreatedDate" CssClass="label" runat="server">Date Added:</asp:Label>
                                    <asp:Label ID="pCreatedDate" class="form-control form-control-lg mb-4 shadow-none" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row form-group justify-content-center ">
                            <asp:Label ID="lblLocationInMorgue" runat="server" class="label text-start" Style="padding-left: 13px">Location In Morgue:</asp:Label>
                            <div class="col-md-6 ">
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlLocationInMorgue" Style="color: #808080" runat="server" class="form-control form-control-lg justify-content-center shadow-none ">

                                        <asp:ListItem Enabled="true" Text="Select Location" Value="-1"></asp:ListItem>
                                        <asp:ListItem Value="Walk-In Left" Text="Walk-In Left"></asp:ListItem>
                                        <asp:ListItem Value="Walk-In Right" Text="Walk-In Right"></asp:ListItem>
                                        <asp:ListItem Value="Walk-In Middle" Text="Walk-In Middle"></asp:ListItem>
                                        <asp:ListItem Value="Walk-In Top Shelf" Text="Walk-In Top Shelf"></asp:ListItem>
                                        <asp:ListItem Value="Walk-In Bottom Shelf" Text="Walk-In Bottom Shelf"></asp:ListItem>
                                        <asp:ListItem Value="Walk-In Bassinet" Text="Walk-In Bassinet"></asp:ListItem>
                                        <asp:ListItem Value="Stand-Up Top" Text="Stand-Up Top"></asp:ListItem>
                                        <asp:ListItem Value="Stand-Up Middle" Text="Stand-Up Middle"></asp:ListItem>
                                        <asp:ListItem Value="Stand-Up Bottom" Text="Stand-Up Bottom"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="media-search col-md-6">
                                <div class="d-flex justify-content-between">
                                    <asp:Button ID="btnUpdate" CssClass="btn btn-primary btn-lg flex-fill me-2" runat="server" Text="Update" OnClick="Update_Click" />
                                    <asp:Button ID="btnRelease" CssClass="btn btn-primary btn-lg flex-fill ms-2" runat="server" Text="Release" OnClick="Release_Click"/>
                                </div>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="hrLine" CssClass=" hr-line mt-5 mb-4"></asp:Panel>


                        <div class="row">
                            <div class="col text-start">
                                <div class="form-group">
                                    <asp:Label ID="lblFuneralHome" CssClass="label" runat="server">Funeral Home:</asp:Label>
                                    <asp:TextBox ID="txtFuneralHome" class="form-control form-control-lg mb-4 shadow-none" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblFuneralHomeEmployee" CssClass="label" runat="server">Funeral Home Employee:</asp:Label>
                                    <asp:TextBox ID="txtFuneralHomeEmployee" class="form-control form-control-lg mb-5 shadow-none" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col text-start">

                                <div class="form-group">
                                    <asp:Label ID="lblOutEmployeeID" CssClass="label" runat="server">Releasing Employee ID:</asp:Label>
                                    <asp:TextBox ID="txtOutEmployeeID" class="form-control form-control-lg mb-4 shadow-none" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblOutEmployeeName" CssClass="label" runat="server">Releasing Employee Name: </asp:Label>
                                    <asp:TextBox ID="txtOutEmployeeName" class="form-control form-control-lg mb-5 shadow-none" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <section class="row d-flex justify-content-end">
                            <div class="col-md-9">
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="btnSubmit" CssClass="btn-media btn btn-primary btn-lg col-md-12 mb-5" runat="server" Text="Submit" data-bs-toggle="modal" data-bs-target="#confirmationModal" OnClientClick="return false;" />
                            </div>
                        </section>
                        <div id="confirmationModal" class="modal fade" tabindex="-1" role="dialog">
                            <div class="modal-dialog modal-dialog-centered" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">Patient Release Confirmation</h5>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="lblConfirmationMessage" runat="server" Text="" CssClass="mb-3" />
                                        <div class="text-start">
                                            <asp:Label ID="message" runat="server" Text="Are you sure you wish to release this patient?"></asp:Label>
                                        </div>

                                        <div class="d-flex justify-content-end">
                                            <button type="button" class="btn btn-secondary me-2" data-bs-dismiss="modal">Return</button>
                                            <asp:Button ID="btnSubmitConfirm" CssClass="btn-media-lg btn btn-primary" runat="server" Text="Yes" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="lblSuccessStatus" runat="server" class="form-control form-control-lg mb-5 col-md-3 p-5"></asp:Label>
                    </div>

                </div>
    
            </div>

        </section>
    </main>


</asp:Content>
