using CEI_PRoject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CEIHaryana.Model.Industry;
using System.Data.SqlClient;

namespace CEIHaryana.Admin
{
    public partial class IntimationForHistory : System.Web.UI.Page
    {
        CEI CEI = new CEI();
        string Id, StaffTo, AssignFrom;
        string Type = string.Empty;

        private static int count;
        private static string IntimationId, AcceptorReturn, Reason, StaffId;
        string InstallType = string.Empty;
        IndustryApiLogDetails logDetails = new IndustryApiLogDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Convert.ToString(Session["AdminId"]) != null && Convert.ToString(Session["AdminId"]) != string.Empty)
                    {
                        if (Convert.ToString(Session["InspectionId"]) != null && Convert.ToString(Session["InspectionId"]) != string.Empty)
                        {
                            ////if (Session["LineID"] != null && Convert.ToString(Session["LineID"]) != "")
                            ////{
                            ////    txtWorkType.Text = "Line";
                            ////    Id = Session["LineID"].ToString();
                            ////}
                            ////else if (Session["SubStationID"] != null && Convert.ToString(Session["LineID"]) != "")
                            ////{
                            ////    txtWorkType.Text = "Substation Transformer";
                            ////    Id = Session["SubStationID"].ToString();
                            ////}
                            ////else if (Session["GeneratingSetId"] != null && Convert.ToString(Session["LineID"]) != "")
                            ////{
                            ////    txtWorkType.Text = "Generating Station";
                            ////    Id = Session["GeneratingSetId"].ToString();
                            ////}
                            ////else if (Session["PeriodicMultiple"] != null && Convert.ToString(Session["PeriodicMultiple"]) != "")
                            ////{
                            ////    txtWorkType.Text = "Multiple";
                            ////    Id = Session["PeriodicMultiple"].ToString();
                            ////}
                            string InspectionId = Convert.ToString(Session["InspectionId"]);
                            GetDetailsWithId(InspectionId);
                            GetInspectionHistoryandPan(InspectionId);
                            if (Type == "New")
                            {
                                GetTestReportData(InspectionId);
                            }
                            else if (Type == "Periodic")
                            {
                                //TRAttachedGrid.Visible = false;
                                GetTestReportDataIfPeriodic(InspectionId);
                            }
                            //GetTestReportData();
                        }
                        else
                        {
                            Session["InspectionId"] = "";
                            Response.Redirect("/Admin/IntimationHistoryForAdmin.aspx", false);
                        }
                    }
                    else
                    {
                        Session["AdminId"] = "";
                        Response.Redirect("/AdminLogout.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("/AdminLogout.aspx", false);
            }
        }
        #region navneet PanHistory on click 10/12/2025
        private void GetInspectionHistoryandPan(string ID)
        {
            try
            {
                //string ID = Session["InspectionId"].ToString();
                DataTable dt = CEI.GetInspectionHistoryandPan(ID);
                if (dt != null && dt.Rows.Count > 0)
                {

                    GridViewPancard.DataSource = dt;
                    GridViewPancard.DataBind();
                }
                else
                {
                    lblpan.Visible = false;
                    HyperLink1.Visible = false;
                    GridViewPancard.DataSource = null;
                    GridViewPancard.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session["AdminId"] = "";
                Response.Redirect("/AdminLogout.aspx", false);
            }
        }
        #endregion
        private void GetTestReportDataIfPeriodic(string ID)
        {
            try
            {
                //ID = Session["InspectionId"].ToString();
                DataSet ds = new DataSet();
                //commented Condition only by gurmeet 1May
                //ds = CEI.GetTestReportDataIfPeriodic(ID);
                ds = CEI.GetInspectionHistoryLogs(ID);
                string TestRportId = string.Empty;
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //if (ds != null && ds.Tables.Count > 0)
                {
                    //Added if and else Condition by neha 9-May-2025
                    LblGridView1.Visible = false;
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    LblGridView1.Visible = true;
                    LblGridView1.Text = "NA";
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    //string script = "alert(\"No Record Found\");";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                ds.Dispose();
            }
            catch (Exception ex)
            {
                Session["AdminId"] = "";
                Response.Redirect("/AdminLogout.aspx", false);
            }
        }
        //Uncommented by neha 8-may
        private void BindDivisions(string District)
        {
            DataSet ds = new DataSet();
            ds = CEI.GetDivisionData(District);
            ddlDivisions.DataSource = ds;
            ddlDivisions.DataTextField = "District";
            ddlDivisions.DataValueField = "District";
            ddlDivisions.DataBind();
            ddlDivisions.Items.Insert(0, new ListItem("Select", "0"));
            ds.Clear();
        }
        //
        private void GetDetailsWithId(string ID)
        {
            try
            {
               //// ID = Session["InspectionId"].ToString();
                DataSet ds = new DataSet();
                ds = CEI.InspectionData(ID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //Added if and else Condition by neha 9-May-2025
                    Type = ds.Tables[0].Rows[0]["IType"].ToString();
                    HyperLink1.Text = ds.Tables[0].Rows[0]["WhoCreated"].ToString();
                    lbltype.Text = ds.Tables[0].Rows[0]["IType"].ToString();
                    Session["Type"] = Type;
                    if (Type == "New")
                    {
                        txtInspectionReportId.Text = ds.Tables[0].Rows[0]["Id"].ToString();
                        txtDistrict.Text = ds.Tables[0].Rows[0]["District"].ToString();
                        // txtPremises.Text = ds.Tables[0].Rows[0]["Inspectiontype"].ToString();
                        //start added by gurmeet 18 july-2025
                        string Premises = ds.Tables[0].Rows[0]["Inspectiontype"].ToString();
                        if (!string.IsNullOrEmpty(Premises))
                        {
                            txtPremises.Text = Premises;
                        }
                        else
                        {
                            txtPremises.Text = "     -";
                        }
                        txtContactPersonContact.Text = ds.Tables[0].Rows[0]["SiteownerContactNumber"].ToString();
                        txtSiteAddress.Text = ds.Tables[0].Rows[0]["SiteownerAddress"].ToString();
                        txtSupervisorContact.Text = ds.Tables[0].Rows[0]["SupervisorPhoneNo"].ToString();
                        txtcontractorContact.Text = ds.Tables[0].Rows[0]["ContractorContactNo"].ToString();
                        txtContractorEmail.Text = ds.Tables[0].Rows[0]["ContractorEmail"].ToString();
                        txtContactPersonEmail.Text = ds.Tables[0].Rows[0]["SiteownerEmail"].ToString();
                        txtSupervisorEmail.Text = ds.Tables[0].Rows[0]["SupervisorEmail"].ToString();
                        //** end added by gurmeet 17 july-2025
                        txtApplicantType.Text = ds.Tables[0].Rows[0]["ApplicantType"].ToString();
                        txtWorkType.Text = ds.Tables[0].Rows[0]["InstallationType"].ToString();
                        if (txtWorkType.Text == "Line")
                        {
                            Capacity.Visible = false;
                            LineVoltage.Visible = true;
                            txtLineVoltage.Text = ds.Tables[0].Rows[0]["Capacity"].ToString();
                        }
                        else
                        {
                            LineVoltage.Visible = false;
                            Capacity.Visible = true;
                            txtCapacity.Text = ds.Tables[0].Rows[0]["Capacity"].ToString();
                        }
                        txtVoltage.Text = ds.Tables[0].Rows[0]["VoltageLevel"].ToString();
                        ////txtTestReportId.Text = ds.Tables[0].Rows[0]["TestRportId"].ToString();
                        txtSiteOwnerName.Text = ds.Tables[0].Rows[0]["OwnerName"].ToString();
                        txtContractorName.Text = ds.Tables[0].Rows[0]["ContractorName"].ToString();
                        txtSupervisorName.Text = ds.Tables[0].Rows[0]["SupervisorName"].ToString();
                        //txtTransactionId.Text = ds.Tables[0].Rows[0]["TransactionId"].ToString();
                        //txtTranscationDate.Text = ds.Tables[0].Rows[0]["TransactionDate1"].ToString();
                        txtAmount.Text = ds.Tables[0].Rows[0]["TotalAmount"].ToString();
                        #region Added 05-09-2025 Neha
                        if (txtAmount.Text == "0")
                        {
                            transactionId.Visible = false;
                            transactionDate.Visible = false;
                        }
                        else
                        {
                            transactionId.Visible = true;
                            transactionDate.Visible = true;
                            txtTransactionId.Text = ds.Tables[0].Rows[0]["TransactionId"].ToString();
                            txtTranscationDate.Text = ds.Tables[0].Rows[0]["TransactionDate1"].ToString();
                        }
                        #endregion

                        // count = Convert.ToInt32(ds.Tables[0].Rows[0]["TestReportCount"].ToString());           //Added     
                        IntimationId = ds.Tables[0].Rows[0]["IntimationId"].ToString();
                        DivTRinMultipleCaseNew.Visible = true;

                        GetGridNewInspectionMultiple(ID);
                        GridBindDocument(ID);
                        //Uncommented by neha 8-may
                        BindDivisions(txtDistrict.Text.Trim());
                        string Status = ds.Tables[0].Rows[0]["ApplicationStatus"].ToString();
                        if (Status.Trim() == "InProcess")
                        {
                            RdbtnAccptReturn.SelectedIndex = RdbtnAccptReturn.Items.IndexOf(RdbtnAccptReturn.Items.FindByValue("0"));
                            RdbtnAccptReturn.Attributes.Add("disabled", "true");
                            RdbtnAccptReturn.Enabled = false;
                            btnBack.Visible = true;
                            btnUpdate.Visible = false;
                        }
                        else if (Status.Trim() == "Return")
                        {
                            RdbtnAccptReturn.SelectedIndex = RdbtnAccptReturn.Items.IndexOf(RdbtnAccptReturn.Items.FindByValue("1"));
                            RdbtnAccptReturn.Attributes.Add("disabled", "true");
                            RdbtnAccptReturn.Enabled = false;
                            Return.Visible = true;
                            txtRejected.Text = ds.Tables[0].Rows[0]["ReturnRemarks"].ToString();
                            txtRejected.Attributes.Add("disabled", "true");
                            btnBack.Visible = true;
                            btnUpdate.Visible = false;                         
                        }
                        else if (Status.Trim() == "Approved" || Status.Trim() == "Rejected")
                        {
                            RdbtnAccptReturn.Enabled = false;
                            txtRejected.Attributes.Add("disabled", "true");
                            btnBack.Visible = true;
                            btnUpdate.Visible = false;
                            RejectionDDL.Visible = true;
                            RejectionRemarksDDL.Visible = true;
                            string RejectedImagPath = ds.Tables[0].Rows[0]["RejectionImage"].ToString();
                            if (!string.IsNullOrEmpty(RejectedImagPath))
                            {
                                Div_RejectionImage.Visible = true;
                                HyperLink_RejectedImg.NavigateUrl = ResolveUrl(RejectedImagPath); //open img
                            }
                            string TypeOfRejection = ds.Tables[0].Rows[0]["TypeOfRejection"].ToString();
                            RejectionDDL.SelectedIndex = RejectionDDL.Items.IndexOf(RejectionDDL.Items.FindByText(TypeOfRejection));
                            RejectionDDL.Attributes.Add("disabled", "true");
                        }
                        ddlReasonType.Visible = true;
                    }
                    else if (Type == "Periodic")
                    {
                        txtInspectionReportId.Text = ds.Tables[0].Rows[0]["Id"].ToString();
                        txtDistrict.Text = ds.Tables[0].Rows[0]["District"].ToString();
                        txtApplicantType.Text = ds.Tables[0].Rows[0]["ApplicantType"].ToString();
                        txtWorkType.Text = ds.Tables[0].Rows[0]["InstallationType"].ToString();
                        txtCapacity.Text = ds.Tables[0].Rows[0]["Capacity"].ToString();
                        txtVoltage.Text = ds.Tables[0].Rows[0]["VoltageLevel"].ToString();
                        ////txtTestReportId.Text = ds.Tables[0].Rows[0]["TestRportId"].ToString();
                        txtSiteOwnerName.Text = ds.Tables[0].Rows[0]["OwnerName"].ToString();
                        ContractorName.Visible = false;
                        SupervisorName.Visible = false;
                        LineVoltage.Visible = false;
                        //txtTransactionId.Text = ds.Tables[0].Rows[0]["TransactionId"].ToString();
                        //txtTranscationDate.Text = ds.Tables[0].Rows[0]["TransactionDate1"].ToString();
                        txtAmount.Text = ds.Tables[0].Rows[0]["TotalAmount"].ToString();
                        if (txtAmount.Text == "0")
                        {
                            transactionId.Visible = false;
                            transactionDate.Visible = false;
                        }
                        else
                        {
                            transactionId.Visible = true;
                            transactionDate.Visible = true;
                            txtTransactionId.Text = ds.Tables[0].Rows[0]["TransactionId"].ToString();
                            txtTranscationDate.Text = ds.Tables[0].Rows[0]["TransactionDate1"].ToString();
                        }
                        //start added by gurmeet 18 july-2025
                        string Premises = ds.Tables[0].Rows[0]["Inspectiontype"].ToString();
                        if (!string.IsNullOrEmpty(Premises))
                        {
                            txtPremises.Text = Premises;
                        }
                        else
                        {
                            txtPremises.Text = "     -";
                        }
                        txtContactPersonContact.Text = ds.Tables[0].Rows[0]["SiteownerContactNumber"].ToString();
                        txtSiteAddress.Text = ds.Tables[0].Rows[0]["SiteownerAddress"].ToString();
                        txtContractorName.Text = ds.Tables[0].Rows[0]["ContractorName"].ToString();
                        txtSupervisorName.Text = ds.Tables[0].Rows[0]["SupervisorName"].ToString();
                        txtSupervisorContact.Text = ds.Tables[0].Rows[0]["SupervisorPhoneNo"].ToString();
                        txtcontractorContact.Text = ds.Tables[0].Rows[0]["ContractorContactNo"].ToString();
                        txtContractorEmail.Text = ds.Tables[0].Rows[0]["ContractorEmail"].ToString();
                        txtContactPersonEmail.Text = ds.Tables[0].Rows[0]["SiteownerEmail"].ToString();
                        txtSupervisorEmail.Text = ds.Tables[0].Rows[0]["SupervisorEmail"].ToString();
                        //** end added by gurmeet 17 july-2025
                        txtSiteOwnerName.Text = ds.Tables[0].Rows[0]["OwnerName"].ToString();
                        // ContractorName.Visible = false;
                        // SupervisorName.Visible = false;
                        LineVoltage.Visible = false;
                        TypeOfInspection.Visible = false;
                        txtInspectionReportId.Text = ds.Tables[0].Rows[0]["Id"].ToString();
                        txtDistrict.Text = ds.Tables[0].Rows[0]["District"].ToString();
                        txtApplicantType.Text = ds.Tables[0].Rows[0]["ApplicantType"].ToString();
                        txtWorkType.Text = ds.Tables[0].Rows[0]["InstallationType"].ToString();
                        txtVoltage.Text = ds.Tables[0].Rows[0]["VoltageLevel"].ToString();
                        txtCapacity.Text = ds.Tables[0].Rows[0]["Capacity"].ToString();
                        txtSiteOwnerName.Text = ds.Tables[0].Rows[0]["OwnerName"].ToString();
                        ContractorName.Visible = false;
                        SupervisorName.Visible = false;
                        LineVoltage.Visible = false;
                        //txtTransactionId.Text = ds.Tables[0].Rows[0]["TransactionId"].ToString();
                        //txtTranscationDate.Text = ds.Tables[0].Rows[0]["TransactionDate1"].ToString();
                        txtAmount.Text = ds.Tables[0].Rows[0]["TotalAmount"].ToString();
                        
                        if (txtAmount.Text == "0")
                        {
                            transactionId.Visible = false;
                            transactionDate.Visible = false;
                        }
                        else
                        {
                            transactionId.Visible = true;
                            transactionDate.Visible = true;
                            txtTransactionId.Text = ds.Tables[0].Rows[0]["TransactionId"].ToString();
                            txtTranscationDate.Text = ds.Tables[0].Rows[0]["TransactionDate1"].ToString();
                        }
                        TypeOfInspection.Visible = false;
                        TRAttached.Visible = true;
                        TRAttachedGrid.Visible = true;
                        //GridView1.Columns[7].Visible = false;
                        //GridView1.Columns[5].Visible = false;
                        IntimationId = ds.Tables[0].Rows[0]["IntimationId"].ToString();
                        grd_Documemnts.Columns[1].Visible = true;

                        ListItem checklistItem1 = ddlReasonType.Items.FindByValue("2");
                        ListItem checklistItem2 = ddlReasonType.Items.FindByValue("3");
                        if (checklistItem1 != null)
                        {
                            checklistItem1.Enabled = false;
                        }
                        if (checklistItem2 != null)
                        {
                            checklistItem2.Enabled = false;
                        }
                        GridBindDocument(ID);
                        DivViewCart.Visible = true;
                        GridToViewCart(ID);
                        //Uncommented by neha 8-may
                        BindDivisions(txtDistrict.Text.Trim());
                        string Status = ds.Tables[0].Rows[0]["ApplicationStatus"].ToString();
                        if (Status.Trim() == "InProcess")
                        {
                            RdbtnAccptReturn.SelectedIndex = RdbtnAccptReturn.Items.IndexOf(RdbtnAccptReturn.Items.FindByValue("0"));
                            RdbtnAccptReturn.Attributes.Add("disabled", "true");
                            RdbtnAccptReturn.Enabled = false;
                            btnBack.Visible = true;
                            btnUpdate.Visible = false;
                        }
                        else if (Status.Trim() == "Return")
                        {
                            RdbtnAccptReturn.SelectedIndex = RdbtnAccptReturn.Items.IndexOf(RdbtnAccptReturn.Items.FindByValue("1"));
                            RdbtnAccptReturn.Attributes.Add("disabled", "true");
                            RdbtnAccptReturn.Enabled = false;
                            Return.Visible = true;
                            txtRejected.Text = ds.Tables[0].Rows[0]["ReturnRemarks"].ToString();
                            txtRejected.Attributes.Add("disabled", "true");
                            btnBack.Visible = true;
                            btnUpdate.Visible = false;
                        }
                        else if (Status.Trim() == "Approved" || Status.Trim() == "Rejected")
                        {
                            RdbtnAccptReturn.Enabled = false;
                            txtRejected.Attributes.Add("disabled", "true");
                            btnBack.Visible = true;
                            btnUpdate.Visible = false;
                            RejectionDDL.Visible = true;
                            RejectionRemarksDDL.Visible = true;
                            string RejectedImagPath = ds.Tables[0].Rows[0]["RejectionImage"].ToString();
                            if (!string.IsNullOrEmpty(RejectedImagPath))
                            {
                                Div_RejectionImage.Visible = true;
                                HyperLink_RejectedImg.NavigateUrl = ResolveUrl(RejectedImagPath); //open img
                            }
                            string TypeOfRejection = ds.Tables[0].Rows[0]["TypeOfRejection"].ToString();
                            RejectionDDL.SelectedIndex = RejectionDDL.Items.IndexOf(RejectionDDL.Items.FindByText(TypeOfRejection));
                            RejectionDDL.Attributes.Add("disabled", "true");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session["AdminId"] = "";
                Response.Redirect("/AdminLogout.aspx", false);
            }
        }
        private void GetGridNewInspectionMultiple(string ID)
        {
            try
            {
                //string ID = Session["InspectionId"].ToString();
                DataSet dsVC = CEI.GetDetailsToViewTRinMultipleCaseNew(ID);
                if (dsVC != null && dsVC.Tables.Count > 0 && dsVC.Tables[0].Rows.Count > 0)
                {

                    //Added if and else Condition by neha 9-May-2025
                    LblGrid_MultipleInspectionTR.Visible = false;
                    Grid_MultipleInspectionTR.DataSource = dsVC;
                    Grid_MultipleInspectionTR.DataBind();
                }
                else
                {
                    LblGrid_MultipleInspectionTR.Visible = true;
                    LblGrid_MultipleInspectionTR.Text = "NA";
                    Grid_MultipleInspectionTR.DataSource = null;
                    Grid_MultipleInspectionTR.DataBind();
                    //string script = "alert('No Record Found');";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            }
            catch (Exception ex)
            {
                Session["AdminId"] = "";
                Response.Redirect("/AdminLogout.aspx", false);
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/IntimationHistoryForAdmin.aspx", false);
            Session["InspectionId"] = "";
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int checksuccessmessage = 0;
            try
            {
                if (Session["InspectionId"].ToString() != null && Session["InspectionId"].ToString() != "" && Session["AdminID"].ToString() != null)
                {
                    StaffId = Session["AdminID"].ToString();
                    ID = Session["InspectionId"].ToString();
                    AssignFrom = Session["AdminID"].ToString();

                    bool isRemarksValid = true;
                    #region RemarksValidation

                    foreach (GridViewRow row in Grid_TRDocuments.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)row.FindControl("chk_Select");
                        TextBox txtRemarks = (TextBox)row.FindControl("txt_Remarks");

                        if (chkSelect != null && chkSelect.Checked && (txtRemarks == null || string.IsNullOrWhiteSpace(txtRemarks.Text)))
                        {
                            isRemarksValid = false;
                            break;
                        }
                    }

                    foreach (GridViewRow row in grd_ChecklistDocumemnts.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)row.FindControl("chk_SelectDoc");
                        TextBox txtRemarks = (TextBox)row.FindControl("txt_RemarksforOwnerDoc");

                        if (chkSelect != null && chkSelect.Checked && (txtRemarks == null || string.IsNullOrWhiteSpace(txtRemarks.Text)))
                        {
                            isRemarksValid = false;
                            break;
                        }
                    }

                    if (!isRemarksValid)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alert('Please fill in remarks for selected rows.');", true);
                        return;
                    }

                    #endregion

                    //Uncommented by neha 8-may
                    if (RadioButtonAction.SelectedValue != "" && RadioButtonAction.SelectedValue != null)
                    {
                        if (RadioButtonAction.SelectedValue == "0")
                        {
                            if (RdbtnAccptReturn.SelectedValue != "" && RdbtnAccptReturn.SelectedValue != null)
                            {
                                AcceptorReturn = RdbtnAccptReturn.SelectedValue == "0" ? "Accepted" : "Return";
                                Reason = string.IsNullOrEmpty(txtRejected.Text) ? null : txtRejected.Text;

                                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                                try
                                {
                                    string reqType = CEI.GetIndustry_RequestType_New(Convert.ToInt32(ID));
                                    if (reqType == "Industry")
                                    {
                                        string serverStatus = CEI.CheckServerStatus("https://investharyana.in");
                                        // string serverStatus = CEI.CheckServerStatus("https://investharyana.in/api/project-service-logs-external_UHBVN");
                                        if (serverStatus != "Server is reachable.")
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alert('HEPC Server Is Not Responding . Please Try After Some Time')", true);
                                            return;
                                        }
                                    }
                                    DataSet ds = new DataSet();
                                    ds = CEI.GetTypeOfInspection(ID);
                                    InstallType = ds.Tables[0].Rows[0]["TypeOfInspection"].ToString();

                                    if (RdbtnAccptReturn.SelectedValue == "2")
                                    {
                                        CEI.UpdateInspectionRejection(ID, StaffId, ddlRejectionReasonType.SelectedItem.ToString(), Reason);
                                        checksuccessmessage = 1;
                                    }
                                    else
                                    {
                                        if (InstallType == "New")
                                        {
                                            if (RdbtnAccptReturn.SelectedValue == "0")
                                            {
                                                CEI.updateInspectionCEI(ID, StaffId, IntimationId, count, txtWorkType.Text.Trim(), AcceptorReturn, Reason, ddlReasonType.SelectedItem.Value);
                                            }
                                            else if (RdbtnAccptReturn.SelectedValue == "1")
                                            {
                                                SqlTransaction transaction = null;
                                                try
                                                {
                                                    using (SqlConnection connection = new SqlConnection(connectionString))
                                                    {
                                                        connection.Open();
                                                        transaction = connection.BeginTransaction();
                                                        CEI.UpdateStatusOfReturnedInspection(ID, StaffId, ddlReasonType.SelectedItem.Value, transaction);

                                                        if (ddlReasonType.SelectedItem.Value == "1") // Checklist Documents
                                                        {
                                                            bool AtLeastOneChecked = false;
                                                            foreach (GridViewRow row in grd_ChecklistDocumemnts.Rows)
                                                            {
                                                                CheckBox chkChecklist = (CheckBox)row.FindControl("chk_SelectDoc");
                                                                if (chkChecklist != null && chkChecklist.Checked)
                                                                {
                                                                    AtLeastOneChecked = true;

                                                                    Label LabelRowId = (Label)row.FindControl("LabelRowId");
                                                                    TextBox txt_RemarksforOwnerDoc = (TextBox)row.FindControl("txt_RemarksforOwnerDoc");
                                                                    if (LabelRowId != null && !string.IsNullOrEmpty(txt_RemarksforOwnerDoc.Text))
                                                                    {
                                                                        CEI.updateReturnRemarksOnBasesOnChecklistDocuments(ID, LabelRowId.Text, txt_RemarksforOwnerDoc.Text, transaction);
                                                                    }
                                                                }
                                                            }
                                                            if (AtLeastOneChecked != true)
                                                            {
                                                                transaction.Rollback();
                                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Tick Atleast One Test Report Before Submit'); ", true);
                                                                return;
                                                            }
                                                        }
                                                        else if (ddlReasonType.SelectedItem.Value == "2") // Test Report Documents
                                                        {
                                                            bool AtLeastOneChecked = false;
                                                            foreach (GridViewRow row in Grid_TRDocuments.Rows)
                                                            {
                                                                CheckBox chk = (CheckBox)row.FindControl("chk_Select");
                                                                if (chk != null && chk.Checked)
                                                                {
                                                                    AtLeastOneChecked = true;

                                                                    Label Labelid = (Label)row.FindControl("Labelid");
                                                                    Label LblIntimationId = (Label)row.FindControl("LblIntimationId");
                                                                    TextBox txtRemarks = (TextBox)row.FindControl("txt_Remarks");
                                                                    if (Labelid != null && !string.IsNullOrEmpty(txtRemarks.Text))
                                                                    {
                                                                        CEI.updateReturnRemarksOnBasesOfTrDocuments(ID, LblIntimationId.Text, Labelid.Text, txtRemarks.Text, transaction);
                                                                    }
                                                                }
                                                            }
                                                            if (!AtLeastOneChecked)
                                                            {
                                                                transaction.Rollback();
                                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Tick Atleast One Test Report Before Submit'); ", true);
                                                            }
                                                        }
                                                        else if (ddlReasonType.SelectedItem.Value == "3") // Checklist & Test Report Documents
                                                        {
                                                            bool isChecklistValid = false;
                                                            bool isTestReportValid = false;
                                                            bool allRemarksFilled = true;

                                                            foreach (GridViewRow row in grd_ChecklistDocumemnts.Rows)
                                                            {
                                                                CheckBox chkChecklist = (CheckBox)row.FindControl("chk_SelectDoc");
                                                                TextBox txt_RemarksforOwnerDoc = (TextBox)row.FindControl("txt_RemarksforOwnerDoc");

                                                                if (chkChecklist != null && chkChecklist.Checked)
                                                                {
                                                                    isChecklistValid = true;
                                                                    if (txt_RemarksforOwnerDoc == null || string.IsNullOrEmpty(txt_RemarksforOwnerDoc.Text))
                                                                    {
                                                                        allRemarksFilled = false;
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                            foreach (GridViewRow row in Grid_TRDocuments.Rows)
                                                            {
                                                                CheckBox chk = (CheckBox)row.FindControl("chk_Select");
                                                                TextBox txtRemarks = (TextBox)row.FindControl("txt_Remarks");
                                                                if (chk != null && chk.Checked)
                                                                {
                                                                    isTestReportValid = true;
                                                                    if (txtRemarks == null || string.IsNullOrEmpty(txtRemarks.Text))
                                                                    {
                                                                        allRemarksFilled = false;
                                                                        break;
                                                                    }
                                                                }
                                                            }

                                                            if (!isChecklistValid || !isTestReportValid)
                                                            {
                                                                transaction.Rollback();
                                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please tick at least one document in both Checklist and Test Report Documents before submitting.');", true);
                                                                return;
                                                            }
                                                            else if (!allRemarksFilled)
                                                            {
                                                                transaction.Rollback();
                                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please ensure all checked rows have remarks filled in.');", true);
                                                                return;
                                                            }
                                                            else
                                                            {
                                                                // Process Checklist Documents
                                                                foreach (GridViewRow row in grd_ChecklistDocumemnts.Rows)
                                                                {
                                                                    CheckBox chkChecklist = (CheckBox)row.FindControl("chk_SelectDoc");
                                                                    if (chkChecklist != null && chkChecklist.Checked)
                                                                    {
                                                                        Label LabelRowId = (Label)row.FindControl("LabelRowId");
                                                                        TextBox txt_RemarksforOwnerDoc = (TextBox)row.FindControl("txt_RemarksforOwnerDoc");

                                                                        if (LabelRowId != null && !string.IsNullOrEmpty(txt_RemarksforOwnerDoc.Text))
                                                                        {
                                                                            CEI.updateReturnRemarksOnBasesOnChecklistDocuments(ID, LabelRowId.Text, txt_RemarksforOwnerDoc.Text, transaction);
                                                                        }
                                                                    }
                                                                }

                                                                // Process Test Report Documents
                                                                foreach (GridViewRow row in Grid_TRDocuments.Rows)
                                                                {
                                                                    CheckBox chk = (CheckBox)row.FindControl("chk_Select");
                                                                    if (chk != null && chk.Checked)
                                                                    {
                                                                        Label Labelid = (Label)row.FindControl("Labelid");
                                                                        Label LblIntimationId = (Label)row.FindControl("LblIntimationId");
                                                                        TextBox txtRemarks = (TextBox)row.FindControl("txt_Remarks");

                                                                        if (Labelid != null && !string.IsNullOrEmpty(txtRemarks.Text))
                                                                        {
                                                                            CEI.updateReturnRemarksOnBasesOfTrDocuments(ID, LblIntimationId.Text, Labelid.Text, txtRemarks.Text, transaction);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        transaction.Commit();
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    transaction.Rollback();
                                                }
                                            }
                                        }
                                        else if (InstallType == "Periodic")
                                        {
                                            CEI.updateInspectionPeriodic(ID, StaffId, IntimationId, txtWorkType.Text.Trim(), AcceptorReturn, Reason, DdlReturnInPeriodic.SelectedItem.Value);
                                        }
                                        checksuccessmessage = 1;
                                    }
                                    string actiontype = AcceptorReturn == "Accepted" ? "InProgress" : "Return";
                                    List<Industry_Api_Post_DataformatModel> ApiPostformatResults = CEI.GetIndustry_OutgoingRequestFormat(Convert.ToInt32(ID), actiontype);
                                    foreach (var ApiPostformatresult in ApiPostformatResults)
                                    {
                                        if (ApiPostformatresult.PremisesType == "Industry")
                                        {
                                            // string accessToken = TokenManagerConst.GetAccessToken(ApiPostformatresult);
                                            string accessToken = TokenManagerConst.GetAccessToken(ApiPostformatresult);
                                            // string accessToken = "dfsfdsfsfsdf";
                                            logDetails = CEI.Post_Industry_Inspection_StageWise_JsonData(
                                                          "https://investharyana.in/api/project-service-logs-external_UHBVN",
                                                          new Industry_Inspection_StageWise_JsonDataFormat_Model
                                                          {
                                                              actionTaken = ApiPostformatresult.ActionTaken,
                                                              commentByUserLogin = ApiPostformatresult.CommentByUserLogin,
                                                              commentDate = ApiPostformatresult.CommentDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                                                              comments = ApiPostformatresult.Comments,
                                                              id = ApiPostformatresult.Id,
                                                              projectid = ApiPostformatresult.ProjectId,
                                                              serviceid = ApiPostformatresult.ServiceId
                                                          }, ApiPostformatresult, accessToken);

                                            if (!string.IsNullOrEmpty(logDetails.ErrorMessage))
                                            {
                                                throw new Exception(logDetails.ErrorMessage);
                                            }

                                            CEI.LogToIndustryApiSuccessDatabase(
                                            logDetails.Url,
                                            logDetails.Method,
                                            logDetails.RequestHeaders,
                                            logDetails.ContentType,
                                            logDetails.RequestBody,
                                            logDetails.ResponseStatusCode,
                                            logDetails.ResponseHeaders,
                                            logDetails.ResponseBody,

                                            new Industry_Api_Post_DataformatModel
                                            {
                                                InspectionId = ApiPostformatresult.InspectionId,
                                                InspectionLogId = ApiPostformatresult.InspectionLogId,
                                                IncomingJsonId = ApiPostformatresult.IncomingJsonId,
                                                ActionTaken = ApiPostformatresult.ActionTaken,
                                                CommentByUserLogin = ApiPostformatresult.CommentByUserLogin,
                                                CommentDate = ApiPostformatresult.CommentDate,

                                                Comments = ApiPostformatresult.Comments,
                                                Id = ApiPostformatresult.Id,
                                                ProjectId = ApiPostformatresult.ProjectId,
                                                ServiceId = ApiPostformatresult.ServiceId,
                                            }
                                        );
                                        }
                                    }
                                }
                                catch (TokenManagerException ex)
                                {
                                    CEI.LogToIndustryApiErrorDatabase(
                                        ex.RequestUrl,
                                        ex.RequestMethod,
                                        ex.RequestHeaders,
                                        ex.RequestContentType,
                                        ex.RequestBody,
                                        ex.ResponseStatusCode,
                                        ex.ResponseHeaders,
                                        ex.ResponseBody,
                                        new Industry_Api_Post_DataformatModel
                                        {
                                            InspectionId = ex.InspectionId,
                                            InspectionLogId = ex.InspectionLogId,
                                            IncomingJsonId = ex.IncomingJsonId,
                                            ActionTaken = ex.ActionTaken,
                                            CommentByUserLogin = ex.CommentByUserLogin,
                                            CommentDate = ex.CommentDate,
                                            Comments = ex.Comments,
                                            Id = ex.Id,
                                            ProjectId = ex.ProjectId,
                                            ServiceId = ex.ServiceId,
                                        }
                                    );
                                    string errorMessage = CEI.IndustryTokenApiReturnedErrorMessage(ex);
                                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alertWithRedirectdata();", true);
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alert('" + ex.Message.ToString() + "')", true);
                                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", $"alert('{errorMessage}')", true);
                                }
                                catch (IndustryApiException ex)
                                {
                                    CEI.LogToIndustryApiErrorDatabase(
                                        ex.RequestUrl,
                                        ex.RequestMethod,
                                        ex.RequestHeaders,
                                        ex.RequestContentType,
                                        ex.RequestBody,
                                        ex.ResponseStatusCode,
                                        ex.ResponseHeaders,
                                        ex.ResponseBody,
                                        new Industry_Api_Post_DataformatModel
                                        {
                                            InspectionId = ex.InspectionId,
                                            InspectionLogId = ex.InspectionLogId,
                                            IncomingJsonId = ex.IncomingJsonId,
                                            ActionTaken = ex.ActionTaken,
                                            CommentByUserLogin = ex.CommentByUserLogin,
                                            CommentDate = ex.CommentDate,

                                            Comments = ex.Comments,
                                            Id = ex.Id,
                                            ProjectId = ex.ProjectId,
                                            ServiceId = ex.ServiceId,
                                        }
                                    );

                                    string errorMessage = CEI.IndustryApiReturnedErrorMessage(ex);

                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alertWithRedirectdata();", true);
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alert('" + ex.Message.ToString() + "')", true);
                                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", $"alert('{errorMessage}')", true);
                                }

                                catch (Exception ex)
                                {
                                    // Rollback the transaction if an error occurs
                                    //transaction.Rollback();
                                    // Handle the exception, log it, etc.
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alert('An error occurred.');", true);
                                }
                                finally
                                {
                                    if (checksuccessmessage == 1)
                                    {
                                        if (AcceptorReturn == "Accepted")
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alertWithRedirectdata();", true);
                                        }
                                        else
                                        {
                                            if (RdbtnAccptReturn.SelectedValue == "2")
                                            {
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessage", "alert('Inspection Rejected Successfully'); window.location='IntimationHistoryForAdmin.aspx'", true);
                                            }
                                            else
                                            {
                                                if (RdbtnAccptReturn.SelectedValue == "1")
                                                {
                                                    if (ddlReasonType.SelectedItem.Value == "0") //Select
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alertWithRedirectdataCommonReturn();", true);
                                                    }
                                                    else if (ddlReasonType.SelectedItem.Value == "1") //Based On Check Documents Returned 
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alertWithRedirectdataCommonReturn();", true);
                                                    }
                                                    else if (ddlReasonType.SelectedItem.Value == "2") //Based On Test Documents Returned 
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alertWithRedirectdataCommonReturn();", true);
                                                    }
                                                    else if (ddlReasonType.SelectedItem.Value == "3") //Based On Test Documents Returned 
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alertWithRedirectdataCommonReturn();", true);
                                                    }
                                                }
                                                else if (RdbtnAccptReturn.SelectedValue == "0")
                                                {
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alertWithRedirectdata();", true);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessage", "alert('Please select an Action for Inspection');", true);
                            }

                            //Uncommented by neha 8-may
                        }
                        else
                        {
                            if (ddlToAssign.SelectedValue != null && ddlToAssign.SelectedValue != "0")
                            {
                                try
                                {
                                    StaffTo = ddlToAssign.SelectedValue;
                                    int x = CEI.UpdateInspectionDataOnAction(ID, StaffTo, AssignFrom);
                                    if (x > 0)
                                    {
                                        ddlDivisions.SelectedIndex = 0;
                                        ddlToAssign.SelectedIndex = 0;
                                        Session["InspectionId"] = "";
                                        string script = $"alert('Inspection sent to {StaffTo} successfully.'); window.location='IntimationHistoryForAdmin.aspx';";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessScript", script, true);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Session["Type"] = null;
                                    Session["Type"] = "";
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert()", "alert('" + ex.Message.ToString() + "'); window.location.href = '/Admin/Transfer_Inspections_ToDifferentStaff_ByAdmin.aspx';", true);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert()", "alert('" + ex.Message.ToString() + "')", true);
                                    return;
                                }
                            }
                            else
                            {
                                ddlToAssign.Focus();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessScript", "alert('Select Staff before save');", true);
                                return;
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessage", "alert('Please select the Process or Transfer');", true);
                    }
                }
                        else
                {
                    Response.Redirect("/AdminLogout.aspx");
                }
                Session["Type"] = null;
                Session["Type"] = "";
            }
            catch (Exception ex)
            {
                Session["AdminId"] = "";
                Response.Redirect("/AdminLogout.aspx", false);
            }
        }
        private void GridToViewCart(string ID)
        {
            try
            {
                //string ID = Session["InspectionId"].ToString();
                DataSet dsVC = CEI.GetDetailsToViewCart(ID);

                //Added if and else Condition by neha 9-May-2025
                if (dsVC != null && dsVC.Tables.Count > 0 && dsVC.Tables[0].Rows.Count > 0)
                {
                    LblGridView2.Visible = false;
                    GridView2.DataSource = dsVC;
                    GridView2.DataBind();
                }
                else
                {
                    LblGridView2.Visible = true;
                    LblGridView2.Text = "NA";
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                    //string script = "alert('No Record Found');";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            }
            catch (Exception ex)
            {
                Session["AdminId"] = "";
                Response.Redirect("/AdminLogout.aspx", false);
            }
        }
        protected void lnkRedirect1_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)(sender);

                GridViewRow row = (GridViewRow)btn.NamingContainer;
                Label lblInstallationName = (Label)row.FindControl("LblInstallationName");
                string installationName = lblInstallationName.Text.Trim();
                Session["InspectionTestReportId"] = btn.CommandArgument;
                if (installationName == "Line")
                {
                    Response.Redirect("/TestReportModal/LineTestReportModal.aspx", false);
                }
                else if (installationName == "Substation Transformer")
                {
                    Response.Redirect("/TestReportModal/SubstationTransformerTestReportModal.aspx", false);
                }
                else if (installationName == "Generating Set")
                {
                    Response.Redirect("/TestReportModal/GeneratingSetTestReportModal.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session["AdminId"] = "";
                Response.Redirect("/AdminLogout.aspx", false);
            }
        }
        protected void grd_Documemnts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string fileName = "";
            try
            {
                if (e.CommandName == "Select")
                {
                    ID = Session["InspectionId"].ToString();
                    fileName = "https://ceiharyana.com" + e.CommandArgument.ToString();
                    // fileName = "https://ceiharyana.com" + e.CommandArgument.ToString();
                    string script = $@"<script>window.open('{fileName}','_blank');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "OpenFileInNewTab", script);
                }
            }
            catch (Exception ex)
            {
                Session["AdminId"] = "";
                Response.Redirect("/AdminLogout.aspx", false);
            }
        }

        //Uncommented by neha 8-may
        private void BindDropDownToAssign(string Division)
        {
            try
            {
                DataSet dsAssign = new DataSet();
                dsAssign = CEI.DdlToStaffAssign(Division);
                ddlToAssign.DataSource = dsAssign;
                ddlToAssign.DataTextField = "Staff";
                ddlToAssign.DataValueField = "StaffUserId";
                ddlToAssign.DataBind();
                ddlToAssign.Items.Insert(0, new ListItem("Select", "0"));
                dsAssign.Clear();
            }
            catch (Exception ex)
            {
                //msg.Text = ex.Message;
            }
        }
        //
        protected void ddlDivisions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDivisions.SelectedValue != "" && ddlDivisions.SelectedValue != null)
            {
                BindDropDownToAssign(ddlDivisions.SelectedValue);
            }
        }
        protected void RdbtnAccptReturn_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnUpdate.Visible = true;
            Return.Visible = false;
            if (RdbtnAccptReturn.SelectedValue == "1") //No(Return)
            {
                Return.Visible = true;
                btnUpdate.Visible = true;
                if (Session["Type"] != null && Session["Type"].ToString() != "" && Session["Type"].ToString() == "Periodic")
                {
                    DdlReturnInPeriodic.Visible = true;
                    DivReason.Visible = true;
                    DivRejectionReasonType.Visible = false;
                }
                else if (Session["Type"] != null && Session["Type"].ToString() != "" && Session["Type"].ToString() == "New")
                {
                    //Return.Visible = true;
                    DdlReturnInPeriodic.Visible = false;
                    DivReason.Visible = false;
                    DivReason.Visible = false;
                    DivRejectionReasonType.Visible = false;
                }
                else
                {
                    DivRejectionReasonType.Visible = false;
                }
            }
            else if (RdbtnAccptReturn.SelectedValue == "2")
            {
                DivRejectionReasonType.Visible = true;
                DivReason.Visible = true;
                DIV_ChecklistDocuments.Visible = false;
                Check_ChecklistDocuments.Visible = false;
                DIV_TRDocuments.Visible = false;
                Check_TRDocuments.Visible = false;
                ddlReasonType.SelectedValue = "0";
            }
            else if (RdbtnAccptReturn.SelectedValue == "0")
            {
                DivRejectionReasonType.Visible = false;
                DivReason.Visible = false;
                DIV_ChecklistDocuments.Visible = false;
                Check_ChecklistDocuments.Visible = false;
                DIV_TRDocuments.Visible = false;
                Check_TRDocuments.Visible = false;
            }
        }

        //Uncommented by neha 8-may
        protected void RadioButtonAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonAction.SelectedValue == "1")
            {
                // BindDivisions();
                TransferButton.Visible = true;
                btnUpdate.Visible = true;
                Action.Visible = false;
                Return.Visible = false;
                //btnAction.Visible = false;
            }
            else
            {
                TransferButton.Visible = false;
                Action.Visible = true;
                Return.Visible = false;
                btnUpdate.Visible = true;
            }
        }
        //
        protected void GridBindDocument(string ID)
        {
            try
            {
                //ID = Session["InspectionId"].ToString();
                DataSet ds = new DataSet();
                ds = CEI.ViewDocuments(ID);

                //Added if and else Condition by neha 9-May-2025
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //if (ds.Tables.Count > 0)
                {
                    Lblgrd_Documemnts.Visible = false;
                    grd_Documemnts.DataSource = ds;
                    grd_Documemnts.DataBind();
                }
                else
                {
                    Lblgrd_Documemnts.Visible = true;
                    Lblgrd_Documemnts.Text = "NA";
                    grd_Documemnts.DataSource = null;
                    grd_Documemnts.DataBind();
                    // string script = "alert(\"No Record Found for document\");";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                ds.Dispose();
            }
            catch (Exception ex)
            {
                Session["AdminId"] = "";
                Response.Redirect("/AdminLogout.aspx", false);
            }
        }
        private void GetTestReportData(string ID)
        {
            try
            {
                //ID = Session["InspectionId"].ToString();
                DataSet ds = new DataSet();
                //commented Condition only by gurmeet 1May
                //ds = CEI.GetTestReport(ID);

                //Added if and else Condition by neha 9-May-2025
                ds = CEI.GetInspectionHistoryLogs(ID);
                string TestRportId = string.Empty;
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //if (ds != null && ds.Tables.Count > 0)
                {
                    LblGridView1.Visible = false;
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    LblGridView1.Visible = true;
                    LblGridView1.Text = "NA";
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    //string script = "alert(\"No Record Found\");";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                ds.Dispose();
            }
            catch (Exception ex)
            {
                Session["AdminId"] = "";
                Response.Redirect("/AdminLogout.aspx", false);
            }
        }
        protected void lnkRedirect_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            Session["InspectionTestReportId"] = btn.CommandArgument; //txtTestReportId.Text;
            if (txtWorkType.Text.Trim() == "Line")
            {
                Response.Redirect("/TestReportModal/LineTestReportModal.aspx", false);
            }
            else if (txtWorkType.Text.Trim() == "Substation Transformer")
            {
                Response.Redirect("/TestReportModal/SubstationTransformerTestReportModal.aspx", false);
            }
            else if (txtWorkType.Text.Trim() == "Generating Set")
            {
                Response.Redirect("/TestReportModal/GeneratingSetTestReportModal.aspx", false);
            }
        }
        protected void lnkRedirectTRr_Click1(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)(sender);
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                Label lblInstallationName = (Label)row.FindControl("LblInstallationName");
                string installationName = lblInstallationName.Text.Trim();
                Session["InspectionTestReportId"] = btn.CommandArgument;
                if (installationName == "Line")
                {
                    Response.Redirect("/TestReportModal/LineTestReportModal.aspx", false);
                }
                else if (installationName == "Substation Transformer")
                {
                    Response.Redirect("/TestReportModal/SubstationTransformerTestReportModal.aspx", false);
                }
                else if (installationName == "Generating Set")
                {
                    Response.Redirect("/TestReportModal/GeneratingSetTestReportModal.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session["AdminId"] = "";
                Response.Redirect("/AdminLogout.aspx", false);
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) 
            {
                //commented Condition only by gurmeet 1May
                //string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
                string status = DataBinder.Eval(e.Row.DataItem, "ActionTaken").ToString();

                if (status == "RETURN")
                {
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                }
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //e.Row.Cells[2].BackColor = System.Drawing.Color.Blue;
                e.Row.Cells[2].BackColor = ColorTranslator.FromHtml("#9292cc");
            }
        }
        #region Return
        protected void ddlReasonType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["InspectionId"]) != null && Convert.ToString(Session["InspectionId"]) != string.Empty)
            {
                String ReportInspectionID = Convert.ToString(Session["InspectionId"]);
                if (ddlReasonType.SelectedValue == "1")
                {
                    DIV_ChecklistDocuments.Visible = true;
                    Check_ChecklistDocuments.Visible = true;
                    GridChecklistDocuments(ReportInspectionID);
                    DIV_TRDocuments.Visible = false;
                    Check_TRDocuments.Visible = false;
                }
                else if (ddlReasonType.SelectedValue == "2")
                {
                    DIV_TRDocuments.Visible = true;
                    Check_TRDocuments.Visible = true;
                    GridTRDocuments(ReportInspectionID);
                    DIV_ChecklistDocuments.Visible = false;
                    Check_ChecklistDocuments.Visible = false;
                }
                else if (ddlReasonType.SelectedValue == "3")
                {
                    DIV_ChecklistDocuments.Visible = true;
                    Check_ChecklistDocuments.Visible = true;
                    DIV_TRDocuments.Visible = true;
                    Check_TRDocuments.Visible = true;

                    GridChecklistDocuments(ReportInspectionID);
                    GridTRDocuments(ReportInspectionID);
                }
                else
                {
                    DIV_ChecklistDocuments.Visible = false;
                    Check_ChecklistDocuments.Visible = false;
                    DIV_TRDocuments.Visible = false;
                    Check_TRDocuments.Visible = false;
                }
            }
            else
            {
                Session["InspectionId"] = "";
                Response.Redirect("/AdminLogout.aspx", false);
            }
        }
        private void GridTRDocuments(string ID)
        {
            try
            {
                //string ID = Session["InspectionId"].ToString();
                DataSet dsVC = CEI.SelectRemarksDocumentsattachedinTR(ID);
                if (dsVC != null && dsVC.Tables.Count > 0 && dsVC.Tables[0].Rows.Count > 0)
                {
                    Grid_TRDocuments.DataSource = dsVC;
                    Grid_TRDocuments.DataBind();
                }
                else
                {
                    Grid_TRDocuments.DataSource = null;
                    Grid_TRDocuments.DataBind();
                    string script = "alert('No Record Found');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            }
            catch (Exception ex)
            {
                Session["AdminId"] = "";
                Response.Redirect("/AdminLogout.aspx", false);
            }
        }
        private void GridChecklistDocuments(string ID)
        {
            try
            {
                ////ID = Session["InspectionId"].ToString();
                DataSet ds = new DataSet();
                ds = CEI.ViewDocuments(ID);

                //Added if and else Condition by neha 9-May-2025
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //if (ds.Tables.Count > 0)
                {
                    grd_ChecklistDocumemnts.DataSource = ds;
                    grd_ChecklistDocumemnts.DataBind();
                }
                else
                {
                    grd_ChecklistDocumemnts.DataSource = null;
                    grd_ChecklistDocumemnts.DataBind();
                    string script = "alert(\"No Record Found\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                ds.Dispose();
            }
            catch (Exception ex)
            {
                Session["AdminId"] = "";
                Response.Redirect("/AdminLogout.aspx", false);
            }
        }
        protected void chk_SelectDoc_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkBox.NamingContainer;
            TextBox txtRemarks = (TextBox)row.FindControl("txt_RemarksforOwnerDoc");

            // Enable or disable the TextBox based on checkbox checked state
            txtRemarks.Enabled = chkBox.Checked;

            if (!chkBox.Checked)
            {
                txtRemarks.Text = string.Empty;
            }
        }
        protected void chk_Select_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkBox.NamingContainer;
            TextBox txtRemarks1 = (TextBox)row.FindControl("txt_Remarks");

            // Enable or disable the TextBox based on checkbox checked state
            txtRemarks1.Enabled = chkBox.Checked;

            if (!chkBox.Checked)
            {
                txtRemarks1.Text = string.Empty;
            }
        }
        protected void Grid_MultipleInspectionTR_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string Count = string.Empty;
            string IntimationId = string.Empty;
            if (e.CommandName == "Select")
            {
                Control ctrl = e.CommandSource as Control;
                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                Label LblInstallationName = (Label)row.FindControl("LblInstallationName");
                Label LblTestReportCount = (Label)row.FindControl("LblTestReportCount");
                //IntimationId = Session["id"].ToString();
                Count = LblTestReportCount.Text.Trim();
                Label LblIntimationId = (Label)row.FindControl("LblIntimationId");
                IntimationId = LblIntimationId.Text.Trim();
                DataSet ds = new DataSet();
                ds = CEI.GetData(LblInstallationName.Text.Trim(), IntimationId, Count);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //if (ds.Tables[0].Rows.Count > 0)
                {
                    if (LblInstallationName.Text.Trim() == "Line")
                    {
                        Session["LineID"] = ds.Tables[0].Rows[0]["TestReportId"].ToString();
                        Response.Redirect("/TestReportModal/LineTestReportModal.aspx", false);
                    }
                    else if (LblInstallationName.Text.Trim() == "Substation Transformer")
                    {
                        Session["SubStationID"] = ds.Tables[0].Rows[0]["TestReportId"].ToString();
                        Response.Redirect("/TestReportModal/SubstationTransformerTestReportModal.aspx", false);
                    }
                    else if (LblInstallationName.Text.Trim() == "Generating Set")
                    {
                        Session["GeneratingSetId"] = ds.Tables[0].Rows[0]["TestReportId"].ToString();
                        Response.Redirect("/TestReportModal/GeneratingSetTestReportModal.aspx", false);
                    }
                }
            }
            else if (e.CommandName == "View")
            {
                string fileName = "";
                fileName = "https://ceiharyana.com" + e.CommandArgument.ToString();
                //fileName = "https://ceiharyana.com" + e.CommandArgument.ToString();
                string script = $@"<script>window.open('{fileName}','_blank');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "OpenFileInNewTab", script);
            }
            else if (e.CommandName == "ViewInvoice")
            {
                string fileName = "";
                fileName = "https://ceiharyana.com" + e.CommandArgument.ToString();
                //fileName = "https://ceiharyana.com" + e.CommandArgument.ToString();
                string script = $@"<script>window.open('{fileName}','_blank');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "OpenFileInNewTab", script);
            }
        }
        protected void Grid_MultipleInspectionTR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                   // Label LblInstallationName = (Label)e.Row.FindControl("LblInstallationName");
                    LinkButton linkButtonInvoice = (LinkButton)e.Row.FindControl("lnkInstallaionInvoice");
                    LinkButton LinkButtonReport = (LinkButton)e.Row.FindControl("lnkManufacturingReport");
                    //Only Condition Changed By Navneet 30-april-2025 
                    //if (LblInstallationName.Text.Trim() == "Line")
                    //{
                    if (LinkButtonReport.CommandArgument.ToString() == null || LinkButtonReport.CommandArgument.ToString() == "" || linkButtonInvoice.CommandArgument.ToString() == null)
                    {
                        //Grid_MultipleInspectionTR.Columns[5].Visible = false;
                        Grid_MultipleInspectionTR.Columns[7].Visible = false;
                       // Grid_MultipleInspectionTR.Columns[6].Visible = false;
                        Grid_MultipleInspectionTR.Columns[8].Visible = false;
                        linkButtonInvoice.Visible = false;
                        LinkButtonReport.Visible = false;
                    }
                    else
                    {
                        //Grid_MultipleInspectionTR.Columns[5].Visible = true;
                        Grid_MultipleInspectionTR.Columns[7].Visible = true;
                       // Grid_MultipleInspectionTR.Columns[6].Visible = true;
                        Grid_MultipleInspectionTR.Columns[8].Visible = true;
                        linkButtonInvoice.Visible = true;
                        LinkButtonReport.Visible = true;
                        ViewState["AllRowsAreLine"] = false;
                    }
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    if (ViewState["AllRowsAreLine"] == null || (bool)ViewState["AllRowsAreLine"] == true)
                    {
                        ddlReasonType.Items.Clear();
                        ddlReasonType.Items.Add(new ListItem("Select", "0"));
                        ddlReasonType.Items.Add(new ListItem("Checklist Documents", "1"));
                    }
                    else
                    {
                        // Restore original items
                        ddlReasonType.Items.Clear();
                        ddlReasonType.Items.Add(new ListItem("Select", "0"));
                        ddlReasonType.Items.Add(new ListItem("Checklist Documents", "1"));
                        ddlReasonType.Items.Add(new ListItem("Test Report Documents", "2"));
                        ddlReasonType.Items.Add(new ListItem("Both (Checklist & TestReport Documents)", "3"));
                    }
                }
            }
            catch (Exception ex)
            {
                Session["AdminId"] = "";
                Response.Redirect("/AdminLogout.aspx", false);
            }
        }
        #endregion
        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    bool allFilesArePDF = true;
        //    for (int i = 1; i <= 15; i++)
        //    {
        //        FileUpload fileUploadControl = (FileUpload)FindControl("FileUpload" + i);
        //        if (fileUploadControl.HasFile)
        //        {
        //            string fileExtension = System.IO.Path.GetExtension(fileUploadControl.FileName);
        //            if (fileExtension.ToLower() != ".pdf")
        //            {
        //                allFilesArePDF = false;
        //                break;
        //            }
        //        }
        //    }

        //    if (allFilesArePDF)
        //    {
        //        string Assign = string.Empty;
        //        string To = string.Empty;
        //        string input = txtVoltage.Text;
        //        string id = Session["LineID"].ToString();
        //        string IntimationId = Session["IntimationId"].ToString();
        //        string CreatedBy = Session["SiteOwnerId"].ToString();
        //        string FileName = string.Empty;
        //        string flpPhotourl = string.Empty;
        //        string flpPhotourl1 = string.Empty;
        //        string flpPhotourl2 = string.Empty;
        //        string flpPhotourl3 = string.Empty;
        //        string flpPhotourl4 = string.Empty;
        //        string flpPhotourl5 = string.Empty;
        //        string flpPhotourl6 = string.Empty;
        //        string flpPhotourl7 = string.Empty;
        //        string flpPhotourl8 = string.Empty;
        //        string flpPhotourl9 = string.Empty;
        //        string flpPhotourl10 = string.Empty;
        //        string flpPhotourl11 = string.Empty;
        //        string flpPhotourl12 = string.Empty;
        //        To = Session["District"].ToString();
        //        if (txtWorkType.Text == "Line")
        //        {
        //            if (input.EndsWith("kv", StringComparison.OrdinalIgnoreCase))
        //            {
        //                string voltagePart = input.Substring(0, input.Length - 2);
        //                if (int.TryParse(voltagePart, out int voltageLevel))
        //                {
        //                    if (voltageLevel >= 11 && voltageLevel <= 33)
        //                    {
        //                        Assign = "SDO";
        //                    }
        //                    else if (voltageLevel >= 33)
        //                    {
        //                        Assign = "Admin@123";
        //                    }
        //                    else if (voltageLevel <= 11)
        //                    {
        //                        Assign = "JE";
        //                    }
        //                }
        //            }

        //            else if (input.EndsWith("v", StringComparison.OrdinalIgnoreCase))
        //            {
        //                Assign = "JE";
        //            }
        //        }
        //        else
        //        {
        //            if (input.EndsWith("KVA", StringComparison.OrdinalIgnoreCase))
        //            {
        //                string voltagePart = input.Substring(0, input.Length - 3);
        //                if (int.TryParse(voltagePart, out int voltageLevel))
        //                {
        //                    if (voltageLevel >= 250 && voltageLevel <= 500)
        //                    {
        //                        Assign = "XEN";
        //                    }
        //                    else if (voltageLevel >= 500)
        //                    {
        //                        Assign = "Admin@123";
        //                    }
        //                    else if (voltageLevel <= 250)
        //                    {
        //                        Assign = "SDO";
        //                    }
        //                }
        //            }
        //            else if (input.EndsWith("MVA", StringComparison.OrdinalIgnoreCase))
        //            {
        //                Assign = "Admin@123";
        //            }
        //        }
        //        if (LineSubstationSupplier.Visible == true)
        //        {
        //            if (FileUpload1.PostedFile.FileName.Length > 0)
        //            {
        //                FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        //                if (!Directory.Exists(Server.MapPath("~/Attachment/" + id + "/RequestLetterFromConcernedOfficer/")))
        //                {
        //                    Directory.CreateDirectory(Server.MapPath("~/Attachment/" + id + "/RequestLetterFromConcernedOfficer/"));
        //                }
        //                string ext = FileUpload1.PostedFile.FileName.Split('.')[1];
        //                string path = "";
        //                path = "/Attachment/" + id + "/RequestLetterFromConcernedOfficer/";
        //                string fileName = "RequestLetterFromConcernedOfficer" + DateTime.Now.ToString("yyyyMMddHHmmssFFF") + "." + ext;
        //                string filePathInfo2 = "";
        //                filePathInfo2 = Server.MapPath("~/Attachment/" + id + "/RequestLetterFromConcernedOfficer/" + fileName);
        //                FileUpload1.PostedFile.SaveAs(filePathInfo2);
        //                flpPhotourl = path + fileName;
        //            }
        //            if (FileUpload2.PostedFile.FileName.Length > 0)
        //            {
        //                FileName = Path.GetFileName(FileUpload2.PostedFile.FileName);
        //                if (!Directory.Exists(Server.MapPath("~/Attachment/" + id + "/ManufacturingTestReportOfEqipment/")))
        //                {
        //                    Directory.CreateDirectory(Server.MapPath("~/Attachment/" + id + "/ManufacturingTestReportOfEqipment/"));
        //                }
        //                string ext = FileUpload2.PostedFile.FileName.Split('.')[1];
        //                string path = "";
        //                path = "/Attachment/" + id + "/ManufacturingTestReportOfEqipment/";
        //                string fileName = "ManufacturingTestReportOfEqipment" + DateTime.Now.ToString("yyyyMMddHHmmssFFF") + "." + ext;
        //                string filePathInfo2 = "";
        //                filePathInfo2 = Server.MapPath("~/Attachment/" + id + "/ManufacturingTestReportOfEqipment/" + fileName);
        //                FileUpload2.PostedFile.SaveAs(filePathInfo2);
        //                flpPhotourl1 = path + fileName;
        //            }
        //        }
        //        if (SupplierSub.Visible == true)
        //        {
        //            if (FileUpload3.PostedFile.FileName.Length > 0)
        //            {
        //                FileName = Path.GetFileName(FileUpload3.PostedFile.FileName);
        //                if (!Directory.Exists(Server.MapPath("~/Attachment/" + id + "/SingleLineDiagramOfLine/")))
        //                {
        //                    Directory.CreateDirectory(Server.MapPath("~/Attachment/" + id + "/SingleLineDiagramOfLine/"));
        //                }
        //                string ext = FileUpload3.PostedFile.FileName.Split('.')[1];
        //                string path = "";
        //                path = "/Attachment/" + id + "/SingleLineDiagramOfLine/";
        //                string fileName = "SingleLineDiagramOfLine" + DateTime.Now.ToString("yyyyMMddHHmmssFFF") + "." + ext;
        //                string filePathInfo2 = "";
        //                filePathInfo2 = Server.MapPath("~/Attachment/" + id + "/SingleLineDiagramOfLine/" + fileName);
        //                FileUpload3.PostedFile.SaveAs(filePathInfo2);
        //                flpPhotourl2 = path + fileName;
        //            }
        //        }
        //        if (LinePersonal.Visible == true)
        //        {
        //            if (FileUpload12.PostedFile.FileName.Length > 0)
        //            {
        //                FileName = Path.GetFileName(FileUpload12.PostedFile.FileName);
        //                if (!Directory.Exists(Server.MapPath("~/Attachment/" + id + "/DemandNoticeOfLine/")))
        //                {
        //                    Directory.CreateDirectory(Server.MapPath("~/Attachment/" + id + "/DemandNoticeOfLine/"));
        //                }
        //                string ext = FileUpload12.PostedFile.FileName.Split('.')[1];
        //                string path = "";
        //                path = "/Attachment/" + id + "/DemandNoticeOfLine/";
        //                string fileName = "DemandNoticeOfLine" + DateTime.Now.ToString("yyyyMMddHHmmssFFF") + "." + ext;
        //                string filePathInfo2 = "";
        //                filePathInfo2 = Server.MapPath("~/Attachment/" + id + "/DemandNoticeOfLine/" + fileName);
        //                FileUpload12.PostedFile.SaveAs(filePathInfo2);
        //                flpPhotourl3 = path + fileName;
        //            }
        //        }
        //        if (PersonalSub.Visible == true)
        //        {
        //            if (FileUpload4.PostedFile.FileName.Length > 0)
        //            {
        //                FileName = Path.GetFileName(FileUpload4.PostedFile.FileName);
        //                if (!Directory.Exists(Server.MapPath("~/Attachment/" + id + "/CopyOfNoticeIssuedByUHBVNorDHBVN/")))
        //                {
        //                    Directory.CreateDirectory(Server.MapPath("~/Attachment/" + id + "/CopyOfNoticeIssuedByUHBVNorDHBVN/"));
        //                }
        //                string ext = FileUpload4.PostedFile.FileName.Split('.')[1];
        //                string path = "";
        //                path = "/Attachment/" + id + "/CopyOfNoticeIssuedByUHBVNorDHBVN/";
        //                string fileName = "CopyOfNoticeIssuedByUHBVNorDHBVN" + DateTime.Now.ToString("yyyyMMddHHmmssFFF") + "." + ext;
        //                string filePathInfo2 = "";
        //                filePathInfo2 = Server.MapPath("~/Attachment/" + id + "/CopyOfNoticeIssuedByUHBVNorDHBVN/" + fileName);
        //                FileUpload4.PostedFile.SaveAs(filePathInfo2);
        //                flpPhotourl4 = path + fileName;
        //            }
        //            if (FileUpload5.PostedFile.FileName.Length > 0)
        //            {
        //                FileName = Path.GetFileName(FileUpload5.PostedFile.FileName);
        //                if (!Directory.Exists(Server.MapPath("~/Attachment/" + id + "/InvoiceOfTransferOfPersonalSubstation/")))
        //                {
        //                    Directory.CreateDirectory(Server.MapPath("~/Attachment/" + id + "/InvoiceOfTransferOfPersonalSubstation/"));
        //                }
        //                string ext = FileUpload5.PostedFile.FileName.Split('.')[1];
        //                string path = "";
        //                path = "/Attachment/" + id + "/InvoiceOfTransferOfPersonalSubstation/";
        //                string fileName = "InvoiceOfTransferOfPersonalSubstation" + DateTime.Now.ToString("yyyyMMddHHmmssFFF") + "." + ext;
        //                string filePathInfo2 = "";
        //                filePathInfo2 = Server.MapPath("~/Attachment/" + id + "/InvoiceOfTransferOfPersonalSubstation/" + fileName);
        //                FileUpload5.PostedFile.SaveAs(filePathInfo2);
        //                flpPhotourl5 = path + fileName;
        //            }
        //            if (FileUpload6.PostedFile.FileName.Length > 0)
        //            {
        //                FileName = Path.GetFileName(FileUpload6.PostedFile.FileName);
        //                if (!Directory.Exists(Server.MapPath("~/Attachment/" + id + "/ManufacturingTestCertificateOfTransformer/")))
        //                {
        //                    Directory.CreateDirectory(Server.MapPath("~/Attachment/" + id + "/ManufacturingTestCertificateOfTransformer/"));
        //                }
        //                string ext = FileUpload6.PostedFile.FileName.Split('.')[1];
        //                string path = "";
        //                path = "/Attachment/" + id + "/ManufacturingTestCertificateOfTransformer/";
        //                string fileName = "ManufacturingTestCertificateOfTransformer" + DateTime.Now.ToString("yyyyMMddHHmmssFFF") + "." + ext;
        //                string filePathInfo2 = "";
        //                filePathInfo2 = Server.MapPath("~/Attachment/" + id + "/ManufacturingTestCertificateOfTransformer/" + fileName);
        //                FileUpload6.PostedFile.SaveAs(filePathInfo2);
        //                flpPhotourl6 = path + fileName;
        //            }
        //            if (FileUpload7.PostedFile.FileName.Length > 0)
        //            {
        //                FileName = Path.GetFileName(FileUpload7.PostedFile.FileName);
        //                if (!Directory.Exists(Server.MapPath("~/Attachment/" + id + "/SingleLineDiagramofTransformer/")))
        //                {
        //                    Directory.CreateDirectory(Server.MapPath("~/Attachment/" + id + "/SingleLineDiagramofTransformer/"));
        //                }
        //                string ext = FileUpload7.PostedFile.FileName.Split('.')[1];
        //                string path = "";
        //                path = "/Attachment/" + id + "/SingleLineDiagramofTransformer/";
        //                string fileName = "SingleLineDiagramofTransformer" + DateTime.Now.ToString("yyyyMMddHHmmssFFF") + "." + ext;
        //                string filePathInfo2 = "";
        //                filePathInfo2 = Server.MapPath("~/Attachment/" + id + "/SingleLineDiagramofTransformer/" + fileName);
        //                FileUpload7.PostedFile.SaveAs(filePathInfo2);
        //                flpPhotourl7 = path + fileName;
        //            }
        //            if (FileUpload8.PostedFile.FileName.Length > 0)
        //            {
        //                FileName = Path.GetFileName(FileUpload8.PostedFile.FileName);
        //                if (!Directory.Exists(Server.MapPath("~/Attachment/" + id + "/InvoiceoffireExtinguisheratSite/")))
        //                {
        //                    Directory.CreateDirectory(Server.MapPath("~/Attachment/" + id + "/InvoiceoffireExtinguisheratSite/"));
        //                }
        //                string ext = FileUpload8.PostedFile.FileName.Split('.')[1];
        //                string path = "";
        //                path = "/Attachment/" + id + "/InvoiceoffireExtinguisheratSite/";
        //                string fileName = "InvoiceoffireExtinguisheratSite" + DateTime.Now.ToString("yyyyMMddHHmmssFFF") + "." + ext;
        //                string filePathInfo2 = "";
        //                filePathInfo2 = Server.MapPath("~/Attachment/" + id + "/InvoiceoffireExtinguisheratSite/" + fileName);
        //                FileUpload8.PostedFile.SaveAs(filePathInfo2);
        //                flpPhotourl8 = path + fileName;
        //            }
        //        }
        //        if (PersonalGenerating.Visible == true)
        //        {
        //            if (FileUpload9.PostedFile.FileName.Length > 0)
        //            {
        //                FileName = Path.GetFileName(FileUpload9.PostedFile.FileName);
        //                if (!Directory.Exists(Server.MapPath("~/Attachment/" + id + "/InvoiceOfDGSetOfGeneratingSet/")))
        //                {
        //                    Directory.CreateDirectory(Server.MapPath("~/Attachment/" + id + "/InvoiceOfDGSetOfGeneratingSet/"));
        //                }
        //                string ext = FileUpload9.PostedFile.FileName.Split('.')[1];
        //                string path = "";
        //                path = "/Attachment/" + id + "/InvoiceOfDGSetOfGeneratingSet/";
        //                string fileName = "InvoiceOfDGSetOfGeneratingSet" + DateTime.Now.ToString("yyyyMMddHHmmssFFF") + "." + ext;
        //                string filePathInfo2 = "";
        //                filePathInfo2 = Server.MapPath("~/Attachment/" + id + "/InvoiceOfDGSetOfGeneratingSet/" + fileName);
        //                FileUpload9.PostedFile.SaveAs(filePathInfo2);
        //                flpPhotourl9 = path + fileName;
        //            }
        //            if (FileUpload10.PostedFile.FileName.Length > 0)
        //            {
        //                FileName = Path.GetFileName(FileUpload10.PostedFile.FileName);
        //                if (!Directory.Exists(Server.MapPath("~/Attachment/" + id + "/ManufacturingCerificateOfDGSet/")))
        //                {
        //                    Directory.CreateDirectory(Server.MapPath("~/Attachment/" + id + "/ManufacturingCerificateOfDGSet/"));
        //                }
        //                string ext = FileUpload10.PostedFile.FileName.Split('.')[1];
        //                string path = "";
        //                path = "/Attachment/" + id + "/ManufacturingCerificateOfDGSet/";
        //                string fileName = "ManufacturingCerificateOfDGSet" + DateTime.Now.ToString("yyyyMMddHHmmssFFF") + "." + ext;
        //                string filePathInfo2 = "";
        //                filePathInfo2 = Server.MapPath("~/Attachment/" + id + "/ManufacturingCerificateOfDGSet/" + fileName);
        //                FileUpload10.PostedFile.SaveAs(filePathInfo2);
        //                flpPhotourl10 = path + fileName;
        //            }
        //            if (FileUpload13.PostedFile.FileName.Length > 0)
        //            {
        //                FileName = Path.GetFileName(FileUpload13.PostedFile.FileName);
        //                if (!Directory.Exists(Server.MapPath("~/Attachment/" + id + "/InvoiceOfExptinguisherOrApparatusAtsite/")))
        //                {
        //                    Directory.CreateDirectory(Server.MapPath("~/Attachment/" + id + "/InvoiceOfExptinguisherOrApparatusAtsite/"));
        //                }
        //                string ext = FileUpload13.PostedFile.FileName.Split('.')[1];
        //                string path = "";
        //                path = "/Attachment/" + id + "/InvoiceOfExptinguisherOrApparatusAtsite/";
        //                string fileName = "InvoiceOfExptinguisherOrApparatusAtsite" + DateTime.Now.ToString("yyyyMMddHHmmssFFF") + "." + ext;
        //                string filePathInfo2 = "";
        //                filePathInfo2 = Server.MapPath("~/Attachment/" + id + "/InvoiceOfExptinguisherOrApparatusAtsite/" + fileName);
        //                FileUpload13.PostedFile.SaveAs(filePathInfo2);
        //                flpPhotourl11 = path + fileName;
        //            }
        //            if (FileUpload11.PostedFile.FileName.Length > 0)
        //            {
        //                FileName = Path.GetFileName(FileUpload11.PostedFile.FileName);
        //                if (!Directory.Exists(Server.MapPath("~/Attachment/" + id + "/StructureStabilityResolvedByAuthorizedEngineer/")))
        //                {
        //                    Directory.CreateDirectory(Server.MapPath("~/Attachment/" + id + "/StructureStabilityResolvedByAuthorizedEngineer/"));
        //                }
        //                string ext = FileUpload11.PostedFile.FileName.Split('.')[1];
        //                string path = "";
        //                path = "/Attachment/" + id + "/StructureStabilityResolvedByAuthorizedEngineer/";
        //                string fileName = "StructureStabilityResolvedByAuthorizedEngineer" + DateTime.Now.ToString("yyyyMMddHHmmssFFF") + "." + ext;
        //                string filePathInfo2 = "";
        //                filePathInfo2 = Server.MapPath("~/Attachment/" + id + "/StructureStabilityResolvedByAuthorizedEngineer/" + fileName);
        //                FileUpload11.PostedFile.SaveAs(filePathInfo2);
        //                flpPhotourl12 = path + fileName;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alert('Select PDF Files only')", true);
        //    }
        //}
        protected void lnkReturn_Command(object sender, CommandEventArgs e)
        {
            string[] args = e.CommandArgument.ToString().Split(',');
            string inspectionId = args[0];
            string inspectionCount = args.Length > 1 ? args[1] : "0";
            InspectionReturnDetails.GetReturnDetails(inspectionId, inspectionCount);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowReturnModal", "$('#ownerModal').modal('show');", true);
        }

        protected void RejectionDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (RejectionDDL.SelectedValue == "1" || RejectionDDL.SelectedValue == "2")
            //{
            //    RejectionFileDiv.Visible = true;
            //}
            //else
            //{
            //    RejectionFileDiv.Visible = false;
            //}

        }
    }
}