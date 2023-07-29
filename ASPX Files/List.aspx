<%@ Page Title="List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" EnableEventValidation="false" Inherits="MorgueTracker3.List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h1 id="listPatientTitle" class=" text-center">Current Morgue Patients</h1>
        <hr />
        <section class="row justify-content-center" aria-labelledby="listPatientsTitle">
            <div class="col-12 mt-3">
                <div class="row">
                    <div class="col-md-3 d-flex flex-column align-items-start">
                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" class="date-picker-label" ></asp:Label>
                        <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" AutoPostBack="true" CssClass="date-picker" />
                    </div>
                    <div class="col-md-7 d-flex flex-column align-items-start">
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date:" class="date-picker-label" ></asp:Label>
                        <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" AutoPostBack="true" CssClass="date-picker"  />
                    </div>
                    <div class="picked-up col-md-2 d-flex flex-column ">
                        <div class="form-group mt-4">
                            <asp:CheckBox ID="PickUpCheck" runat="server" AutoPostBack="true" />
                            <asp:Label ID="PickUpLabel" runat="server" Text="Picked Up" class="date-picker-label" AssociatedControlID="PickUpCheck" />
                        </div>
                    </div>
                    <br />
                </div>
       
                <asp:Label ID="lblStatus" runat="server" CssClass="form-control form-control-lg text-center my-5 col-md-3 p-5" ></asp:Label>
             
            </div>
            <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-light custom-table my-5    x" >

                <Columns>
                    <asp:BoundField DataField="Patient_Name" HeaderText="Patient Name" ItemStyle-Width="120" />
                    <asp:BoundField DataField="Patient_ID" HeaderText="Patient ID" ItemStyle-Width="100" />
                    <asp:BoundField DataField="In_Employee_Name" HeaderText="Employee Name" ItemStyle-Width="100" />
                    <asp:BoundField DataField="In_Employee_ID" HeaderText="Employee ID" ItemStyle-Width="110" />
                    <asp:BoundField DataField="Created_Date" HeaderText="Created Date" ItemStyle-Width="180" />
                    <asp:BoundField DataField="Location_In_Morgue" HeaderText="Location In Morgue" ItemStyle-Width="180" />
                    <asp:BoundField DataField="Funeral_Home" HeaderText="Funeral Home" ItemStyle-Width="160" />
                    <asp:BoundField DataField="Funeral_Home_Employee" HeaderText="Funeral Home Employee" ItemStyle-Width="100" />
                    <asp:BoundField DataField="Out_Employee_Name" HeaderText="Release Employee Name" ItemStyle-Width="100" />
                    <asp:BoundField DataField="Out_Employee_ID" HeaderText="Release Employee ID" ItemStyle-Width="110" />
                    <asp:BoundField DataField="Picked_Up_Date" HeaderText="Picked Up Date" ItemStyle-Width="180" />
                </Columns>
            </asp:GridView>

            <div class="row d-flex justify-content-end ">
                <asp:Button ID="btnExport" runat="server" CssClass="col-md-3 btn btn-primary btn-lg mb-5" Text="Export to Excel" OnClick="btnExport_Click" />
            </div>
        </section>

    </main>
</asp:Content>