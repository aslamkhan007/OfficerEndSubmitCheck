<%@ Page Title="" Language="C#" MasterPageFile="~/Officers/Officers.Master" AutoEventWireup="true" CodeBehind="PeriodicInspection.aspx.cs" Inherits="CEIHaryana.Officers.PeriodicInspection" %>

<%@ Register Src="~/UserCPages/InspectionReturnDetails.ascx" TagPrefix="uc1" TagName="InspectionReturnDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="shortcut icon" type="image/png" href="/css2/style.min.css" />
    <link rel="stylesheet" href="/css2/style.css" />
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.5.2/css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/1.13.5/css/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.7.0.js"></script>
    <script src="https://cdn.datatables.net/1.13.5/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.5/js/dataTables.bootstrap4.min.js"></script>
    <script src="https://kit.fontawesome.com/57676f1d80.js" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        //Allow Only Aplhabet, Delete and Backspace

        function isAlpha(keyCode) {

            return ((keyCode >= 65 && keyCode <= 90) || keyCode == 8 || keyCode == 32 || keyCode == 190)

        }

        function alphabetKey(e) {
            var allow = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz \b'
            var k;
            k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
            return (allow.indexOf(String.fromCharCode(k)) != -1);
        }
    </script>
    <style>
        .submit {
            border: 1px solid #563d7c;
            border-radius: 5px;
            color: white;
            padding: 5px 10px 5px 10px;
            background: left 3px top 5px no-repeat #563d7c;
        }

            .submit:hover {
                border: 1px solid #563d7c;
                border-radius: 5px;
                color: white;
                padding: 5px 10px 5px 10px;
                background: left 3px top 5px no-repeat #26005f;
                box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
            }

        .table-dark {
            text-align: center !important;
            background-color: #9292cc !important;
        }

        .col-md-4 {
            margin-bottom: 15px;
        }

        .form-control {
            box-shadow: rgba(0, 0, 0, 0.16) 0px 1px 4px;
            margin-left: 0px !important;
            height: 30px;
        }

        select.form-control {
            box-shadow: rgba(0, 0, 0, 0.16) 0px 1px 4px;
            margin-left: 0px !important;
            height: 30px;
        }

        label {
            font-size: 13px;
        }

        .form-control:focus {
            border: 2px solid #80bdff;
        }

        select.form-control:focus {
            border: 2px solid #80bdff;
        }

        .select2-container .select2-selection--single {
            height: 30px !important;
        }

        .select2-container--default .select2-selection--single {
            border: 1px solid #ccc !important;
            border-radius: 0px !important;
        }

        span.select2-selection.select2-selection--single {
            padding: 0px 0px 0px 5px;
            box-shadow: rgba(0, 0, 0, 0.16) 0px 1px 4px;
            margin-left: 0px !important;
            height: 30px;
            border-radius: 5px !important;
        }

            span.select2-selection.select2-selection--single:focus {
                border: 2px solid #80bdff;
            }

        .card .card-title {
            font-size: 1rem !important;
        }

        .btn-primary:hover {
            box-shadow: rgba(50, 50, 93, 0.25) 0px 30px 60px -12px inset, rgba(0, 0, 0, 0.3) 0px 18px 36px -18px inset;
        }

        button.btn.btn-primary.mr-2 {
            padding: 10px 25px 10px 25px;
            font-size: 18px;
        }

        select.form-control.select-form.select2 {
            height: 30px !important;
            padding: 2px 0px 5px 10px;
        }

        ul.chosen-choices {
            border-radius: 5px;
        }

        input#customFile {
            padding: 0px 0px 0px 0px;
        }

        input#ContentPlaceHolder1_txtName {
            font-size: 12.5px !important;
        }


        input#ContentPlaceHolder1_txtagency {
            font-size: 12.5px;
        }

        input#ContentPlaceHolder1_RadioButtonList2_0 {
            margin-left: 10px;
            margin-right: 5px;
        }

        input#ContentPlaceHolder1_RadioButtonList2_1 {
            margin-left: 10px;
            margin-right: 5px;
        }

        th.headercolor {
            background: #9292cc;
            color: white;
            width: 1% !important;
        }
        /* ------------------ MODAL STYLING ------------------ */

        /* Background Overlay */
        .modal {
            display: none;
            position: fixed;
            z-index: 9999;
            inset: 0;
            background: rgba(0, 0, 0, 0.40);
            backdrop-filter: blur(6px);
            -webkit-backdrop-filter: blur(6px);
            animation: fadeIn 0.3s ease-out;
        }

        /* Modal Box */
        .modal-content {
            background: #ffffff;
            margin: auto;
            padding: 25px 30px;
            width: 75%;
            border-radius: 12px;
            box-shadow: 0 10px 35px rgba(0,0,0,0.25);
            animation: slideUp 0.35s ease-out;
            position: relative;
            border: 1px solid #eaeaea;
            top: 10%;
            left: 7%;
        }

        /* Smooth Fade Animation */
        @keyframes fadeIn {
            from {
                opacity: 0;
            }

            to {
                opacity: 1;
            }
        }

        /* Modal Slide Animation */
        @keyframes slideUp {
            from {
                opacity: 0;
                transform: translateY(40px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        /* Close Button */
        .close {
            position: absolute;
            right: 18px;
            top: 12px;
            font-size: 26px;
            color: #666;
            font-weight: bold;
            cursor: pointer;
            transition: 0.3s ease;
        }

            .close:hover {
                color: #ff3b30;
                transform: scale(1.2);
            }

        .chk-container {
            display: flex;
            align-items: flex-start;
        }

            .chk-container label {
                margin-left: 8px;
            }
    </style>

    <script type="text/javascript">
        function alertWithRedirectdata() {

            alert('Inspection Request is Successfully Accepted');
            window.location.href = "/Officers/NewApplications.aspx";
        }
        function alertWithRedirectdataReturn() {
            alert('Inspection Request is Returned to Site Owner');
            window.location.href = "/Officers/NewApplications.aspx";
        }

        function alertWithRedirectdataSupervisorReturn() {
            alert('Inspection Request is Returned to Supervisor');
            window.location.href = "/Officers/NewApplications.aspx";
        }

        function alertWithRedirectdataCommonReturn() {
            alert('Inspection Request is Returned to Owner');
            window.location.href = "/Officers/NewApplications.aspx";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="card-body" style="box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px; padding: 25px; margin-bottom: 25px; border-radius: 10px; margin-top: 10px;">
            <div class="card-title" style="margin-bottom: 5px; font-size: 17px; margin-bottom: 20px; font-weight: 600; margin-left: -10px;">
                Inspection Detail
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" ID="lblpan">Pan-Card No.- </asp:Label>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="javascript:void(0);" CssClass="openModal"></asp:HyperLink>
            </div>
            <!-- Modal Overlay -->
            <div id="myModal" class="modal">
                <div class="modal-content">
                    <span class="close">&times;</span>

                    <h3>Details</h3>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridViewPancard" CssClass="table table-bordered table-striped table-responsive" runat="server" AutoGenerateColumns="false">
                                <HeaderStyle BackColor="#B7E2F0" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo">
                                        <HeaderStyle CssClass="headercolor" />
                                        <ItemStyle />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id" HeaderText="InspectionId">
                                        <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="InstallationType" HeaderText="InstallationType">
                                        <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="District" HeaderText="District">
                                        <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CAPACITY" HeaderText="Capacity">
                                        <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VoltageLevel" HeaderText="VoltageLevel">
                                        <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate">
                                        <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AssignTo" HeaderText="AssignTo">
                                        <HeaderStyle HorizontalAlign="Left" CssClass="headercolor thwidth" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ApplicationStatus" HeaderText="Status">
                                        <HeaderStyle HorizontalAlign="Left" CssClass="headercolor thwidth" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card" style="margin: -11px; padding: 11px; margin-bottom: 20px;">
                <div class="row">
                    <div class="col-md-4" runat="server">
                        <label>Inspection Application No</label>
                        <asp:TextBox class="form-control" ID="txtInspectionReportID" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="PermisesType" visible="true" runat="server">
                        <label>
                            Type of Premises
                        </label>
                        <asp:TextBox class="form-control" ID="txtPremises" ReadOnly="true" autocomplete="off" TabIndex="7" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>
                            Type of Applicant
                        </label>
                        <asp:TextBox class="form-control" ID="txtApplicantType" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>
                            Type of Installation
                        </label>
                        <asp:TextBox class="form-control" ID="txtWorkType" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="Capacity" runat="server">
                        <label for="Capacity">Capacity</label>
                        <asp:TextBox class="form-control" runat="server" ID="txtCapacity" ReadOnly="true" Style="margin-left: 18px"> </asp:TextBox>
                    </div>
                    <div class="col-md-4" id="LineVoltage" runat="server">
                        <label for="Capacity">Voltage</label>
                        <asp:TextBox class="form-control" runat="server" ID="txtLineVoltage" ReadOnly="true" Style="margin-left: 18px"> </asp:TextBox>
                    </div>
                    <div class="col-md-4" runat="server">
                        <label>Voltage Level</label>
                        <asp:TextBox class="form-control" ID="txtVoltage" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="card-title" style="margin-bottom: 5px; font-size: 17px; margin-top: 20px; font-weight: 600; margin-left: -10px; margin-bottom: 20px;">
                Site Owner Details
            </div>
            <div class="card" style="margin: -11px; padding: 11px; margin-bottom: 20px;">
                <div class="row">
                    <div class="col-md-4" runat="server">
                        <label>Owner Name</label>
                        <asp:TextBox class="form-control" ID="txtSiteOwnerName" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-8" id="OwnerAddress" visible="true" runat="server">
                        <label>Address</label>
                        <asp:TextBox class="form-control" ID="txtAddress" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" runat="server">
                        <label>District</label>
                        <asp:TextBox class="form-control" ID="txtDistrict" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="ContractorName" visible="true" runat="server">
                        <label>Contractor Name</label>
                        <asp:TextBox class="form-control" ID="txtContractorName" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="SupervisorName" visible="true" runat="server">
                        <label>Supervisor Name</label>
                        <asp:TextBox class="form-control" ID="txtSupervisorName" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="SiteOwnerContact" visible="true" runat="server">
                        <label>Contact Person Mobile No.</label>
                        <asp:TextBox class="form-control" ID="txtSiteOwnerContact" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="ContractorPhoneNo" visible="true" runat="server">
                        <label>Contractor Phone No.</label>
                        <asp:TextBox class="form-control" ID="txtContractorPhoneNo" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-4" runat="server">
                        <label>Supervisor Contact No.</label>
                        <asp:TextBox class="form-control" ID="txtSupervisorContact" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <%--<div class="col-md-4" id="ContractorEmail" visible="true" runat="server">
               <label>Contractor Email</label>
               <asp:TextBox class="form-control" ID="txtContractorEmail" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
           </div>--%>

                    <%-- <div class="col-md-4" id="SupervisorEmail" visible="true" runat="server">
               <label>Supervisor Email</label>
               <asp:TextBox class="form-control" ID="txtSupervisorEmail" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
           </div>  --%>
                    <div class="col-md-4" id="Div2" runat="server" visible="true">
                        <label>Contact Person Email</label>
                        <asp:TextBox class="form-control" ID="txtContactPersonEmail" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="ContractorEmail" runat="server" visible="true">
                        <label>Contractor Email</label>
                        <asp:TextBox class="form-control" ID="txtContractorEmail" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="SupervisorEmail" visible="true" runat="server">
                        <label>Supervisor Email</label>
                        <asp:TextBox class="form-control" ID="txtSupervisorEmail" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div id="TranscationDetails" runat="server">
                <div class="card-title" style="margin-top: -15px; margin-bottom: 20px; margin-top: 20px; font-size: 17px; font-weight: 600; margin-left: -10px;">
                    Transaction Details
                </div>
                <div class="card" style="box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px; padding: 25px; margin-bottom: 25px; border-radius: 10px; margin-top: 10px;">
                    <div class="row">
                        <div class="col-md-4" runat="server" id="transactionId" visible="true">
                            <label>Transaction ID(GRN Number)</label>
                            <asp:TextBox class="form-control" ID="txtTransactionId" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                        </div>
                        <div class="col-md-4" runat="server" id="transactionDate" visible="true">
                            <label>Transaction Date</label>
                            <asp:TextBox class="form-control" ID="txtTranscationDate" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                        </div>
                        <div class="col-md-4" runat="server">
                            <label>Fees Amount</label>
                            <asp:TextBox class="form-control" ID="txtAmount" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="card-title" style="margin-bottom: 5px; font-size: 17px; margin-bottom: 20px; font-weight: 600; margin-left: -10px;">
                    Documents Attached
                </div>
                <div class="card" style="margin: -11px; padding: 11px; margin-bottom: 20px;">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="grd_Documemnts" CssClass="table table-bordered table-striped table-responsive" runat="server" OnRowCommand="grd_Documemnts_RowCommand" AutoGenerateColumns="false">
                                <HeaderStyle BackColor="#B7E2F0" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo">
                                        <HeaderStyle Width="5%" CssClass="headercolor" />
                                        <ItemStyle Width="5%" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="InstallationType" HeaderText="Installation Type" Visible="false">
                                        <HeaderStyle HorizontalAlign="Left" Width="15%" CssClass="headercolor" />
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DocumentName" HeaderText="Documents Name">
                                        <HeaderStyle HorizontalAlign="Left" Width="15%" CssClass="headercolor" />
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Uploaded Documents" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LnkDocumemtPath" runat="server" CommandArgument='<%# Bind("DocumentPath") %>' CommandName="Select">Click here to view document </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="2%" CssClass="headercolor"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="row" id="TRAttached" runat="server" visible="true">
                    <div class="card-title" style="margin-bottom: 20px; margin-top: 15px; font-size: 17px; font-weight: 600; margin-left: 5px;">
                        Inspection Detail
                    </div>
                </div>
                <div class="card" style="margin: -11px; padding: 11px; margin-bottom: 20px;" id="TRAttachedGrid" runat="server" visible="true">
                    <div class="col-12" style="padding: 0px;">
                        <asp:GridView ID="GridView1" CssClass="table table-bordered table-striped table-responsive" runat="server" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="grd_Documemnts_RowCommand" AutoGenerateColumns="false">
                            <HeaderStyle BackColor="#B7E2F0" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo">
                                    <HeaderStyle CssClass="headercolor" />
                                    <ItemStyle />
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="InstallationType" HeaderText="Installation Type">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <%--                            <asp:BoundField DataField="ActionTaken" HeaderText="ActionTaken">
                       <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                       <ItemStyle HorizontalAlign="Left" />
                   </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="ActionTaken">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblActionTaken" runat="server" Text='<%# Eval("ActionTaken") %>' Visible="false" />
                                        <asp:Label ID="lblInspectionId" runat="server" Text='<%# Eval("Id") %>' Visible="false" />

                                        <!-- If ActionTaken is RETURN, show LinkButton -->
                                        <asp:LinkButton
                                            ID="lnkReturn"
                                            runat="server"
                                            Text="Return"
                                            CommandName="ShowReturnPopup"
                                            CommandArgument='<%# Eval("Id") + "," + Eval("InspectionCount") %>'
                                            OnCommand="lnkReturn_Command"
                                            CssClass="text-danger"
                                            Visible='<%# Eval("ActionTaken").ToString() == "Return" %>' />

                                        <!-- Otherwise, show normal text -->
                                        <asp:Label
                                            ID="lblNormalStatus"
                                            runat="server"
                                            Text='<%# Eval("ActionTaken") %>'
                                            Visible='<%# Eval("ActionTaken").ToString() != "Return" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="TestRportId" HeaderText="TestReportId" Visible="false">
                       <HeaderStyle HorizontalAlign="Left" Width="15%" CssClass="headercolor" />
                       <ItemStyle HorizontalAlign="Left" Width="15%" />
                   </asp:BoundField>
                   <asp:BoundField DataField="TestRportId" HeaderText="TestReportId" Visible="false">
                       <HeaderStyle HorizontalAlign="Left" Width="15%" CssClass="headercolor" />
                       <ItemStyle HorizontalAlign="Left" Width="15%" />
                   </asp:BoundField>
                   <asp:TemplateField HeaderText="View TestReports" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                       <ItemTemplate>
                           <asp:LinkButton ID="lnkRedirect" runat="server" Text="View Test Report" OnClick="lnkRedirect_Click" CommandName="ViewTestReport" CommandArgument='<%# Eval("TestRportId") %>' />
                       </ItemTemplate>
                       <ItemStyle HorizontalAlign="Center" Width="2%" CssClass="headercolor"></ItemStyle>
                       <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                   </asp:TemplateField>--%>
                                <asp:BoundField DataField="ActionDate" HeaderText="ActionDate">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AssignTo" HeaderText="AssignTo">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <%--  <asp:BoundField DataField="ReturnDate" HeaderText="ReturnDate">
                       <HeaderStyle HorizontalAlign="Left" Width="15%" CssClass="headercolor" />
                       <ItemStyle HorizontalAlign="Left" Width="15%" />
                   </asp:BoundField>--%>
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <%--<asp:BoundField DataField="ReturnBased" HeaderText="Return Based">
                       <HeaderStyle HorizontalAlign="Left" Width="15%" CssClass="headercolor" />
                       <ItemStyle HorizontalAlign="Left" Width="15%" />
                   </asp:BoundField>--%>
                            </Columns>
                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="row" id="Div1" runat="server" visible="true">
                    <div class="card-title" style="margin-bottom: 20px; margin-top: 15px; font-size: 17px; font-weight: 600; margin-left: 5px;">
                        Test Report Detail
                    </div>
                </div>
                <div class="card" style="margin: -11px; padding: 11px; margin-bottom: 20px;" id="DivViewCart" runat="server" visible="false">
                    <div class="col-12" style="padding: 0px;">
                        <asp:GridView ID="GridView2" CssClass="table table-bordered table-striped table-responsive" runat="server" AutoGenerateColumns="false">
                            <HeaderStyle BackColor="#B7E2F0" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo">
                                    <HeaderStyle Width="5%" CssClass="headercolor" />
                                    <ItemStyle Width="5%" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="InstallationType" HeaderText="InstallationType">
                                    <HeaderStyle HorizontalAlign="Left" Width="15%" CssClass="headercolor" />
                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TestReportId" HeaderText="TestReportId" Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" Width="15%" CssClass="headercolor" />
                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Id" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="LblInstallationName" runat="server" Text='<%#Eval("InstallationName") %>'></asp:Label>
                                        <asp:Label ID="LblTestReportCount" runat="server" Text='<%#Eval("TestReportCount") %>'></asp:Label>
                                        <asp:Label ID="LblNewInspectionId" runat="server" Text='<%#Eval("NewInspectionId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Voltage" HeaderText="Voltage(In Volts)">
                                    <HeaderStyle HorizontalAlign="Left" Width="15%" CssClass="headercolor" />
                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Capacity" HeaderText="Capacity(In KVA)">
                                    <HeaderStyle HorizontalAlign="Left" Width="15%" CssClass="headercolor" />
                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="View TestReports" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkRedirect1" runat="server" Text="View Test Report" OnClick="lnkRedirect1_Click" CommandName="ViewTestReport" CommandArgument='<%# Eval("TestReportId") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="2%" CssClass="headercolor"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <uc1:InspectionReturnDetails runat="server" ID="InspectionReturnDetails" />
                <div class="row">
                    <div class="col-md-4">
                        <asp:TextBox class="form-control" Visible="false" ID="txtTestReportId" ReadOnly="true" autocomplete="off" TabIndex="7" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                </div>

                <div class="card-title" style="margin-bottom: 5px; font-size: 17px; margin-bottom: 20px; font-weight: 600; margin-left: -10px;">
                    User Undertaking
                </div>
                <div class="card" style="margin: -11px; padding: 11px; padding: 10px 10px 20px 10px !important; margin-bottom: 20px; box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;">
                    <div class="row">
                        <div class="col-md-12 chk-container">
                            <asp:CheckBox ID="CheckBox1" Checked="true" Enabled="false" runat="server" />
                            <label for="CheckBox1">
                                We undertake that we shall maintain and operate all the electrical installations in a condition free from danger and as recommended by the manufacturer and/or by the relevant Code of Practice of the Bureau of Indian Standards.
                            </label>
                        </div>
                    </div>
                </div>

                <%--    <asp:UpdatePanel ID="Updatepanel1" runat="server">
       <ContentTemplate>--%>
                <div class="row">
                    <p style="margin-top: auto; margin-bottom: auto;">
                        Action Required
           <samp style="color: red">* </samp>
                    </p>
                    <asp:RadioButtonList ID="RadioButtonList2" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" TabIndex="25">
                        <asp:ListItem Text="Yes(Accept)" Value="0"></asp:ListItem>
                        <asp:ListItem Text="No(Return)" Value="1" style="margin-top: auto; margin-bottom: auto;"></asp:ListItem>
                        <asp:ListItem Text="Reject" Value="2" style="margin-top: auto; margin-bottom: auto; margin-left: 8px;"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="row" id="Rejection" runat="server" visible="false">
                    <div class="col-md-6">
                        <label>
                            ReasonType:        
                        </label>
                        <asp:DropDownList Style="width: 100% !important;" class="form-control select-form select2" ID="ddlReasonType" TabIndex="8" runat="server" Enabled="false">
                            <asp:ListItem Value="0" Text="Based On TestReport"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Based On Documents" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList Style="width: 100% !important;" class="form-control select-form select2" Visible="false" ID="ddlRejectionReasonType" TabIndex="8" runat="server">
                            <asp:ListItem Value="0" Text="Incorrect Data in WorkIntimation"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-6" id="RejectionRemarksDDL" runat="server" visible="false">
                        <label>
                            Rejection Remarks<samp style="color: red"> * </samp>
                        </label>
                        <asp:DropDownList class="form-control  select-form select2" runat="server" AutoPostBack="true" ID="RejectionDDL" selectionmode="Multiple" Style="width: 100% !important;" OnSelectedIndexChanged="RejectionDDL_SelectedIndexChanged" >
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Work not complete" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Site locked / Entry not provided" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Incorrect site address" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Any other reason" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="RejectionDDL" runat="server" ForeColor="Red" InitialValue="0" ValidationGroup="Submit" ErrorMessage="Required"></asp:RequiredFieldValidator>

                    </div>
                    <div id="Div_RejectionImage" runat="server" visible="false" class="col-md-4">
                        <label>
                            Rejection Image
                        </label>
                        <br />
                        <asp:HyperLink
                            ID="HyperLink_RejectedImg"
                            runat="server"
                            CssClass="myLink"
                            Text="View Uploaded Image"
                            Target="_blank" />
                    </div>
                    <div class="col-md-6" id="RejectionReason" runat="server" visible="false">
                        <label>
                            Reason<samp style="color: red"> * </samp>
                        </label>
                        <asp:TextBox class="form-control" ID="txtRejected" TextMode="MultiLine" Rows="2" MaxLength="200" autocomplete="off" TabIndex="7" runat="server" Style="margin-left: 18px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator60" ControlToValidate="txtRejected" runat="server" ForeColor="Red" ValidationGroup="Submit" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <%--     </ContentTemplate>
   </asp:UpdatePanel>--%>
            </div>
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-4" style="text-align: center;">
                    <asp:Button ID="btnSubmit" Text="Submit" runat="server" class="btn btn-primary mr-2" ValidationGroup="Submit" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnBack" Text="Back" runat="server" class="btn btn-primary mr-2" OnClick="btnBack_Click" />
                </div>
            </div>
        </div>


    </div>
    <script src="/Assets/js/js/vendor.bundle.base.js"></script>
    <script>
        document.addEventListener("DOMContentLoaded", function () {

            var modal = document.getElementById("myModal");
            var openBtn = document.querySelector(".openModal");
            var closeBtn = document.querySelector(".close");

            // Open modal on HyperLink click
            openBtn.addEventListener("click", function () {
                modal.style.display = "block";
            });

            // Close modal on X click
            closeBtn.addEventListener("click", function () {
                modal.style.display = "none";
            });

            // Close modal on outside click
            window.onclick = function (event) {
                if (event.target === modal) {
                    modal.style.display = "none";
                }
            };

        });
    </script>
</asp:Content>
