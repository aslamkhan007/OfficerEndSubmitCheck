<%@ Page Title="" Language="C#" MasterPageFile="~/Officers/Officers.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="InProcessInspection.aspx.cs" Inherits="CEIHaryana.Officers.InProcessInspection" %>

<%@ Register Src="~/UserCPages/InspectionReturnDetails.ascx" TagPrefix="uc1" TagName="InspectionReturnDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="shortcut icon" type="image/png" href="/css2/style.min.css" />
    <link rel="stylesheet" href="/css2/style.css" />
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css" />
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
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
    <!-- Bootstrap CSS -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />

    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>

    <!-- Bootstrap JavaScript Bundle -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>


    <style>
        th.headercolor {
            color: white !important;
            width: 1%;
        }

        .multiselect {
            width: 100%;
        }

        .selectBox {
            position: relative;
        }

            .selectBox select {
                width: 100%;
            }

        .overSelect {
            position: absolute;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
        }

        #mySelectOptions {
            display: none;
            border: 0.5px #7c7c7c solid;
            background-color: #ffffff;
            max-height: 150px;
            overflow-y: scroll;
        }

            #mySelectOptions label {
                display: block;
                font-weight: normal;
                display: block;
                white-space: nowrap;
                min-height: 1.2em;
                background-color: #ffffff00;
                padding: 0 2.25rem 0 .75rem;
                /* padding: .375rem 2.25rem .375rem .75rem; */
            }

                #mySelectOptions label:hover {
                    background-color: #1e90ff;
                }

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

        th.headercolor {
            background: #9292cc;
            color: white;
        }

        select#ContentPlaceHolder1_ddlSuggestion {
            display: block;
            width: 90%;
            padding-left: 12px;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #212529;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            border-radius: .25rem;
            transition: border-color .15s ease-in-out, box-shadow .15s ease-in-out;
            height: 30px !important;
        }

        textarea#ContentPlaceHolder1_txtSuggestion {
            display: block;
            width: 100%;
            padding-left: 12px;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #212529;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            border-radius: .25rem;
            transition: border-color .15s ease-in-out, box-shadow .15s ease-in-out;
            height: 100px !important;
        }

        .modal1 {
            display: none; /* Hidden by default */
            position: fixed;
            z-index: 1;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgb(0, 0, 0);
            background-color: rgba(0, 0, 0, 0.4);
        }

        .modal-content {
            background-color: #fefefe;
            margin: 15% auto;
            padding: 20px;
            border: 1px solid #888;
            width: 80%;
        }

        input#ContentPlaceHolder1_BtnAddSuggestion {
            padding-top: 2px;
            padding-bottom: 2px;
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

        .close:hover {
            color: #ff3b30;
            transform: scale(1.2);
        }

        input#ContentPlaceHolder1_RejectionFileUpload {
            padding-top: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="card-body" style="box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px; padding: 25px; margin-bottom: 25px; border-radius: 10px; margin-top: 10px;">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <div class="card-title" style="margin-top: -15px; margin-bottom: 20px; font-size: 17px; font-weight: 600; margin-left: -10px;">
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
                    <div class="col-md-4" id="InspectionType" runat="server" visible="true">
                        <label>Type of Premises</label>
                        <asp:TextBox class="form-control" ID="txtPremises" ReadOnly="true" autocomplete="off" TabIndex="7" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>Type of Applicant</label>
                        <asp:TextBox class="form-control" ID="txtApplicantType" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>Type of Installation</label>
                        <asp:TextBox class="form-control" ID="txtWorkType" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="Capacity" runat="server">
                        <label for="Capacity">Capacity</label>
                        <asp:TextBox class="form-control" runat="server" ID="txtCapacity" ReadOnly="true" Style="margin-left: 18px"> </asp:TextBox>
                    </div>
                    <div class="col-md-4" id="LineVoltage" runat="server" visible="true">
                        <label for="Capacity">Voltage</label>
                        <asp:TextBox class="form-control" runat="server" ID="txtLineVoltage" ReadOnly="true" Style="margin-left: 18px"> </asp:TextBox>
                    </div>
                    <div class="col-md-4" runat="server">
                        <label>Voltage Level</label>
                        <asp:TextBox class="form-control" ID="txtVoltage" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>

                    <%-- <div class="col-md-4" runat="server" visible="true">
                        <label>Fees Amount</label>
                        <asp:TextBox class="form-control" ID="txtAmount" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>--%>
                </div>
            </div>
            <div class="card-title" style="margin-top: -15px; margin-bottom: 20px; margin-top: 20px; font-size: 17px; font-weight: 600; margin-left: -10px;">
                Site Owner Details
            </div>
            <div class="card" style="margin: -11px; padding: 11px; margin-bottom: 20px;">
                <div class="row">
                    <div class="col-md-4" runat="server">
                        <label>Owner Name</label>
                        <asp:TextBox class="form-control" ID="txtSiteOwnerName" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-8" id="Address" runat="server" visible="true">
                        <label>Address</label>
                        <asp:TextBox class="form-control" ID="txtAddress" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>
                            District
                        </label>
                        <asp:TextBox class="form-control" ID="txtOwnerDistrict" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="ContractorName" runat="server" visible="true">
                        <label>Contractor Name</label>
                        <asp:TextBox class="form-control" ID="txtContractorName" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="divSupervisorName" runat="server" visible="true">
                        <label>Supervisor Name</label>
                        <asp:TextBox class="form-control" ID="txtSupervisorName" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="SiteOwnerContact" runat="server" visible="true">
                        <label>Contact Person Mobile No.</label>
                        <asp:TextBox class="form-control" ID="txtSiteOwnerContact" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="ContractorPhoneNo" runat="server" visible="true">
                        <label>Contractor Phone No.</label>
                        <asp:TextBox class="form-control" ID="txtContractorPhoneNo" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="Div1" runat="server" visible="true">
                        <label>Supervisor Phone No.</label>
                        <asp:TextBox class="form-control" ID="txtSupervisorMobile" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="Div2" runat="server" visible="true">
                        <label>Contact Person Email</label>
                        <asp:TextBox class="form-control" ID="txtContactPersonEmail" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="ContractorEmail" runat="server" visible="true">
                        <label>Contractor Email</label>
                        <asp:TextBox class="form-control" ID="txtContractorEmail" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="SupervisorEmail" runat="server" visible="true">
                        <label>Supervisor Email</label>
                        <asp:TextBox class="form-control" ID="txtSupervisorEmail" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>


                    <%--<div class="col-md-4" id="Inspection_Type" runat="server" visible="false">
                        <label>Inspection Type</label>
                        <asp:TextBox class="form-control" ID="TxtInspection" ReadOnly="true" autocomplete="off" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>--%>
                </div>
            </div>
            <div class="card-title" style="margin-bottom: 5px; font-size: 17px; margin-top: 20px; font-weight: 600; margin-left: -10px; margin-bottom: 20px;">
                Transaction Details
            </div>
            <div class="card" style="margin: -11px; padding: 11px; margin-bottom: 20px; box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;">
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

            <div class="card-title" style="margin-bottom: 5px; font-size: 17px; font-weight: 600; margin-left: -10px;">
                Documents Attached
            </div>
            <div class="row card" style="padding-top: 10px;">
                <div class="col-12">
                    <asp:GridView ID="grd_Documemnts" CssClass="table table-bordered table-striped table-responsive" runat="server" OnRowCommand="grd_Documemnts_RowCommand" OnRowDataBound="grd_Documemnts_RowDataBound" AutoGenerateColumns="false">
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
                            <asp:TemplateField HeaderText="Current Documents" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkDocumemtPath" runat="server" CommandArgument='<%# Bind("CurrentDocumentPath") %>' CommandName="Select">Click here to view document </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="2%" CssClass="headercolor"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Previous Documents" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkDocumemtPath2" runat="server" Text='<%# Bind("PreviousDocumentPath") %>' CommandArgument='<%# Bind("PreviousDocumentPath") %>' CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="2%" CssClass="headercolor"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="DocumentRemarks" HeaderText="Returned Reason" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" Width="15%" CssClass="headercolor" />
                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                            </asp:BoundField>
                        </Columns>
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
                    </asp:GridView>
                </div>
            </div>
            <div class="row" id="divTestReportAttachment" runat="server" visible="true">
                <div class="card-title" style="margin-bottom: 5px; margin-top: 15px; font-size: 17px; font-weight: 600; margin-left: -10px;">
                    Inspection Details
                </div>
                <div class="card" style="padding-top: 10px; margin-left: 0px !important;">


                    <asp:GridView ID="GridView1" CssClass="table table-bordered table-striped table-responsive" runat="server" OnRowCommand="grd_Documemnts_RowCommand" AutoGenerateColumns="false">
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

                            <%-- <asp:TemplateField HeaderText="View TestReports" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" Visible="false">
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
                            <%-- <asp:BoundField DataField="ReturnBased" HeaderText="Return Based">
                                <HeaderStyle HorizontalAlign="Left" Width="15%" CssClass="headercolor" />
                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="Id" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubmittedDate" runat="server" Text='<%#Eval("ActionDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
                    </asp:GridView>


                </div>

            </div>
            <div class="card-title" style="margin-bottom: 5px; margin-top: 15px; font-size: 17px; font-weight: 600; margin-left: -10px;">
                Test Report Detail
            </div>
            <div class="card" style="margin: -11px; margin-top: 15px; padding: 11px; margin-bottom: 20px;" id="DivTestReports" runat="server" visible="false">
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

            <div class="card" style="margin-top: 15px; padding: 11px; margin-bottom: 20px; margin-left: -15px; margin-right: -15px;" id="DivViewTRinMultipleCaseNew" runat="server" visible="false">
                <asp:GridView ID="Grid_MultipleInspectionTR" CssClass="table table-bordered table-striped table-responsive" OnRowDataBound="Grid_MultipleInspectionTR_RowDataBound" OnRowCommand="Grid_MultipleInspectionTR_RowCommand" runat="server" AutoGenerateColumns="false">
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
                                <asp:Label ID="LblInstallationName" runat="server" Text='<%#Eval("Typeofinstallation") %>'></asp:Label>
                                <asp:Label ID="LblTestReportCount" runat="server" Text='<%#Eval("Count") %>'></asp:Label>
                                <asp:Label ID="LblNewInspectionId" runat="server" Text='<%#Eval("InspectionId") %>'></asp:Label>
                                <asp:Label ID="LblIntimationId" runat="server" Text='<%#Eval("IntimationId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View Test Report" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkRedirectTR" runat="server" Text="View Test Report" OnClick="lnkRedirectTR_Click1" CommandName="Select" CommandArgument='<%# Eval("TestReportId") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="2%" CssClass="headercolor"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Previous Installaion Invoice" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" Visible="false">
                            <ItemTemplate>
                                <%--<asp:LinkButton ID="lnkPreviousInstallaionInvoice" runat="server" Text='<%# Eval("PrevinstallaionInvoice") %>' CommandName="ViewInvoice" CommandArgument='<%# Eval("PrevinstallaionInvoice") %>' />--%>
                                <asp:LinkButton ID="lnkPreviousInstallaionInvoice" runat="server" Text="View Document" CommandName="ViewInvoice" CommandArgument='<%# Eval("PrevinstallaionInvoice") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="2%" CssClass="headercolor"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Installaion Invoice" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkInstallaionInvoice" runat="server" Text="View Document" CommandName="ViewInvoice" CommandArgument='<%# Eval("installaionInvoice") %>' Visible='<%# Eval("installaionInvoice").ToString() != "" %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="2%" CssClass="headercolor"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Previous Manufacturing Report" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" Visible="false">
                            <ItemTemplate>
                                <%--<asp:LinkButton ID="lnkPreviosManufacturingReport" runat="server" Text='<%# Eval("PrevManufacturingReport") %>' CommandName="View" CommandArgument='<%# Eval("PrevManufacturingReport") %>' />--%>
                                <asp:LinkButton ID="lnkPreviosManufacturingReport" runat="server" Text="View Document" CommandName="View" CommandArgument='<%# Eval("PrevManufacturingReport") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="2%" CssClass="headercolor"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Manufacturing Report" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkManufacturingReport" runat="server" Text="View Document" CommandName="View" CommandArgument='<%# Eval("ManufacturingReport") %>' Visible='<%# Eval("installaionInvoice").ToString() != "" %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="2%" CssClass="headercolor"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" CssClass="headercolor" />
                        </asp:TemplateField>
                        <%--  <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                            <HeaderStyle HorizontalAlign="Left" Width="15%" CssClass="headercolor" />
                            <ItemStyle HorizontalAlign="Left" Width="15%" />
                        </asp:BoundField>--%>
                    </Columns>
                </asp:GridView>
                <div class="col-12" style="padding: 0px;">
                </div>
            </div>

            <%-- <div class="row" style="margin-bottom: 30px;">
                <div class="col-12" style="text-align: center">
                    <asp:LinkButton ID="lnkRedirect" runat="server" AutoPostBack="true" OnClick="lnkRedirect_Click" Text="View Test Report" />
                </div>
            </div>--%>
            <div class="col-md-4">
                <asp:TextBox class="form-control" Visible="false" ID="txtTestReportId" ReadOnly="true" autocomplete="off" TabIndex="7" runat="server" Style="margin-left: 18px"></asp:TextBox>
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

            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <div class="row">

                <div class="col-md-4" id="ApprovalRequired" runat="server" visible="true">
                    <label>
                        Approval<samp style="color: red"> * </samp>
                    </label>
                    <asp:DropDownList class="form-control  select-form select2" runat="server" AutoPostBack="true" ID="ddlReview" selectionmode="Multiple" Style="width: 100% !important;" OnSelectedIndexChanged="ddlReview_SelectedIndexChanged">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Rejected" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator57" ControlToValidate="ddlReview" runat="server" ForeColor="Red" InitialValue="0" ValidationGroup="Submit" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4" id="RejectionBasis" runat="server" visible="false">
                    <label>
                        Rejection Based On<samp style="color: red"> * </samp>
                    </label>
                    <asp:TextBox class="form-control" ID="txtRejectionBasis" autocomplete="off" TabIndex="7" runat="server" Style="margin-left: 18px"></asp:TextBox>

                </div>
                <div class="col-md-4" id="InspectionDate" runat="server">
                    <label for="StartDate">
                        Inspection Date<samp style="color: red"> * </samp>
                    </label>
                    <asp:TextBox class="form-control" ID="txtInspectionDate" TabIndex="16" autocomplete="off" Type="Date" min='0000-01-01' max='9999-01-01' runat="server" Style="margin-left: 18px"></asp:TextBox>

                </div>
                <div class="row">
                    <div class="col-12" id="ExNote" runat="server" visible="false" style="margin-bottom: 25px;">
                        <label>
                            Note
                        </label>
                        <asp:TextBox class="form-control" ID="txtNote" autocomplete="off" TabIndex="7" MaxLength="500" TextMode="MultiLine" Rows="2" runat="server" Style="margin-left: 18px"></asp:TextBox>

                    </div>
                </div>
                <div class="row">
                    <div class="col-6" id="RejectionRemarksDDL" runat="server" visible="false">
                        <label>
                            Rejection Remarks<samp style="color: red"> * </samp>
                        </label>
                        <asp:DropDownList class="form-control  select-form select2" runat="server" AutoPostBack="true" ID="RejectionDDL" selectionmode="Multiple" Style="width: 100% !important;" OnSelectedIndexChanged="RejectionDDL_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Work not complete" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Site locked / Entry not provided" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Incorrect site address" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Any other reason" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="RejectionDDL" runat="server" ForeColor="Red" InitialValue="0" ValidationGroup="Submit" ErrorMessage="Required"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-6" id="RejectionFileDiv" runat="server" visible="false">
                        <label>
                            Upload Image<samp style="color: red"> * </samp>
                        </label>

                        <asp:FileUpload
                            ID="RejectionFileUpload"
                            runat="server"
                            CssClass="form-control" />

                        <asp:RequiredFieldValidator
                            ID="RequiredFieldValidatorFile"
                            ControlToValidate="RejectionFileUpload" accept=".jpg,.jpeg,.png"
                            runat="server"
                            ForeColor="Red"
                            ValidationGroup="Submit"
                            ErrorMessage="File required">
                        </asp:RequiredFieldValidator>
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
                    <div class="col-12" id="Rejection" runat="server" visible="false">
                        <label>
                            Reason<samp style="color: red"> * </samp>
                        </label>
                        <asp:TextBox class="form-control" ID="txtRejected" autocomplete="off" TabIndex="7" runat="server" Style="margin-left: 18px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator60" ControlToValidate="txtRejected" runat="server" ForeColor="Red" ValidationGroup="Submit" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row" id="ddlSuggestions" visible="false" runat="server">
                    <div class="col-6">


                        <asp:DropDownList ID="ddlSuggestion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSuggestion_SelectedIndexChanged" Style="width: 100%;">
                        </asp:DropDownList>

                    </div>
                    <div class="col-6">
                        <asp:Button ID="BtnAddSuggestion" Text="Add Suggestion" runat="server" class="btn btn-primary mr-2" OnClientClick="$('#modal1').modal('show'); return false;" />
                    </div>
                </div>
                <div id="Note" runat="server" visible="false">
                    <label>
                        <span style="color: red;">NOTE:</span>&nbsp;YOU CAN NOT GIVE MORE THAN 4 SUGGESTIONS.   
                    </label>


                </div>


                <div class="row" style="margin-top: 25px;">

                    <div class="col-12" id="Suggestion" runat="server" visible="false">

                        <asp:TextBox class="form-control" ID="txtSuggestion" TextMode="MultiLine" Rows="2" MaxLength="1000" autocomplete="off" TabIndex="7" runat="server" Style="margin-left: 18px"></asp:TextBox>
                    </div>
                </div>




            </div>
        </div>
        <uc1:InspectionReturnDetails runat="server" ID="InspectionReturnDetails" />
        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-6" style="text-align: center;">
                <asp:Button ID="btnSuggestions" Text="Save Suggestions" runat="server" Visible="false" class="btn btn-primary mr-2" OnClick="btnSuggestions_Click" />
                <asp:Button ID="btnPreview" Text="Preview" runat="server" Visible="false" class="btn btn-primary mr-2" OnClick="btnPreview_Click" />
                <asp:Button ID="btnSubmit" Text="Submit" runat="server" class="btn btn-primary mr-2" ValidationGroup="Submit" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnBack" Text="Back" runat="server" class="btn btn-primary mr-2" OnClick="btnBack_Click" />
                <asp:Button ID="Backbtn" Text="Back" runat="server" class="btn btn-primary mr-2" OnClick="Backbtn_Click" Visible="false" />
            </div>
        </div>
    </div>
    <%--  <div id="modal1" class="modal" runat="server" visible="false">
    <div class="modal-content">
        <span class="close" data-modal="modal1" >&times;</span>
        <div class="card-title">
            Suggestion
        </div>
        <div>
            <asp:TextBox class="form-control" ID="txtSugg" TextMode="MultiLine" Rows="2" MaxLength="1000" autocomplete="off" TabIndex="7" runat="server" Style="margin-left: 18px"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="BtnSugg" Text="Submit" runat="server" CssClass="btn btn-primary mr-2" OnClick="BtnSugg_Click"/>
        </div>
    </div>
