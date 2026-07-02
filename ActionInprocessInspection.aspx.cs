 using AjaxControlToolkit;
using CEI_PRoject;
using CEIHaryana.Model.Industry;
using iText.StyledXmlParser.Jsoup.Nodes;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Label = System.Web.UI.WebControls.Label;

namespace CEIHaryana.Admin
{
    public partial class ActionInprocessInspection : System.Web.UI.Page
    {
        CEI CEI = new CEI();
        private static int lineNumber = 0;
        IndustryApiLogDetails logDetails = new IndustryApiLogDetails();
        private static string ApprovedorReject, Reason, StaffId, Suggestions, ExNotes;
        string Type = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["AdminID"] != null && Session["AdminID"].ToString() != "")
                    {
                        lineNumber = 0;
                        GetData();
                        //Changes of neeraj Suggestions Merged on 8-May-2025
                        BindSuggestions();
                        if (Type == "New")
                        {
                            GetTestReportData();
                        }
                        else if (Type == "Periodic")
                        {
                            GetTestReportDataIfPeriodic();
                        }
                        else
                        {
                            Session["AdminId"] = "";
                            Response.Redirect("/AdminLogout.aspx", false);
                        }

                        //GetTestReportData();
                        btnPreview.Visible = false;
                        //Changes of neeraj Suggestions Merged on 8-May-2025
                        btnSuggestions.Visible = false;
                        Page.Session["ClickCount"] = "0";
                    }
                }
            }
            catch
            {
                Response.Redirect("/AdminLogout.aspx");
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
        //Changes of neeraj Suggestions Merged on 8-May-2025
        private void BindSuggestions()
        {
            try
            {
                DataSet dsSuggestion = new DataSet();
                dsSuggestion = CEI.GetSuggestions();
                ddlSuggestion.DataSource = dsSuggestion;
                ddlSuggestion.DataTextField = "Suggestions";
                ddlSuggestion.DataValueField = "Suggestions";
                ddlSuggestion.DataBind();
                ddlSuggestion.Items.Insert(0, new ListItem("Select", "0"));
                dsSuggestion.Clear();
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert()", "alert('" + ex.Message.ToString() + "')", true);
                return;
            }
        }
        //

        private void GetTestReportDataIfPeriodic()
        {
            try
            {
                ID = Session["InProcessInspectionId"].ToString();
                DataSet ds = new DataSet();
                //commented Condition only by gurmeet 1May
                //ds = CEI.GetTestReportDataIfPeriodic(ID);
                ds = CEI.GetInspectionHistoryLogs(ID);
                string TestRportId = string.Empty;
                if (ds != null && ds.Tables.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    string script = "alert(\"No Record Found\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                ds.Dispose();
            }

            //Changes of neeraj Suggestions Merged on 8-May-2025
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert()", "alert('" + ex.Message.ToString() + "')", true);
                return;
            }
        }

        protected void lnkRedirect_Click(object sender, EventArgs e)
        {

            LinkButton lnkRedirect = (LinkButton)sender;
            string testReportId = lnkRedirect.CommandArgument;
            Session["InspectionTestReportId"] = testReportId;
            if (txtWorkType.Text.Trim() == "Line")
            {
                Response.Write("<script>window.open('/TestReportModal/LineTestReportModal.aspx','_blank');</script>");
            }
            else if (txtWorkType.Text.Trim() == "Substation Transformer")
            {
                Response.Write("<script>window.open('/TestReportModal/SubstationTransformerTestReportModal.aspx','_blank');</script>");
            }
            else if (txtWorkType.Text.Trim() == "Generating Set")
            {
                Response.Write("<script>window.open('/TestReportModal/GeneratingSetTestReportModal.aspx','_blank');</script>");
            }
            }
        private void GetData()
        {
            try
            {
                ID = Session["InProcessInspectionId"].ToString();
                GetInspectionHistoryandPan(ID);
                DataSet ds = new DataSet();
                ds = CEI.InspectionData(ID);
                Type = ds.Tables[0].Rows[0]["IType"].ToString();
                HyperLink1.Text = ds.Tables[0].Rows[0]["WhoCreated"].ToString();
                lbltype.Text = ds.Tables[0].Rows[0]["IType"].ToString();
                if (Type == "New")
                {
                    txtInspectionReportID.Text = ds.Tables[0].Rows[0]["Id"].ToString();
                    //Added By gurmeet 26-July-2025
                    //txtPremises.Text = ds.Tables[0].Rows[0]["Inspectiontype"].ToString();
                    string Premises = ds.Tables[0].Rows[0]["Inspectiontype"].ToString();
                    if (!string.IsNullOrEmpty(Premises))
                    {
                        txtPremises.Text = Premises;
                    }
                    else
                    {
                        txtPremises.Text = "     -";
                    }
                    txtSiteOwnerContact.Text = ds.Tables[0].Rows[0]["SiteownerContactNumber"].ToString();
                    txtContractorName.Text = ds.Tables[0].Rows[0]["ContractorName"].ToString();
                    txtContractorPhoneNo.Text = ds.Tables[0].Rows[0]["ContractorContactNo"].ToString();
                    txtContractorEmail.Text = ds.Tables[0].Rows[0]["ContractorEmail"].ToString();
                    txtSupervisorName.Text = ds.Tables[0].Rows[0]["SupervisorName"].ToString();
                    txtSupervisorEmail.Text = ds.Tables[0].Rows[0]["SupervisorEmail"].ToString();
                    txtTransactionId.Text = ds.Tables[0].Rows[0]["TransactionId"].ToString();
                    txtTranscationDate.Text = ds.Tables[0].Rows[0]["TransactionDate1"].ToString();
                    txtAmount.Text = ds.Tables[0].Rows[0]["TotalAmount"].ToString();
                    //
                    txtApplicantType.Text = ds.Tables[0].Rows[0]["TypeOfApplicant"].ToString();
                    txtWorkType.Text = ds.Tables[0].Rows[0]["TypeOfInstallation"].ToString();
                    Session["InstallationType"] = txtWorkType.Text;
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
                    txtSiteOwnerName.Text = ds.Tables[0].Rows[0]["OwnerName"].ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0]["SiteownerAddress"].ToString();
                    txtDistrict.Text = ds.Tables[0].Rows[0]["District"].ToString();
                  
                    txtTestReportId.Text = ds.Tables[0].Rows[0]["TestRportId"].ToString();
                    string SiteInspectionDate = ds.Tables[0].Rows[0]["InspectionDate"].ToString();
                    Session["InspectionType"] = ds.Tables[0].Rows[0]["Type_of_Inspection"].ToString();
                    GridBindDocument();


                    DivTRinMultipleCaseNew.Visible = true;
                    GetGridNewInspectionMultiple();

                    string Status = ds.Tables[0].Rows[0]["ApplicationStatus"].ToString();

                    if (Status == "Approved")
                    {
                        InspectionDate.Visible = false;
                        txtSuggestion.Text = ds.Tables[0].Rows[0]["Suggestion"].ToString();
                        if (!string.IsNullOrEmpty(txtSuggestion.Text))
                        {
                            Suggestion.Visible = true;
                            txtSuggestion.ReadOnly = true;
                        }
                        ddlReview.SelectedIndex = ddlReview.Items.IndexOf(ddlReview.Items.FindByText(Status));
                        ddlReview.Attributes.Add("disabled", "true");
                        txtSuggestion.Attributes.Add("disabled", "true");

                        if (!string.IsNullOrEmpty(SiteInspectionDate))
                        {
                            InspectionDate.Visible = true;
                            txtInspectionDate.Text = DateTime.Parse(SiteInspectionDate).ToString("yyyy-MM-dd");
                            txtInspectionDate.Attributes.Add("disabled", "true");
                        }
                        btnBack.Visible = true;
                        btnSubmit.Visible = false;
                    }
                    if (Status == "Rejected")
                    {
                        InspectionDate.Visible = false;

                        if (!string.IsNullOrEmpty(SiteInspectionDate))
                        {
                            InspectionDate.Visible = true;
                            txtInspectionDate.Text = DateTime.Parse(SiteInspectionDate).ToString("yyyy-MM-dd");
                            txtInspectionDate.Attributes.Add("disabled", "true");
                        }
                        Rejection.Visible = true;
                        txtRejected.Text = ds.Tables[0].Rows[0]["ReasonForRejection"].ToString();
                        ddlReview.SelectedIndex = ddlReview.Items.IndexOf(ddlReview.Items.FindByText(Status));
                        //ApprovedReject.Visible = true;
                        //ApprovalRequired.Visible = false;
                        ddlReview.Attributes.Add("disabled", "true");
                        txtRejected.Attributes.Add("disabled", "true");
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
                        btnBack.Visible = true;
                        btnSubmit.Visible = false;
                    }
                    if (Status == "Return")
                    {
                        InspectionDate.Visible = false;
                        ApprovalRequired.Visible = false;
                        btnSubmit.Visible = false;

                        // Rejection.Visible = true;
                        //txtRejected.Text = ds.Tables[0].Rows[0]["ReturnRemarks"].ToString();
                        ddlReview.Attributes.Add("disabled", "true");
                        //txtRejected.Attributes.Add("disabled", "true");
                        //txtRejected.ReadOnly = true;

                    }
                }
                else if (Type == "Periodic")
                {
                    txtDistrict.Text = ds.Tables[0].Rows[0]["District"].ToString();
                    txtInspectionReportID.Text = ds.Tables[0].Rows[0]["Id"].ToString();
                    InspectionType.Visible = false;
                    txtApplicantType.Text = ds.Tables[0].Rows[0]["TypeOfApplicant"].ToString();
                    txtWorkType.Text = ds.Tables[0].Rows[0]["TypeOfInstallation"].ToString();
                    Session["InstallationType"] = txtWorkType.Text;
                    if (txtWorkType.Text == "Line")
                    {
                        Capacity.Visible = false;
                        LineVoltage.Visible = false;
                        txtLineVoltage.Text = ds.Tables[0].Rows[0]["Capacity"].ToString();
                    }
                    else
                    {
                        LineVoltage.Visible = false;
                        Capacity.Visible = true;
                        txtCapacity.Text = ds.Tables[0].Rows[0]["Capacity"].ToString();
                    }
                    txtVoltage.Text = ds.Tables[0].Rows[0]["VoltageLevel"].ToString();
                    txtSiteOwnerName.Text = ds.Tables[0].Rows[0]["OwnerName"].ToString();
                    Session["InspectionType"] = ds.Tables[0].Rows[0]["Type_of_Inspection"].ToString();
                    //added by gurmeet 18 july
                    string Premises = ds.Tables[0].Rows[0]["Inspectiontype"].ToString();
                    if (!string.IsNullOrEmpty(Premises))
                    {
                        txtPremises.Text = Premises;
                    }
                    else
                    {
                        txtPremises.Text = "     -";
                    }                   
                    txtContactPersonEmail.Text = ds.Tables[0].Rows[0]["SiteownerEmail"].ToString();                  
                    txtSiteOwnerContact.Text = ds.Tables[0].Rows[0]["SiteownerContactNumber"].ToString();
                    txtContractorName.Text = ds.Tables[0].Rows[0]["ContractorName"].ToString();
                    txtContractorPhoneNo.Text = ds.Tables[0].Rows[0]["ContractorContactNo"].ToString();
                    txtContractorEmail.Text = ds.Tables[0].Rows[0]["ContractorEmail"].ToString();
                    txtSupervisorName.Text = ds.Tables[0].Rows[0]["SupervisorName"].ToString();
                    txtSupervisorEmail.Text = ds.Tables[0].Rows[0]["SupervisorEmail"].ToString();
                    txtTransactionId.Text = ds.Tables[0].Rows[0]["TransactionId"].ToString();
                    txtTranscationDate.Text = ds.Tables[0].Rows[0]["TransactionDate1"].ToString();
                    txtAmount.Text = ds.Tables[0].Rows[0]["TotalAmount"].ToString();
                    //divTestReportAttachment.Visible = false;
                    //Address.Visible = false;
                    //SiteOwnerContact.Visible = false;
                    //ContractorName.Visible = false;
                    //ContractorPhoneNo.Visible = false;
                    //ContractorEmail.Visible = false;
                    //divSupervisorName.Visible = false;
                    //SupervisorEmail.Visible = false;
                    //end here gurmeet
                    string SiteInspectionDate = ds.Tables[0].Rows[0]["InspectionDate"].ToString();
                    grd_Documemnts.Columns[1].Visible = true;

                    //GridView1.Columns[5].Visible = false; commet by gurmeet 29 aprail
                   // GridView1.Columns[3].Visible = false;

                    GridBindDocument();
                    DivTestReports.Visible = true;
                    GridToViewCart();

                    string Status = ds.Tables[0].Rows[0]["ApplicationStatus"].ToString();

                    if (Status == "Approved")
                    {
                        InspectionDate.Visible = false;
                        txtSuggestion.Text = ds.Tables[0].Rows[0]["Suggestion"].ToString();
                        if (!string.IsNullOrEmpty(txtSuggestion.Text))
                        {
                            Suggestion.Visible = true;
                            txtSuggestion.ReadOnly = true;
                        }
                        ddlReview.SelectedIndex = ddlReview.Items.IndexOf(ddlReview.Items.FindByText(Status));
                        ddlReview.Attributes.Add("disabled", "true");
                        txtSuggestion.Attributes.Add("disabled", "true");

                        if (!string.IsNullOrEmpty(SiteInspectionDate))
                        {
                            InspectionDate.Visible = true;
                            txtInspectionDate.Text = DateTime.Parse(SiteInspectionDate).ToString("yyyy-MM-dd");
                            txtInspectionDate.Attributes.Add("disabled", "true");
                        }
                        btnBack.Visible = true;
                        btnSubmit.Visible = false;
                    }
                    if (Status == "Rejected")
                    {
                        InspectionDate.Visible = false;

                        if (!string.IsNullOrEmpty(SiteInspectionDate))
                        {
                            InspectionDate.Visible = true;
                            txtInspectionDate.Text = DateTime.Parse(SiteInspectionDate).ToString("yyyy-MM-dd");
                            txtInspectionDate.Attributes.Add("disabled", "true");
                        }
                        Rejection.Visible = true;
                        txtRejected.Text = ds.Tables[0].Rows[0]["ReasonForRejection"].ToString();
                        ddlReview.SelectedIndex = ddlReview.Items.IndexOf(ddlReview.Items.FindByText(Status));
                        ddlReview.Attributes.Add("disabled", "true");
                        txtRejected.Attributes.Add("disabled", "true");
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
                        btnBack.Visible = true;
                        btnSubmit.Visible = false;
                    }
                    if (Status == "Return")
                    {
                        InspectionDate.Visible = false;
                        ApprovalRequired.Visible = false;
                        btnSubmit.Visible = false;
                        ddlReview.Attributes.Add("disabled", "true");
                    }
                }
            }
            catch (Exception ex)
            {

                //Changes of neeraj Suggestions Merged on 8-May-2025
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert()", "alert('" + ex.Message.ToString() + "')", true);
                return;
            }
        }
        protected void grd_Documemnts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string fileName = "";
            try
            {
                if (e.CommandName == "Select")
                {
                    //ID = Session["InspectionId"].ToString();
                    fileName = "https://ceiharyana.com" + e.CommandArgument.ToString();
                    string script = $@"<script>window.open('{fileName}','_blank');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "OpenFileInNewTab", script);

                }

            }
            catch (Exception ex)
            {

                //Changes of neeraj Suggestions Merged on 8-May-2025
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert()", "alert('" + ex.Message.ToString() + "')", true);
                return;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int ClickCount = 0;
            ClickCount = Convert.ToInt32(Session["ClickCount"]);
            if (ClickCount < 1)
            {
                //ClickCount = ClickCount + 1;
                //Session["ClickCount"] = ClickCount;
                int checksuccessmessage = 0;
                try
                {
                    if (Session["InProcessInspectionId"].ToString() != null && Session["InProcessInspectionId"].ToString() != "" && Session["AdminID"].ToString() != null)
                    {
                        StaffId = Session["AdminID"].ToString();
                        ID = Session["InProcessInspectionId"].ToString();

                        //Changes of neeraj with Suggestions Merged on 8-May-2025
                        String SubmittedDated = "";// hnSubmittedDate.Value;
                        if (ddlReview.SelectedValue != null && ddlReview.SelectedValue != "" && ddlReview.SelectedValue != "0")
                        {
                            //Commented by gurmeet 19-May-2025
                            foreach (GridViewRow row in GridView1.Rows)
                            {
                                if (row.RowType == DataControlRowType.DataRow)
                                {                                   
                                    //Label lblstatus = (Label)row.FindControl("lblStatus");
                                    Label lblSubmittedDate = (Label)row.FindControl("lblSubmittedDate");
                                    SubmittedDated = lblSubmittedDate.Text;
                                }
                            }
                            //
                            DateTime inspectionDate;

                            if (!DateTime.TryParse(txtInspectionDate.Text, out inspectionDate))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alert('Invalid inspection date.');", true);
                                return;
                            }


                            DateTime submittedDate;

                            //Changes of neeraj with Suggestions Merged on 8-May-2025
                            if (!DateTime.TryParse(SubmittedDated, out submittedDate))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alert('Invalid submitted date.');", true);
                                return;
                            }

                            //Changes of neeraj with Suggestions Merged on 8-May-2025
                            DateTime serverDate = DateTime.Now.Date;
                            if (inspectionDate.Date > serverDate.Date)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alert('Inspection date must be less than Today date.');", true);
                                return;
                            }
                            if (inspectionDate.Date < submittedDate.Date)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alert('Inspection/Approval date must be greater or equal to the last action date of application.');", true);
                                return;
                            }                           

                            ClickCount = ClickCount + 1;
                            Session["ClickCount"] = ClickCount;

                            ApprovedorReject = ddlReview.SelectedItem.ToString();
                            Reason = string.IsNullOrEmpty(txtRejected.Text) ? null : txtRejected.Text.Trim();

                            if (Suggestion.Visible == true)
                            {
                                Suggestions = string.IsNullOrEmpty(txtSuggestion.Text) ? null : txtSuggestion.Text.Trim();
                            }

                            //Changes of neeraj Notes Merged on 8-May-2025
                            if (ExNote.Visible == true)
                            {
                                ExNotes = string.IsNullOrEmpty(txtNote.Text) ? null : txtNote.Text.Trim();
                            }
                            string RejectionType = null;

                            if (RejectionRemarksDDL.Visible == true)
                            {
                                RejectionType = RejectionDDL.SelectedValue == "0" ? null : RejectionDDL.SelectedItem.Text;
                            }

                            string RejectionImage = null;

                            if (RejectionFileDiv.Visible == true && RejectionFileUpload.HasFile)
                            {
                                try
                                {
                                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

                                    int maxFileSize = 1 * 1024 * 1024;

                                    string fileExtension = Path.GetExtension(RejectionFileUpload.FileName).ToLower();

                                    if (!allowedExtensions.Contains(fileExtension))
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Only image files (JPG, JPEG, PNG) are allowed.');", true);
                                        return;
                                    }

                                    if (RejectionFileUpload.PostedFile.ContentLength > maxFileSize)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Image size must be less than 1 MB.');", true);
                                        return;
                                    }

                                    string folderPath = Server.MapPath("~/Attachment/Officers/InspectionAttachment/" + ID + "/");

                                    if (!Directory.Exists(folderPath))
                                    {
                                        Directory.CreateDirectory(folderPath);
                                    }

                                    string uniqueFileName = ID + "_RejectionImg_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;

                                    string filePath = Path.Combine(folderPath, uniqueFileName);

                                    string virtualPath = "~/Attachment/Officers/InspectionAttachment/" + ID + "/" + uniqueFileName;
                                    RejectionFileUpload.SaveAs(filePath);


                                    RejectionImage = virtualPath;
                                }
                                catch
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                                        "alert('Error while uploading image.');", true);
                                }
                            }

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
                                //Changes of neeraj Notes Merged on 8-May-2025
                                txtInspectionDate.Text = DateTime.Parse(txtInspectionDate.Text).ToString("yyyy-MM-dd");

                                CEI.InspectionFinalAction(ID, StaffId, ApprovedorReject , RejectionType, RejectionImage, Reason, Suggestions, txtInspectionDate.Text, ExNotes);
                                if (ApprovedorReject.Trim() == "Approved")
                                {
                                    CEI.InsertApprovedCertificatedata(ID);
                                }
                                checksuccessmessage = 1;

                                string actiontype = ApprovedorReject == "Approved" ? "Approved" : "Rejected";
                                List<Industry_Api_Post_DataformatModel> ApiPostformatResults = CEI.GetIndustry_OutgoingRequestFormat(Convert.ToInt32(ID), actiontype);
                                foreach (var ApiPostformatresult in ApiPostformatResults)
                                {
                                    if (ApiPostformatresult.PremisesType == "Industry")
                                    {
                                        string accessToken = TokenManagerConst.GetAccessToken(ApiPostformatresult);

                                        logDetails = CEI.Post_Industry_Inspection_StageWise_JsonData(
                                            "https://investharyana.in/api/project-service-logs-external_UHBVN",
                                            new Industry_Inspection_StageWise_JsonDataFormat_Model
                                            {
                                                actionTaken = ApiPostformatresult.ActionTaken,
                                                commentByUserLogin = ApiPostformatresult.CommentByUserLogin,
                                                //commentDate = ApiPostformatresult.CommentDate,
                                                commentDate = ApiPostformatresult.CommentDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                                                comments = ApiPostformatresult.Comments,
                                                id = ApiPostformatresult.Id,
                                                projectid = ApiPostformatresult.ProjectId,
                                                serviceid = ApiPostformatresult.ServiceId,
                                                //projectid = "245df444-1808-4ff6-8421-cf4a859efb4c",
                                                //serviceid = "e31ee2a6-3b99-4f42-b61d-38cd80be45b6"
                                                certificateNumber = ApiPostformatresult.CertificateNumber,
                                                certificateIssueDate = ApiPostformatresult.CertificateIssueDate?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                                                certificateExpireDate = ApiPostformatresult.CertificateExpireDate?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                                            },
                                            ApiPostformatresult,
                                            accessToken
                                        );

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

                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alertWithRedirectdata('" + ApprovedorReject + "');", true);
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
                                // ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alert('" + ex.Message.ToString() + "')", true);
                                //  ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", $"alert('{errorMessage}')", true);
                            }
                            catch (Exception ex)
                            {

                                // Handle the exception, log it, etc.
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alert('An error occurred.');", true);
                            }
                            finally
                            {
                                if (checksuccessmessage == 1)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alertWithRedirectdata('" + ApprovedorReject + "');", true);
                                }
                            }

                        }
                        else
                        {
                            ddlReview.Focus();
                            return;
                        }
                    }
                    else
                    {
                        Response.Redirect("/AdminLogout.aspx");
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert()", "alert('" + ex.Message.ToString() + "')", true);
                    return;


                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessage", "alert('You double click on Button.'); window.location='AdminMaster.aspx'", true);
            }
           

        }
        private void GridToViewCart()
        {
            try
            {
                string ID = Session["InProcessInspectionId"].ToString();
                DataSet dsVC = CEI.GetDetailsToViewCart(ID);

                if (dsVC != null && dsVC.Tables.Count > 0 && dsVC.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = dsVC;
                    GridView2.DataBind();
                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                    string script = "alert('No Record Found');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert()", "alert('" + ex.Message.ToString() + "')", true);
                return;
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert()", "alert('" + ex.Message.ToString() + "')", true);
                return;
            }
        }
        protected void ddlSuggestion_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Changes of neeraj Suggestions Merged on 8-May-2025
            if (string.IsNullOrEmpty(txtSuggestion.Text) || txtSuggestion.Text == "\r\n")
            {
                lineNumber = 0;
            }
            if (lineNumber == 0)
            {
                lineNumber = 1;
            }
            else
            {
                lineNumber++;
            }
            string selectedItemText = ddlSuggestion.SelectedItem.Text;
            if (lineNumber > 4)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alert('Suggestion can\'t be more than 4 suggestions.');", true);
                return;

            }
            txtSuggestion.Text += lineNumber + ". " + selectedItemText + "\n";
            ddlSuggestion.Items.Remove(ddlSuggestion.SelectedItem);
            //lineNumber++;
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlReview.SelectedValue == "1")
                {
                    if (Session["InProcessInspectionId"].ToString() != null && Session["InProcessInspectionId"].ToString() != "")
                    {
                        ID = Session["InProcessInspectionId"].ToString();
                        string InspectionType = Session["InspectionType"].ToString();

                        //Changes of neeraj Suggestions Merged on 8-May-2025
                        string InstallationType = Session["InstallationType"].ToString();


                        SqlCommand cmd = new SqlCommand("Sp_insertTempInspection");
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
                        cmd.Connection = con;
                        if (con.State == ConnectionState.Closed)
                        {
                            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                            con.Open();
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@inspectionId", ID);
                        cmd.Parameters.AddWithValue("@suggestion", txtSuggestion.Text.Trim());
                        cmd.Parameters.AddWithValue("@ReasionRejection", txtRejected.Text == null ? null : txtRejected.Text);
                        cmd.Parameters.AddWithValue("@InspectionType", InspectionType);
                        DateTime initialIssueDate;
                        if (DateTime.TryParse(txtInspectionDate.Text, out initialIssueDate) && initialIssueDate != DateTime.MinValue)
                        {
                            cmd.Parameters.AddWithValue("@inspectionDate", initialIssueDate);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@inspectionDate", DBNull.Value);
                        }

                        //Changes of neeraj Suggestions Merged on 8-May-2025
                        cmd.Parameters.AddWithValue("@Note", txtNote.Text.Trim());
                        int x = cmd.ExecuteNonQuery();
                        con.Close();
                        if (x > 0)
                        {
                            btnPreview.Visible = false;
                            btnSuggestions.Visible = true;
                            if (InspectionType == "New")
                            {                              
                                Response.Redirect("/Print_Forms/NewInspectionApprovalCertificate.aspx", false);
                                
                            }
                            else
                            {
                                Response.Redirect("/Print_Forms/PeriodicApprovalCertificate.aspx", false);
                            }
                        }
                        //btnPreview.Visible = false;
                    }
                    // }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert()", "alert('" + ex.Message.ToString() + "')", true);
                return;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/ActionInspectioHistrory.aspx", false);
        }
        //Commented by gurmeet 19-May-2025
        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //commented Condition only by gurmeet 1May
        //        //string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
        //        string status = DataBinder.Eval(e.Row.DataItem, "ActionTaken").ToString();
        //        Label lblSubmittedDate = (Label)e.Row.FindControl("lblSubmittedDate");
        //        hnSubmittedDate.Value = lblSubmittedDate.Text;
        //        if (status == "RETURN")
        //        {
        //            e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
        //        }

        //    }
        //    if (e.Row.RowType == DataControlRowType.Header)
        //    {
        //        //e.Row.Cells[2].BackColor = System.Drawing.Color.Blue;
        //        e.Row.Cells[2].BackColor = ColorTranslator.FromHtml("#9292cc");
        //    }
        //}
        //
        protected void ddlReview_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Changes of neeraj Suggestions Merged on 8-May-2025
            Rejection.Visible = false;
            Suggestion.Visible = false;
            ddlSuggestions.Visible = false;
            btnPreview.Visible = false;
            btnSuggestions.Visible = false;
            if (ddlReview.SelectedValue == "2")
            {
                Rejection.Visible = true;
                txtSuggestion.Text = "";
                txtNote.Text = "";
                ExNote.Visible = false;
                btnPreview.Visible = false;
                btnSuggestions.Visible = false;
                Note.Visible = false;
                RejectionRemarksDDL.Visible = true;
            }
            else if (ddlReview.SelectedValue == "1")
            {
                btnSuggestions.Visible = true;
                btnPreview.Visible = true;
                Note.Visible = true;
                ExNote.Visible = true;
                ddlSuggestions.Visible = true;
                Suggestion.Visible = true;
                RejectionRemarksDDL.Visible = false;
                RejectionDDL.SelectedValue = "0";
                RejectionFileDiv.Visible = false;

            }
        }
        protected void RejectionDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RejectionDDL.SelectedValue == "1" || RejectionDDL.SelectedValue == "2")
            {
                RejectionFileDiv.Visible = true;
            }
            else
            {
                RejectionFileDiv.Visible = false;
            }

        }

        protected void GridBindDocument()
        {
            try
            {
                ID = Session["InProcessInspectionId"].ToString();
                DataSet ds = new DataSet();
                ds = CEI.ViewDocuments(ID);
                if (ds.Tables.Count > 0)
                {
                    grd_Documemnts.DataSource = ds;
                    grd_Documemnts.DataBind();
                }
                else
                {
                    grd_Documemnts.DataSource = null;
                    grd_Documemnts.DataBind();
                    string script = "alert(\"No Record Found\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                ds.Dispose();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert()", "alert('" + ex.Message.ToString() + "')", true);
                return;
            }
        }

        private void GetTestReportData()
        {
            try
            {
                ID = Session["InProcessInspectionId"].ToString();
                DataSet ds = new DataSet();
                //commented Condition only by gurmeet 1May
                //ds = CEI.GetTestReport(ID);
                ds = CEI.GetInspectionHistoryLogs(ID);
                string TestRportId = string.Empty;
                if (ds != null && ds.Tables.Count > 0)
                {
                    //TestRportId = ds.Tables[0].Rows[0]["TestRportId"].ToString();
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    string script = "alert(\"No Record Found\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                //Session["TestRportId"] = TestRportId;

                ds.Dispose();
            }
            catch (Exception ex) 
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert()", "alert('" + ex.Message.ToString() + "')", true);
                return;
            }
        }

        //Changes of neeraj Suggestions Merged on 8-May-2025
        protected void btnSuggestions_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlReview.SelectedValue == "1")
                {
                    if (Session["InProcessInspectionId"].ToString() != null && Session["InProcessInspectionId"].ToString() != "")
                    {
                        ID = Session["InProcessInspectionId"].ToString();
                        string InspectionType = Session["InspectionType"].ToString();
                        string InstallationType = Session["InstallationType"].ToString();


                        SqlCommand cmd = new SqlCommand("Sp_insertTempInspection");
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
                        cmd.Connection = con;
                        if (con.State == ConnectionState.Closed)
                        {
                            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                            con.Open();
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@inspectionId", ID);
                        cmd.Parameters.AddWithValue("@suggestion", txtSuggestion.Text.Trim());
                        cmd.Parameters.AddWithValue("@ReasionRejection", txtRejected.Text == null ? null : txtRejected.Text);
                        cmd.Parameters.AddWithValue("@InspectionType", InspectionType);
                        DateTime initialIssueDate;
                        if (DateTime.TryParse(txtInspectionDate.Text, out initialIssueDate) && initialIssueDate != DateTime.MinValue)
                        {
                            cmd.Parameters.AddWithValue("@inspectionDate", initialIssueDate);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@inspectionDate", DBNull.Value);
                        }
                        cmd.Parameters.AddWithValue("@Note", txtNote.Text.Trim());
                        int x = cmd.ExecuteNonQuery();
                        con.Close();
                        if (x > 0)
                        {
                            btnPreview.Visible = true;
                            btnSuggestions.Visible = true;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert()", "alert('" + ex.Message.ToString() + "')", true);
                return;
            }
        }

        protected void btnSugg_Click(object sender, EventArgs e)
        {
            CEI.InsertSuggestions(txtSugg.Text.Trim());
            txtSugg.Text = "";
            BindSuggestions();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Suggestion Submitted Successfully.');", true);
        }

      //
        private void GetGridNewInspectionMultiple()
        {
            try
            {
                string ID = Session["InProcessInspectionId"].ToString();
                DataSet dsVC = CEI.GetDetailsToViewTRinMultipleCaseNew(ID);
                if (dsVC != null && dsVC.Tables.Count > 0 && dsVC.Tables[0].Rows.Count > 0)
                {
                    Grid_MultipleInspectionTR.DataSource = dsVC;
                    Grid_MultipleInspectionTR.DataBind();
                }
                else
                {
                    Grid_MultipleInspectionTR.DataSource = null;
                    Grid_MultipleInspectionTR.DataBind();
                    string script = "alert('No Record Found');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert()", "alert('" + ex.Message.ToString() + "')", true);
                return;
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
            catch (Exception ex) { }
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
                if (ds.Tables[0].Rows.Count > 0)
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
                string script = $@"<script>window.open('{fileName}','_blank');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "OpenFileInNewTab", script);
            }
            else if (e.CommandName == "ViewInvoice")
            {
                string fileName = "";
                fileName = "https://ceiharyana.com" + e.CommandArgument.ToString();
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
                        Grid_MultipleInspectionTR.Columns[5].Visible = false;
                        Grid_MultipleInspectionTR.Columns[6].Visible = false;
                        linkButtonInvoice.Visible = false;
                        LinkButtonReport.Visible = false;
                    }
                    else
                    {
                        Grid_MultipleInspectionTR.Columns[5].Visible = true;
                        Grid_MultipleInspectionTR.Columns[6].Visible = true;
                        linkButtonInvoice.Visible = true;
                        LinkButtonReport.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert()", "alert('" + ex.Message.ToString() + "')", true);
                return;
            }
        }
        protected void lnkReturn_Command(object sender, CommandEventArgs e)
        {
            string[] args = e.CommandArgument.ToString().Split(',');
            string inspectionId = args[0];
            string inspectionCount = args.Length > 1 ? args[1] : "0";
            InspectionReturnDetails.GetReturnDetails(inspectionId, inspectionCount);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowReturnModal","$('#ownerModal').modal('show');", true);
        }
    }
}