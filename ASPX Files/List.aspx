<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="MorgueTracker3.List" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <section class="row justify-content-center" aria-labelledby="listPatientsTitle">
            <div class="col-12 ">
                <div class="row">
                    <div class="start-date col-md-3 d-flex flex-column align-items-start">
                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" class="date-picker-label"></asp:Label>
                        <asp:TextBox ID="txtStartDate" runat="server" OnTextChanged="SearchByDate_Click" TextMode="Date" AutoPostBack="true" CssClass="date-picker" />
                    </div>
                    <div class="end-date col-md-7 d-flex flex-column align-items-start">
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date:" class="date-picker-label"></asp:Label>
                        <asp:TextBox ID="txtEndDate" runat="server" OnTextChanged="SearchByDate_Click" TextMode="Date" AutoPostBack="true" CssClass="date-picker" />
                    </div>
                    <div class="picked-up col-md-2 d-flex flex-column ">
                        <div class="form-group text-end">
                            <asp:CheckBox ID="PickUpCheck" runat="server" AutoPostBack="true" OnCheckedChanged="SearchByDate_Click" />
                            <asp:Label ID="PickUpLabel" runat="server" OnTextChanged="SearchByDate_Click" Text="Picked Up" class="date-picker-label" AssociatedControlID="PickUpCheck" />
                        </div>
                    </div>
                    <br />
                </div>
                <asp:Label ID="lblStatus" runat="server" CssClass="form-control form-control-lg text-center my-5 col-md-3 p-5"></asp:Label>
                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-light custom-table my-5 " AllowPaging="true" PageSize="5" OnPageIndexChanging="gvList_PageIndexChanging">
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



                <div class="row d-flex justify-content-end ">
                    <div class="col text-end">
                        <asp:Button ID="btnExport" runat="server" CssClass=" btn-media col-md-2 btn btn-primary btn-lg mb-5" OnClick="btnExport_Click" Text="Export" />
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