</div>--%>

    <div class="modal fade" id="modal1" tabindex="-1" role="dialog" aria-labelledby="updatePasswordModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="updatePasswordModalLabel">Suggestion</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <label>
                                Suggestion<samp style="color: red"> * </samp>
                            </label>
                            <asp:TextBox class="form-control" ID="txtSugg" runat="server" Style="margin-left: 18px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSugg" runat="server" CssClass="btn btn-primary" Text="Add Suggestion" OnClick="btnSugg_Click" />
                    <button type="button" class="btn btn-secondary" onclick="closeModal()">Close</button>
                </div>
            </div>
        </div>
    </div>


    <%--   </ContentTemplate>
            </asp:UpdatePanel>--%>

    <script type="text/javascript">
        function alertWithRedirectdata(Message) {
            if (confirm('Inspection Request has been Successfully ' + Message)) {
                window.location.href = "/Officers/InProcessRequest.aspx";
            } else {
            }
        }
    </script>
    <script>
        function closeModal() {
            $('#modal1').modal('hide');
        }
    </script>
    <%-- <script>
    $(document).ready(function () {
    // Reinitialize modal after postback if it's supposed to be open
    var modalOpen = '<%= txtSugg.ClientID %>';
    if (modalOpen) {
        $('#modal1').modal('show');
    }
});
    </script>--%>
    <script type="text/javascript">
        window.onload = (event) => {
            initMultiselect();
        };

        function initMultiselect() {
            checkboxStatusChange();

            document.addEventListener("click", function (evt) {
                var flyoutElement = document.getElementById('myMultiselect'),
                    targetElement = evt.target; // clicked element

                do {
                    if (targetElement == flyoutElement) {
                        // This is a click inside. Do nothing, just return.
                        //console.log('click inside');
                        return;
                    }

                    // Go up the DOM
                    targetElement = targetElement.parentNode;
                } while (targetElement);

                // This is a click outside.
                toggleCheckboxArea(true);
                //console.log('click outside');
            });
        }

        //function checkboxStatusChange() {375592978
        //    var multiselect = document.getElementById("mySelectLabel");
        //    var multiselectOption = multiselect.getElementsByTagName('option')[0];

        //    var values = [];
        //    var checkboxes = document.getElementById("mySelectOptions");
        //    var checkedCheckboxes = checkboxes.querySelectorAll('input[type=checkbox]:checked');

        //    for (const item of checkedCheckboxes) {
        //        var checkboxValue = item.getAttribute('value');
        //        values.push(checkboxValue);
        //    }

        //    var dropdownValue = "Nothing is selected";
        //    if (values.length > 0) {
        //        dropdownValue = values.join(', ');
        //    }

        //    multiselectOption.innerText = dropdownValue;
        //}

        function toggleCheckboxArea(onlyHide = false) {
            var checkboxes = document.getElementById("mySelectOptions");
            var displayValue = checkboxes.style.display;

            if (displayValue != "block") {
                if (onlyHide == false) {
                    checkboxes.style.display = "block";
                }
            } else {
                checkboxes.style.display = "none";
            }
        }
    </script>
    <%--<script>
        $(document).ready(function () {
            $('#modal1').modal('show');
        });
</script>--%>
    <script type="text/javascript">
        window.onload = function () {
            var today = new Date().toISOString().split('T')[0];
            document.getElementById('<%= txtInspectionDate.ClientID %>').setAttribute('max', today);
        };
    </script>
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
