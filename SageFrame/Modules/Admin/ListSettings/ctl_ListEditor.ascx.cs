#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using SageFrame.Core.ListManagement;
#endregion

namespace SageFrame.Modules.Admin.ControlPanel
{
    public partial class ctl_ListEditor : BaseAdministrationUserControl
    {
        string _listName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    
                    GetParentList();
                    PopulateTreeRootLevel();
                    BindGridOnPageLoad();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            trEnableSort.Visible = false;
        }

        private void BindGridOnPageLoad()
        {
            try
            {
                bool Issystem = true;
                string CurrentCultureName = GetCurrentCultureName;
                ListManagementController objController = new ListManagementController();
                List<ListInfo> defaultList = objController.GetDefaultList(CurrentCultureName, -1);
                foreach (ListInfo topList in defaultList)
                {
                    BindGrid(topList.ListName, topList.ParentKey);
                    List<ListInfo> listSystemCheck = objController.GetListByPortalID(CurrentCultureName, -1);
                    foreach (ListInfo system in listSystemCheck)
                    {
                        if (system.ListName == topList.ListName)
                            Issystem = system.SystemList;
                    }
                    lblListName.Text = topList.ListName;
                    lblDeleteList.Text = "Delete " + topList.ListName + " List";
                    ViewState["PARENTKEY"] = topList.ParentKey;
                    ViewState["LISTNAME"] = topList.ListName;
                }

                if (Issystem == true)
                {
                    gdvSubList.Columns[4].Visible = false;

                }
                else
                {
                    gdvSubList.Columns[4].Visible = true;
                }
                lblParent.Visible = false;
                if (ViewState["LIST"] != null)
                {
                    lblEntry.Text = ViewState["LIST"].ToString() + " " + GetSageMessage("ListSettings", "Entries");
                }
                ViewMode();
                pnlListAll.Visible = true;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void PopulateTreeRootLevel()
        {
            try
            {
                ListManagementController objController = new ListManagementController();
                List<ListInfo> nodeList = objController.GetListByPortalID(GetCurrentCultureName, -1);

                int count = 1;
                foreach (ListInfo node in nodeList)
                {
                    if (node.ParentKey.ToString() == "")
                    {
                        if (node.ListName != _listName)
                        {
                            TreeNode tn = new TreeNode();
                            tn.Text = node.ListName.ToString();
                            tn.Value = node.ParentKey.ToString() + ":" + count.ToString();
                            PopulateSubLevel(tn, node.ParentKey.ToString());
                            tvList.Nodes.Add(tn);
                            _listName = node.ListName;
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            tvList.CollapseAll();
        }
        private void PopulateSubLevel(TreeNode node, string parentKey)
        {
            try
            {
                ListManagementController objController = new ListManagementController();
                List<ListInfo> nodeList = objController.GetListByPortalID(GetCurrentCultureName, -1);

                int count = 1;
                foreach (ListInfo resultNode in nodeList)
                {
                    if (parentKey == "")
                    {
                        if (resultNode.ParentList == node.Text)
                        {
                            TreeNode nNode = new TreeNode();
                            nNode.Text = resultNode.Parent + ":" + resultNode.ListName.ToString();
                            nNode.Value = resultNode.ParentKey.ToString() + ":" + count;
                            PopulateSubLevel(nNode, resultNode.ParentKey.ToString());
                            node.ChildNodes.Add(nNode);
                        }
                    }
                    else
                    {
                        string[] tempNodes = SplitString(node.Text);
                        string tempNode = tempNodes[1];
                        string temp = parentKey + ":" + tempNode;
                        if (resultNode.ParentList == temp)
                        {
                            TreeNode nNode = new TreeNode();
                            nNode.Text = resultNode.Parent + ":" + resultNode.ListName.ToString();
                            nNode.Value = resultNode.ParentKey.ToString() + ":" + count;
                            PopulateSubLevel(nNode, resultNode.ParentKey.ToString());
                            node.ChildNodes.Add(nNode);

                        }
                    }
                    count = count + 1;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void tvList_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                string listName = tvList.SelectedNode.Text;
                string parentKey = tvList.SelectedNode.Value;
                string deleteText = lblDeleteList.Text;
                string[] texts = deleteText.Split(' ');
                if (texts.Length > 0)
                {
                    lblDeleteList.Text = texts[0] + " " + listName + " " + ((texts.Length - 1) > 0 ? texts[texts.Length - 1] : "");
                }
                if (tvList.SelectedNode.Value.Contains(":"))
                {
                    string[] parentKeys = SplitString(parentKey);
                    parentKey = parentKeys[0];
                }

                if (tvList.SelectedNode.Text.Contains(":"))
                {
                    string[] listNames = SplitString(listName);
                    listName = listNames[1];
                    lblParent.Visible = true;
                    lblParentText.Visible = true;
                }
                else
                {
                    lblParent.Visible = false;
                    lblParentText.Visible = false;
                }
                ListManagementController objController = new ListManagementController();
                List<ListInfo> listSystemCheck = objController.GetListByPortalID(GetCurrentCultureName, -1);

                bool Issystem = true;
                foreach (ListInfo system in listSystemCheck)
                {
                    if (system.ListName == listName)
                        Issystem = system.SystemList;
                }
                lblListName.Text = listName;
                ViewState["PARENTKEY"] = parentKey;
                ViewState["LISTNAME"] = listName;
                BindGrid(listName, parentKey);

                if (Issystem == true)
                {
                    gdvSubList.Columns[4].Visible = false;

                }
                else
                {
                    gdvSubList.Columns[4].Visible = true;
                }
                lblParent.Text = parentKey;
                if (ViewState["LIST"] != null)
                {
                    lblEntry.Text = ViewState["LIST"].ToString() + " " + GetSageMessage("ListSettings", "Entries");
                }
                ViewMode();
                pnlListAll.Visible = true;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

        }

        //Adding New List Started
        protected void imgAddNewList_Click(object sender, EventArgs e)
        {
            AddMode();
            ShowControls();
            ViewState["NEWLIST"] = "Add";
            ddlParentEntry.Items.Clear();
            ddlParentEntry.Enabled = false;
            ClearForm();
            GetParentList();
            BindTreeView();
        }

        #region "DropDown List Initialization"

        private void GetParentList()
        {
            try
            {
                ddlParentList.Items.Clear();
                ListManagementController objController = new ListManagementController();
                List<ListInfo> objList = objController.GetListByPortalID(GetCurrentCultureName, -1);

                if (objList != null)
                {
                    ddlParentList.Items.Insert(0, new ListItem("None Specified", "0"));
                    int i = 1;
                    foreach (ListInfo LPR in objList)
                    {
                        if (LPR.Parent.ToString() != "")
                        {
                            ddlParentList.Items.Insert(i, new ListItem(LPR.Parent.ToString() + ":" + LPR.ListName.ToString(), LPR.ParentKey.ToString() + ":" + i.ToString()));
                        }
                        else
                        {
                            ddlParentList.Items.Insert(i, new ListItem(LPR.ListName.ToString(), ":" + i.ToString()));
                        }
                        i++;
                    }
                    ddlParentList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void ddlParentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlParentList.SelectedIndex != 0)
                {
                    ddlParentEntry.Enabled = true;
                    string listName = string.Empty;
                    string[] listNames = SplitString(ddlParentList.SelectedItem.Text);

                    if (listNames.Length == 2)
                    {
                        listName = listNames[1];
                    }
                    else
                    {
                        listName = listNames[0];
                    }
                    string[] parentId = SplitString(ddlParentList.SelectedValue.ToString());

                    ddlParentEntry.Items.Clear();
                    ListManagementController objController = new ListManagementController();
                    List<ListInfo> listParentEntry = objController.GetListInfo(listName, parentId[0], -1, GetCurrentCultureName);

                    if (listParentEntry != null)
                    {

                        int i = 0;
                        foreach (ListInfo list in listParentEntry)
                        {
                            ddlParentEntry.Items.Insert(i, new ListItem(list.ListName.ToString() + ":" + list.Text.ToString(), list.EntryID.ToString()));
                            i++;

                        }
                    }
                    ddlParentEntry.DataBind();
                }
                else
                {
                    ddlParentEntry.Items.Clear();
                    ddlParentEntry.Enabled = false;
                }
                lblCurrencyCode.Visible = false;
                lblDisplayLocale.Visible = false;
                txtDisplayLocale.Visible = false;
                txtCurrencyCode.Visible = false;
                trDisplayLocale.Visible = false;
                trCurrencyCode.Visible = false;
                trEnableSort.Visible = false;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        #endregion

        protected void imgSave_Click(object sender, EventArgs e)
        {
            try
            {
                // string listName = txtListName.Text;
                string value = txtEntryValue.Text;
                string text = txtEntryText.Text;
                string currencyCode = txtCurrencyCode.Text.Trim();
                string displayLocale = txtDisplayLocale.Text.Trim();
                string createdBy = GetUsername;
                bool isActive = false;

                if (!CheckUniqueness(txtListName.Text))
                {

                    if (chkActive.Checked == true)
                    {
                        isActive = true;
                    }

                    if (ViewState["NEWLIST"] != null)
                    {
                        ViewState["NEWLIST"] = null;
                        AddNewList();
                    }
                    else if (ViewState["LISTNAME"] != null && ViewState["ADDSUBLIST"] != null)
                    {
                        ViewState["ADDSUBLIST"] = null;
                        string listName = ViewState["LISTNAME"].ToString();
                        int parentId = 0;
                        int level = 0;
                        int definitionId = -1;
                        int portalId = -1;
                        bool displayOrder = true;
                        if (ViewState["PARENTKEY"] != null)
                        {
                            ListManagementController objController = new ListManagementController();
                            List<ListManagementInfo> objlist = objController.GetEntriesByNameParentKeyAndPortalID(listName, ViewState["PARENTKEY"].ToString(), -1, GetCurrentCultureName);
                            foreach (ListManagementInfo listDetail in objlist)
                            {
                                parentId = listDetail.ParentID;
                                level = listDetail.Level;
                                definitionId = listDetail.DefinitionID;
                                portalId = listDetail.PortalID;

                            }
                        }
                        try
                        {
                            ListManagementController objController = new ListManagementController();
                            objController.AddNewList(new ListInfo(listName, value, text, parentId, level, currencyCode, displayLocale, displayOrder, definitionId, "", portalId, isActive, createdBy, GetCurrentCultureName));
                            ViewMode();
                            BindGrid(ViewState["LISTNAME"].ToString(), ViewState["PARENTKEY"].ToString());
                        }
                        catch (Exception ex)
                        {
                            ProcessException(ex);
                        }
                    }
                    else if (ViewState["LISTNAME"] != null && ViewState["ENTRYID"] != null)
                    {

                        int entryId = int.Parse(ViewState["ENTRYID"].ToString());
                        ViewState["ENTRYID"] = null;
                        try
                        {
                            ListManagementController objController = new ListManagementController();
                            objController.UpdateListEntry(entryId, value, text, currencyCode, displayLocale, "", isActive, createdBy, GetCurrentCultureName);
                            ViewMode();
                            BindGrid(ViewState["LISTNAME"].ToString(), ViewState["PARENTKEY"].ToString());
                        }
                        catch (Exception ex)
                        {
                            ProcessException(ex);
                        }

                    }
                }
                else
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("ListSettings", "ListAlreadyExists"), "", SageMessageType.Alert);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected bool CheckUniqueness(string listName)
        {
            bool isExist = false;
            int parentId;
            if (ddlParentList.SelectedIndex != 0)
            {
                parentId = int.Parse(ddlParentEntry.SelectedValue.ToString());
            }
            else
            {
                parentId = 0;
            }
            ListManagementController lmCont = new ListManagementController();
            List<ListInfo> lstInfo = lmCont.GetListInfo(listName, GetCurrentCulture(),parentId);
            if (lstInfo.Count > 0)
            {
                isExist = true;
            }
            else
            {
                isExist = false;
            }
            return isExist;
        }

        private void AddNewList()
        {
            string listName = txtListName.Text.Trim();
            string value = txtEntryValue.Text.Trim();
            string text = txtEntryText.Text.Trim();
            int parentId = 0;
            int level = 0;
            int definitionId = -1;
            int portalId = -1;// GetPortalID;
            string createdBy = GetUsername;
            bool displayOrder = false;
            bool isActive = false;
            string currencyCode = txtCurrencyCode.Text.Trim();
            string displayLocale = txtDisplayLocale.Text.Trim();

            if (chkShort.Checked == true)
            {
                displayOrder = true;
            }

            if (chkActive.Checked == true)
            {
                isActive = true;
            }
            if (ddlParentList.SelectedIndex != 0)
            {
                try
                {
                    parentId = int.Parse(ddlParentEntry.SelectedValue.ToString());
                    string selectedListName = string.Empty;
                    string[] selectedListNames = SplitString(ddlParentEntry.SelectedItem.Text);
                    selectedListName = selectedListNames[0];

                    ListManagementController objController = new ListManagementController();
                    List<ListManagementInfo> objList = objController.GetListEntriesByNameValueAndEntryID(selectedListName, "", int.Parse(ddlParentEntry.SelectedValue.ToString()), GetCurrentCultureName);
                    foreach (ListManagementInfo parentLevel in objList)
                    {
                        level = int.Parse(parentLevel.Level.ToString()) + 1;
                    }
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
            try
            {
                ListManagementController objController = new ListManagementController();
                int ListID = objController.AddNewList(new ListInfo(listName, value, text, parentId, level, currencyCode, displayLocale, displayOrder, definitionId, "", portalId, isActive, createdBy, GetCurrentCultureName));

                if (ListID == 0)
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("ListSettings", "ListAlreadyExists"), "", SageMessageType.Alert);
                }
                else
                {
                    BindTreeView();
                    ViewMode();
                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("ListSettings", "ListIsAddedSuccessfully"), "", SageMessageType.Success);
                    BindGridOnPageLoad();
                }

            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private string[] SplitString(string textToSplit)
        {
            string[] splitedText = textToSplit.Split(new char[] { ':' });
            return splitedText;
        }

        protected void imgAddSubList_Click(object sender, EventArgs e)
        {
            AddEditMode();
            HideControls();
            ViewState["ADDSUBLIST"] = "add";
            ClearForm();
            BindTreeView();
        }

        #region "Deleting list according to selected level"

        protected void imgDeleteList_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["LISTNAME"] != null)
                {
                    ListManagementController objController = new ListManagementController();
                    List<ListManagementInfo> objList = objController.GetListCopyEntriesByNameParentKeyAndPortalID(ViewState["LISTNAME"].ToString(), ViewState["PARENTKEY"].ToString(), -1, GetCurrentCultureName);//GetPortalID;
                    foreach (ListManagementInfo listEntry in objList)
                    {
                        GetListByEntryId(listEntry.EntryID);
                        DeleteList(listEntry.EntryID);
                        BindTreeView();
                        pnlListAll.Visible = false;
                        BindGridOnPageLoad();
                        ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("ListSettings", "ListIsDeletedSuccessfully"), "", SageMessageType.Success);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void UpdateDeleteLabel()
        {
          
            string listName = tvList.SelectedNode.Text;
            string deleteText = lblDeleteList.Text;
            string[] texts = deleteText.Split(' ');
            lblDeleteList.Text = texts[0] + " " + listName + " " + ((texts.Length - 1) > 0 ? texts[texts.Length - 1] : "");
        }

        private void GetListByEntryId(int entryId)
        {
            try
            {
                ListManagementController objController = new ListManagementController();
                List<ListManagementInfo> listByEntryId = objController.GetListEntryByParentID(entryId, GetCurrentCultureName);
                foreach (ListManagementInfo listbyParent in listByEntryId)
                {
                    GetListByEntryId(listbyParent.EntryID);
                    DeleteList(listbyParent.EntryID);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        private void DeleteList(int entryId)
        {
            try
            {
                ListManagementController objController = new ListManagementController();
                bool isExist = objController.DeleteListEntry(entryId, true, GetCurrentCultureName);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        #endregion



        protected void gdvSubList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int entryId = int.Parse(e.CommandArgument.ToString());
                if (e.CommandName == "Delete")
                {
                    try
                    {
                        ListManagementController objController = new ListManagementController();
                        bool isExist = objController.DeleteListEntry(entryId, true, GetCurrentCultureName);
                        BindTreeView();
                        if (isExist)
                        {
                            if (ViewState["LISTNAME"] != null)
                            {
                                string listName = ViewState["LISTNAME"].ToString();
                                string parentKey = ViewState["PARENTKEY"].ToString();
                                BindGrid(listName, parentKey);

                            }
                        }
                        else
                        {
                            BindGridOnPageLoad();
                        }

                        ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("ListSettings", "ListIsDeletedSuccessfully"), "", SageMessageType.Success);
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }
                }
                else if (e.CommandName == "SortUp")
                {
                    try
                    {
                        ListManagementController objController = new ListManagementController();
                        objController.SortList(entryId, true, GetCurrentCultureName);
                        ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("ListSettings", "TheListIsShiftedUpSuccessfully"), "", SageMessageType.Success);
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }

                }
                else if (e.CommandName == "SortDown")
                {
                    try
                    {
                        ListManagementController objController = new ListManagementController();
                        objController.SortList(entryId, false, GetCurrentCultureName);
                        ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("ListSettings", "TheListIsShiftedDownSuccessfully"), "", SageMessageType.Success);
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }
                }
                else if (e.CommandName == "Edit")
                {
                    try
                    {
                        HideControls();
                        ViewState["ENTRYID"] = entryId;
                        ListManagementController objController = new ListManagementController();
                        ListManagementInfo editObj = objController.GetListEntryDetails(" ", " ", entryId, GetCurrentCultureName);
                        txtEntryText.Text = editObj.Text;
                        txtEntryValue.Text = editObj.Value;
                        txtCurrencyCode.Text = editObj.CurrencyCode;
                        txtDisplayLocale.Text = editObj.DisplayLocale;
                        chkActive.Checked = editObj.IsActive;
                        AddEditMode();
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }
                }
                if (ViewState["LISTNAME"] != null)
                {
                    BindGrid(ViewState["LISTNAME"].ToString(), ViewState["PARENTKEY"].ToString());
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void AddMode()
        {
            pnlAddList.Visible = true;
            pnlListAll.Visible = false;
            trEnableSort.Visible = false;
        }
        private void AddEditMode()
        {
            pnlAddList.Visible = true;
            pnlViewList.Visible = false;
            trEnableSort.Visible = false;

        }

        private void ViewMode()
        {
            pnlAddList.Visible = false;
            pnlViewList.Visible = true;
            trEnableSort.Visible = false;

        }
        private void HideControls()
        {
            lblListNameText.Visible = false;
            trEnableSort.Visible = false;
            txtListName.Visible = false;
            chkShort.Visible = false;
            ddlParentEntry.Visible = false;
            ddlParentList.Visible = false;
            lblParentEntryText.Visible = false;
            lblParentListText.Visible = false;
            lblCurrencyCode.Visible = false;
            lblDisplayLocale.Visible = false;
            trDisplayLocale.Visible = false;
            trCurrencyCode.Visible = false;
            txtDisplayLocale.Visible = false;
            txtCurrencyCode.Visible = false;
            tdListName.Visible = false;
            tdParentList.Visible = false;
            tdParentEntry.Visible = false;

            tdDisplayLocale.Visible = false;
            tdCurrencyCode.Visible = false;

            trListName.Visible = false;
            trParentEntry.Visible = false;
            trParentList.Visible = false;

            lblSortOrder.Visible = false;
            if (ViewState["LISTNAME"] != null)
            {

                if (ViewState["LISTNAME"].ToString() == "Country")
                {
                    lblCurrencyCode.Visible = true;
                    lblDisplayLocale.Visible = true;
                    txtDisplayLocale.Visible = true;
                    txtCurrencyCode.Visible = true;


                    tdDisplayLocale.Visible = true;
                    tdCurrencyCode.Visible = true;
                    trDisplayLocale.Visible = true;
                    trCurrencyCode.Visible = true;

                    trDisplayLocale.Visible = true;
                    trCurrencyCode.Visible = true;
                    trEnableSort.Visible = false;

                }
            }
            trEnableSort.Visible = false;
        }
        private void ShowControls()
        {
            lblListNameText.Visible = true;
            txtListName.Visible = true;
            txtDisplayLocale.Visible = false;
            txtCurrencyCode.Visible = false;
            chkShort.Visible = true;
            ddlParentEntry.Visible = true;
            ddlParentList.Visible = true;
            lblParentEntryText.Visible = true;
            lblParentListText.Visible = true;
            lblCurrencyCode.Visible = false;
            lblDisplayLocale.Visible = false;
            lblSortOrder.Visible = true;
            trEnableSort.Visible = true;
            tdListName.Visible = true;
            tdParentList.Visible = true;
            tdParentEntry.Visible = true;

            tdDisplayLocale.Visible = false;
            tdCurrencyCode.Visible = false;

            trDisplayLocale.Visible = false;
            trCurrencyCode.Visible = false;

            trListName.Visible = true;
            trParentEntry.Visible = true;
            trParentList.Visible = true;
            trEnableSort.Visible = false;
        }

        protected void gdvSubList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void gdvSubList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void gdvSubList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("imgDelete");
                btnDelete.Attributes.Add("onclick", "javascript:return confirm('" + GetSageMessage("ListSettings", "WantToDelete") + "')");

            }
        }


        private void BindGrid(string listName, string parentKey)
        {
            try
            {
                ListManagementController objController = new ListManagementController();
                DataSet ds = objController.GetListInfoInDataSet(listName, parentKey, -1, GetCurrentCultureName);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    DataTable dtList = ds.Tables[0];
                    gdvSubList.DataSource = dtList;
                    gdvSubList.DataBind();
                    ViewState["LISTTABLE"] = dtList;
                    ViewState["LIST"] = dtList.Rows.Count;
                    if (gdvSubList.Rows.Count > 0)
                    {
                        lblEntry.Text = dtList.Rows.Count + " " + GetSageMessage("ListSettings", "Entries");
                        if (gdvSubList.PageIndex == 0)
                        {
                            gdvSubList.Rows[0].FindControl("imgListUp").Visible = false;

                        }
                        if (gdvSubList.PageIndex == (gdvSubList.PageCount - 1))
                        {
                            gdvSubList.Rows[gdvSubList.Rows.Count - 1].FindControl("imgListDown").Visible = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void BindTreeView()
        {
            try
            {
                tvList.Nodes.Clear();
                PopulateTreeRootLevel();
                tvList.CollapseAll();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void OnCancelShowGrid()
        {
            try
            {
                if (ViewState["LISTTABLE"] != null)
                {
                    gdvSubList.DataSource = (DataTable)ViewState["LISTTABLE"];
                    gdvSubList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imgCancel_Click(object sender, EventArgs e)
        {
            OnCancelShowGrid();
            ViewMode();
        }

        private void ClearForm()
        {
            txtCurrencyCode.Text = "";
            txtDisplayLocale.Text = "";
            txtEntryText.Text = "";
            txtEntryValue.Text = "";
            txtListName.Text = "";
            chkActive.Checked = false;
            chkShort.Checked = false;
            trEnableSort.Visible = false;
        }

        protected void imgCancelAll_Click(object sender, EventArgs e)
        {
            pnlListAll.Visible = false;
            BindTreeView();
            trEnableSort.Visible = false;
        }

        #region "Pager Region"

        protected void gdvSubList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvSubList.PageIndex = e.NewPageIndex;
            BindGridForSameList();

        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGridPageSize.SelectedValue != "0")
            {
                gdvSubList.AllowPaging = true;
                gdvSubList.PageSize = int.Parse(ddlGridPageSize.SelectedValue);
                gdvSubList.PageIndex = 0;
            }
            else
            {
                gdvSubList.AllowPaging = false;
            }
            BindGridForSameList();


        }


        private void BindGridForSameList()
        {
            if (ViewState["LISTNAME"] != null && ViewState["PARENTKEY"] != null)
            {
                BindGrid(ViewState["LISTNAME"].ToString(), ViewState["PARENTKEY"].ToString());
            }
        }

        #endregion
    }
}