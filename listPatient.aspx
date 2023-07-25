<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="listPatient.aspx.cs" Inherits="Morgue_Tracker.listPatient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <link rel="stylesheet" type="text/css" href="content/jquery.datetimepicker.css" />
    <main>
        <h1 id="listPatientTitle" class=" text-center" style="font-size: 30px">Current Morgue Patients</h1>
        <br />
        <br />
        <section class="row justify-content-center" aria-labelledby="listPatientsTitle">
            <div class="col-12 mt-3">
                <div class="row">
                    <div class="col-md-3 pr-1 d-flex flex-column align-items-start">
                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" CssClass="left-align-label" Style="font-family: 'Microsoft JhengHei', sans-serif; font-weight: bold; font-size: 15px"></asp:Label>
                        <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" AutoPostBack="true" OnTextChanged="SearchByDate_Click" Style="width: 190px; font-family: 'Microsoft JhengHei', sans-serif; font-size: 15px" />
                    </div>
                    <div class="col-md-7 d-flex flex-column align-items-left">
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date:" CssClass="left-align-label" Style="font-family: 'Microsoft JhengHei', sans-serif; font-weight: bold; font-size: 15px"></asp:Label>
                        <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" AutoPostBack="true" OnTextChanged="SearchByDate_Click" Style="width: 190px; font-family: 'Microsoft JhengHei', sans-serif; font-size: 15px" />
                    </div>
                    <div class="col-md-2 d-flex flex-column align-items-end">
                        <div class="form-group">
                            <br />
                            <asp:CheckBox ID="PickUpCheck" runat="server" OnCheckedChanged="SearchByDate_Click" AutoPostBack="true" Style="text-align: right;" />
                            <asp:Label ID="PickUpLabel" runat="server" Text="Picked Up" Style="font-family: 'Microsoft JhengHei', sans-serif; font-weight: bold; font-size: 15px" AssociatedControlID="PickUpCheck" />

                        </div>

                    </div>
                    <br />
                </div>
            </div>
            <br />
            <br />
            <br />
            <br />
            <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-light table-hover table-striped" AllowPaging="false" PageSize="5" OnPageIndexChanging="gvList_PageIndexChanging">
                <PagerSettings Position="Bottom" />
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
            <br />
            <br />
            <br />
            <div class="row d-flex justify-content-end">
                <asp:Button ID="btnExport" runat="server" CssClass=" col-md-3 btn-custom btn-primary btn-lg" Text="Export to Excel" OnClick="btnExport_Click"  />
            </div>
        </section>
    </main>

</asp:Content>
