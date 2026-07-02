using CEI_PRoject;
using CEIHaryana.Model.Industry;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace CEIHaryana.Officers
{
    public partial class PeriodicInspection : System.Web.UI.Page
    {
        CEI CEI = new CEI();
        private static int count;
        private static string IntimationId, AcceptorReturn, Reason, StaffId;
        string Type = string.Empty;
        string InstallType = string.Empty;
        IndustryApiLogDetails logDetails = new IndustryApiLogDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if ((Convert.ToString(Session["StaffID"]) != null && Convert.ToString(Session["StaffID"]) != string.Empty) &&
                        (Convert.ToString(Session["InspectionId"]) != null && Convert.ToString(Session["InspectionId"]) != string.Empty))
                    {
                        GetData();
                        GetInspectionHistoryandPan(Session["InspectionId"].ToString());
                        GetTestReportDataIfPeriodic();
                    }
                    Page.Session["ClickCount"] = "0";
                }
            }
            catch
            {
                Response.Redirect("/OfficerLogout.aspx");
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
        private void GetTestReportDataIfPeriodic()
        {
            try
            {
                ID = Session["InspectionId"].ToString();
                DataSet ds = new DataSet();
                // comment by gurmeet1 may 2025
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
            catch (Exception ex) { }
        }

        public void GetData()
        {
            try
            {
                ID = Session["InspectionId"].ToString();

                DataSet ds = new DataSet();
                ds = CEI.InspectionData(ID);
                Type = ds.Tables[0].Rows[0]["IType"].ToString();
                HyperLink1.Text = ds.Tables[0].Rows[0]["WhoCreated"].ToString();
                txtInspectionReportID.Text = ds.Tables[0].Rows[0]["Id"].ToString();
                txtWorkType.Text = ds.Tables[0].Rows[0]["TypeOfInstallation"].ToString();
                txtApplicantType.Text = ds.Tables[0].Rows[0]["TypeOfApplicant"].ToString();
                txtCapacity.Text = ds.Tables[0].Rows[0]["Capacity"].ToString();
                txtVoltage.Text = ds.Tables[0].Rows[0]["VoltageLevel"].ToString();
                txtSiteOwnerName.Text = ds.Tables[0].Rows[0]["OwnerName"].ToString();
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
                //PermisesType.Visible = false;
                txtPremises.Text = ds.Tables[0].Rows[0]["Inspectiontype"].ToString();
                    //.ToLower() == "industry" ? "Industry" : "NonIndustry";

                LineVoltage.Visible = false;
                //commented by gurmeet 22 july toshow same pages
                txtSiteOwnerName.Text = ds.Tables[0].Rows[0]["OwnerName"].ToString();
                txtContactPersonEmail.Text = ds.Tables[0].Rows[0]["SiteownerEmail"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["SiteownerAddress"].ToString();
                txtSiteOwnerContact.Text = ds.Tables[0].Rows[0]["SiteownerContactNumber"].ToString();
                txtContractorName.Text = ds.Tables[0].Rows[0]["ContractorName"].ToString();
                txtContractorPhoneNo.Text = ds.Tables[0].Rows[0]["ContractorContactNo"].ToString();
                txtSupervisorContact.Text = ds.Tables[0].Rows[0]["SupervisorPhoneNo"].ToString();
                txtSupervisorName.Text = ds.Tables[0].Rows[0]["SupervisorName"].ToString();
                txtDistrict.Text = ds.Tables[0].Rows[0]["District"].ToString();
                txtContractorEmail.Text = ds.Tables[0].Rows[0]["ContractorEmail"].ToString();
                txtSupervisorName.Text = ds.Tables[0].Rows[0]["SupervisorName"].ToString();
                txtSupervisorEmail.Text = ds.Tables[0].Rows[0]["SupervisorEmail"].ToString();
                //ContractorName.Visible = false;
                //ContractorPhoneNo.Visible = false;
                //ContractorEmail.Visible = false;
                //SupervisorName.Visible = false;
                //SupervisorEmail.Visible = false;
                //SiteOwnerContact.Visible = false;
                //OwnerAddress.Visible = false;
                //end here 22 july
                TRAttached.Visible = true;
                TRAttachedGrid.Visible = true;
                // comment by gurmeet1 may 2025
                // GridView1.Columns[7].Visible = false;
                //  GridView1.Columns[5].Visible = false;
                IntimationId = ds.Tables[0].Rows[0]["IntimationId"].ToString();
                grd_Documemnts.Columns[1].Visible = true;

                GridBindDocument();
                DivViewCart.Visible = true;
                GridToViewCart();

                string Status = ds.Tables[0].Rows[0]["ApplicationStatus"].ToString();
                if (Status.Trim() == "InProcess")
                {
                    RadioButtonList2.SelectedIndex = RadioButtonList2.Items.IndexOf(RadioButtonList2.Items.FindByValue("0"));
                    RadioButtonList2.Attributes.Add("disabled", "true");
                    RadioButtonList2.Enabled = false;
                    btnBack.Visible = true;
                    btnSubmit.Visible = false;
                }
                else if (Status.Trim() == "Return")
                {
                    RadioButtonList2.SelectedIndex = RadioButtonList2.Items.IndexOf(RadioButtonList2.Items.FindByValue("1"));
                    RadioButtonList2.Attributes.Add("disabled", "true");
                    RadioButtonList2.Enabled = false;
                    Rejection.Visible = true;
                    txtRejected.Text = ds.Tables[0].Rows[0]["ReturnRemarks"].ToString();
                    txtRejected.Attributes.Add("disabled", "true");

                    btnBack.Visible = true;
                    btnSubmit.Visible = false;
                }
                else if (Status.Trim() == "Approved" || Status.Trim() == "Rejected")
                {
                    RadioButtonList2.Enabled = false;
                    txtRejected.Attributes.Add("disabled", "true");
                    btnBack.Visible = true;
                    btnSubmit.Visible = false;
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        protected void lnkRedirect_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            Session["InspectionTestReportId"] = btn.CommandArgument;

            if (txtWorkType.Text.Trim() == "Line")
            {
                Response.Redirect("/TestReportModal/LineTestReportModal.aspx");
            }
            else if (txtWorkType.Text.Trim() == "Substation Transformer")
            {
                Response.Redirect("/TestReportModal/SubstationTransformerTestReportModal.aspx");
            }
            else if (txtWorkType.Text.Trim() == "Generating Set")
            {
                Response.Redirect("/TestReportModal/GeneratingSetTestReportModal.aspx");
            }
        }
        protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Rejection.Visible = false;
                if (RadioButtonList2.SelectedValue == "0")
                {
                    Rejection.Visible = false;
                }
                else
                {
                    if (RadioButtonList2.SelectedValue == "1")
                    {
                        Rejection.Visible = true;
                        RejectionReason.Visible = true;
                        
                        ddlReasonType.Visible = true;
                        ddlRejectionReasonType.Visible = false;
                    }
                    else
                    {

                        Rejection.Visible = true;
                        RejectionReason.Visible = true;
                        ddlReasonType.Visible = false;
                        ddlRejectionReasonType.Visible = true;
                    }
                }
                //else if (RadioButtonList2.SelectedValue == "1")
                //{
                //    Rejection.Visible = true;
                //}
            }
            catch (Exception ex)
            { }

        }
        protected void grd_Documemnts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string fileName = "";
            try
            {
                if (e.CommandName == "Select")
                {
                    fileName = "https://ceiharyana.com" + e.CommandArgument.ToString();
                    //fileName = "https://ceiharyana.com" + e.CommandArgument.ToString();
                    string script = $@"<script>window.open('{fileName}','_blank');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "OpenFileInNewTab", script);
                }
            }
            catch (Exception ex)
            { }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // comment by gurmeet1 may 2025
                // string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
                string status = DataBinder.Eval(e.Row.DataItem, "ActionTaken").ToString();

                if (status == "Return")
                {
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                }
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].BackColor = ColorTranslator.FromHtml("#9292cc");
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int ClickCount = 0;
            ClickCount = Convert.ToInt32(Session["ClickCount"]);
            if (ClickCount < 1)
            {
               
                int checksuccessmessage = 0;
                try
                {
                    if (Session["InspectionId"].ToString() != null && Session["InspectionId"].ToString() != "" && Session["StaffID"].ToString() != null)
                    {
                        StaffId = Session["StaffID"].ToString();
                        ID = Session["InspectionId"].ToString();

                        if (RadioButtonList2.SelectedValue != "" && RadioButtonList2.SelectedValue != null)
                        {
                            AcceptorReturn = RadioButtonList2.SelectedValue == "0" ? "Accepted" : "Return";
                            Reason = string.IsNullOrEmpty(txtRejected.Text) ? null : txtRejected.Text;
                            ClickCount = ClickCount + 1;
                            Session["ClickCount"] = ClickCount;
                            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();

                            try
                            {
                                string reqType = CEI.GetIndustry_RequestType_New(Convert.ToInt32(ID));
                                if (reqType == "Industry")
                                {
                                    string serverStatus = CEI.CheckServerStatus("https://investharyana.in");
                                    if (serverStatus != "Server is reachable.")
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alert('HEPC Server Is Not Responding . Please Try After Some Time')", true);
                                        return;
                                    }
                                }
                                DataSet ds = new DataSet();
                                ds = CEI.GetTypeOfInspection(ID);
                                InstallType = ds.Tables[0].Rows[0]["TypeOfInspection"].ToString();
                                if (RadioButtonList2.SelectedValue == "2")
                                {
                                    CEI.UpdateInspectionRejection(ID, StaffId, ddlRejectionReasonType.SelectedItem.ToString(), Reason);

                                    checksuccessmessage = 1;
                                }
                                else
                                {
                                    if (InstallType == "Periodic")
                                    {
                                        CEI.updateInspectionPeriodic(ID, StaffId, IntimationId, txtWorkType.Text.Trim(), AcceptorReturn, Reason, ddlReasonType.SelectedItem.Value);
                                    }
                                    checksuccessmessage = 1;
                                }
                                //string actiontype = AcceptorReturn == "Accepted" ? "InProgress" : "Return";
                                string actiontype = AcceptorReturn == "Accepted" ? "InProgress" :
                                                    AcceptorReturn == "Return" ? "Return" :
                                                    AcceptorReturn == "Rejected" ? "Rejected" : "";
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
                            }

                            catch (Exception ex)
                            {
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
                                        if (RadioButtonList2.SelectedValue == "2")
                                        {
                                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alertWithRedirectdataOfRejection();", true);
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessage", "alert('Inspection Request Rejected Successfully'); window.location='NewApplications.aspx'", true);
                                        }
                                        else
                                        {
                                            if (ddlReasonType.SelectedItem.Value != null)
                                            {
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessage", "alert('Inspection Returned to Site Owner'); window.location='NewApplications.aspx'", true);

                                            }
                                            //if (ddlReasonType.SelectedItem.Value == "1") //Based On Documents Returned 
                                            //{
                                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alertWithRedirectdataCommonReturn();", true);
                                            //}
                                            //}
                                            //if (ddlReasonType.SelectedItem.Value == "0") //Based On Test Report Returned
                                            //{
                                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alertWithRedirectdataCommonReturn();", true);
                                            //}
                                            //if (ddlReasonType.SelectedItem.Value == "1") //Based On Documents Returned 
                                            //{
                                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "alertWithRedirectdataCommonReturn();", true);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessage", "alert('Please select Option in Action Required');", true);
                        }
                    }
                    else
                    {
                        Response.Redirect("/OfficerLogout.aspx");
                    }
                }
                catch (Exception ex)
                { }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessage", "alert('You double click on Button.'); window.location='NewApplications.aspx'", true);
            }
        }
        private void GridToViewCart()
        {
            try
            {
                string ID = Session["InspectionId"].ToString();
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
            { }
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
            catch (Exception ex) { }
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Officers/NewApplications.aspx", false);
        }
        protected void GridBindDocument()
        {
            try
            {
                ID = Session["InspectionId"].ToString();
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
            { }
        }
        protected void lnkReturn_Command(object sender, CommandEventArgs e)
        {
            string[] args = e.CommandArgument.ToString().Split(',');
            string inspectionId = args[0];
            string inspectionCount = args.Length > 1 ? args[1] : "0";
            InspectionReturnDetails.GetReturnDetails(inspectionId, inspectionCount);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowReturnModal", "$('#ownerModal').modal('show');", true);
        }
    }
}