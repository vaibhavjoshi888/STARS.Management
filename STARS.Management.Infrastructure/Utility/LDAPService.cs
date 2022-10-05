using Microsoft.Extensions.Options;
using STARS.Management.Core.DTO;
using STARS.Management.Core.Interface;
using STARS.Management.Core.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text.RegularExpressions;

namespace STARS.Management.Infrastructure.Utility;
public class LDAPService : ILDAPService
{
    private DirectorySearcher search;
    public LDAPService(IOptions<LDAPContext> lDapContext)
    {
        string ldapConnection = lDapContext.Value.Server;
        DirectoryEntry dEntry = new DirectoryEntry(ldapConnection);
        search = new DirectorySearcher(dEntry);
    }

    public ADUser GetUserFromAD(string userNameOrEmail, bool isEmail)
    {

        ADUser adUser = new ADUser();
        SearchResultCollection resultList = null;

        if (!isEmail && !String.IsNullOrWhiteSpace(userNameOrEmail))
        {
            search.Filter = string.Format("(&(objectCategory=user)(samaccountname={0}))", userNameOrEmail.Trim());
        }
        else if (isEmail && !String.IsNullOrWhiteSpace(userNameOrEmail))
        {
            search.Filter = string.Format("(&(objectCategory=user)(mail={0}))", userNameOrEmail.Trim());
        }

        try
        {
            resultList = search.FindAll();
            search.PropertiesToLoad.Add("cn");
            search.PropertiesToLoad.Add("displayName");
            search.PropertiesToLoad.Add("userPrincipalName");
            search.PropertiesToLoad.Add("sAMAccountName");
            search.PropertiesToLoad.Add("mail");
            search.PropertiesToLoad.Add("telephonenumber");
            search.PropertiesToLoad.Add("givenname");
            search.PropertiesToLoad.Add("sn");
            search.PropertiesToLoad.Add("physicaldeliveryofficename");
            search.PropertiesToLoad.Add("employeetype");
            search.PropertiesToLoad.Add("employeeid");
            search.PropertiesToLoad.Add("employeenumber");
            search.PropertiesToLoad.Add("title");
            search.PropertiesToLoad.Add("department");
            search.PropertiesToLoad.Add("division");
            search.PropertiesToLoad.Add("manager");
            search.PropertiesToLoad.Add("thumbnailphoto");


            SearchResult result = search.FindOne();

            if (result != null)
            {
                adUser.CorpID = (result.Properties.Contains("sAMAccountName")) ? result.Properties["sAMAccountName"][0].ToString() : "";
                adUser.FullName = (result.Properties.Contains("displayName")) ? result.Properties["displayName"][0].ToString() : "";
                adUser.DisplayName = (result.Properties.Contains("displayName")) ? result.Properties["displayName"][0].ToString() : "";
                adUser.SamaAccountName = (result.Properties.Contains("sAMAccountName")) ? result.Properties["sAMAccountName"][0].ToString() : "";
                adUser.Email = (result.Properties.Contains("mail")) ? result.Properties["mail"][0].ToString() : "";
                adUser.Phone = (result.Properties.Contains("telephonenumber")) ? result.Properties["telephonenumber"][0].ToString() : "";
                adUser.GivenName = (result.Properties.Contains("givenname")) ? result.Properties["givenname"][0].ToString() : "";
                adUser.Surname = (result.Properties.Contains("sn")) ? result.Properties["sn"][0].ToString() : "";
                adUser.PhysicalDeliveryOfficeName = (result.Properties.Contains("physicaldeliveryofficename")) ? result.Properties["physicaldeliveryofficename"][0].ToString() : "";
                adUser.EmployeeType = (result.Properties.Contains("employeetype")) ? result.Properties["employeetype"][0].ToString() : "";
                adUser.EmployeeId = (result.Properties.Contains("employeeid")) ? result.Properties["employeeid"][0].ToString() : "";
                adUser.EmployeeNumber = (result.Properties.Contains("employeenumber")) ? result.Properties["employeenumber"][0].ToString() : "";
                adUser.Title = (result.Properties.Contains("title")) ? result.Properties["title"][0].ToString() : "";
                adUser.Department = (result.Properties.Contains("department")) ? result.Properties["department"][0].ToString() : "";
                adUser.Manager = (result.Properties.Contains("manager")) ? result.Properties["manager"][0].ToString() : "";
                adUser.Division = (result.Properties.Contains("division")) ? result.Properties["division"][0].ToString() : "";
                adUser.ThumbnailPhoto = (result.Properties.Contains("thumbnailphoto")) ? (Byte[])result.Properties["thumbnailphoto"][0] : Array.Empty<byte>();

            }
        }
        catch (Exception ex)
        {
            throw ex;
            // log.Error("LDAPService.GetUserFromAD method: An Error occurred while getting user info from AD", ex);
        }

        return adUser;
    }

    public bool IsValidADUser(string corpUserId, string pwd)
    {
        bool isValidUser = false;

        try
        {
            string corpUserName = string.Empty;

            if (corpUserId.ToLower().Contains("corp\\"))
                corpUserName = corpUserId.ToLower().Replace("corp\\", "");
            else
                corpUserName = corpUserId;


            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "CORP"))
            {
                // validate the credentials
                isValidUser = pc.ValidateCredentials(corpUserName.Trim(), pwd.Trim(), ContextOptions.Negotiate);
            }
        }
        catch (Exception)
        {
            throw new Exception("An error occurred while validating user credential.");
        }


        return isValidUser;
    }

    public string GetUserFullName(string corpUserID)
    {
        string userFullName = string.Empty;
        string inputUserNameOrEmail = corpUserID.Trim();
        ADUser userInfo;
        string corpUserName = string.Empty;

        if (corpUserID.ToLower().Contains("corp\\"))
            corpUserName = corpUserID.ToLower().Replace("corp\\", "");
        else
            corpUserName = corpUserID;

        userInfo = GetUserFromAD(corpUserName, false);


        if (userInfo != null)
        {
            userFullName = userInfo.FullName;
        }

        userFullName = (String.IsNullOrWhiteSpace(userFullName)) ? corpUserID : userFullName;

        return userFullName;
    }

    public string GetUserEmail(string corpUserID)
    {
        string userEmail = string.Empty;
        string inputUserNameOrEmail = corpUserID.Trim();
        ADUser userInfo;
        string corpUserName = string.Empty;

        if (corpUserID.ToLower().Contains("corp\\"))
            corpUserName = corpUserID.ToLower().Replace("corp\\", "");
        else
            corpUserName = corpUserID;

        userInfo = GetUserFromAD(corpUserName, false);


        if (userInfo != null)
        {
            userEmail = userInfo.Email;
        }



        return userEmail;
    }

    public Tuple<bool, ADUser> ValidateAndGetADUser(string corpUserId, string pwd)
    {
        ADUser adUser = new ADUser();
        bool isValidUser = false;

        try
        {
            string corpUserName = string.Empty;

            if (corpUserId.ToLower().Contains("corp\\"))
                corpUserName = corpUserId.ToLower().Replace("corp\\", "");
            else
                corpUserName = corpUserId;


            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "CORP"))
            {
                // validate the credentials
                isValidUser = pc.ValidateCredentials(corpUserName.Trim(), pwd.Trim(), ContextOptions.Negotiate);

                if (isValidUser == true)
                {
                    adUser = GetUserFromAD(corpUserName, false);
                }
            }
        }
        catch (Exception)
        {
            throw new Exception("An error occurred while validating user credential.");
        }


        return Tuple.Create<bool, ADUser>(isValidUser, adUser);
    }

    public ADUser GetManagerInfo(string managerStr)
    {
        ADUser manager = new ADUser();

        if (!string.IsNullOrEmpty(managerStr))
        {
            try
            {
                if (managerStr.Contains(",") && managerStr.Contains("CN="))
                {
                    string[] mgrAttr = managerStr.Split(',');
                    string managerCN = mgrAttr[0].ToString().Split('=')[1].ToString();
                    manager = GetUserFromAD(managerCN, false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        return manager;
    }

    public Tuple<List<ADUser>, int> SearchADUsers(string searchText, int pageZise = 25)
    {
        string retVal = String.Empty;
        SearchResultCollection resultList = null;
        List<ADUser> adUsers = new List<ADUser>();
        string txtResult = string.Empty;

        //fix issue with comma, dot before and after a word
        //remove characters that you specify in a character array from the beginning and end of a string.
        searchText = searchText.Trim(new Char[] { ' ', ',', '.' });
        if (String.IsNullOrWhiteSpace(searchText))
        {
            return Tuple.Create(adUsers, 0);
        }

        if (!String.IsNullOrWhiteSpace(searchText))
        {

            //** With Partial Search for firstname or displayname(to fix -"rhea" - displayName=*{0}* takes very long time to search) **//
            search.Filter = string.Format("(&(objectCategory=user)(!userAccountControl:1.2.840.113556.1.4.803:=2)(userAccountControl:1.2.840.113556.1.4.803:=512)(|(cn={0})(mail={0})(samaccountname={0})(samaccountname={0})(givenname={0}*)(sn={0}*)(displayName={0}*)))", searchText.Trim());
        }

        string[] propertylist = "cn,displayName,userPrincipalName,sAMAccountName,mail,telephonenumber,givenname,sn,physicaldeliveryofficename,employeetype,employeeid,employeenumber,title,department,division,manager,thumbnailphoto".Split(',').ToArray();
        search.PropertiesToLoad.AddRange(propertylist);

        search.PageSize = (pageZise <= 0) ? 25 : pageZise;

        resultList = search.FindAll();

        //** Advanced Search by different combination of Firstname & Lastname with blank, comma and dot seperator **  
        #region Advanced Search by FirstName & LastName           
        if (resultList.Count < 1)
        {
            searchText = Regex.Replace(searchText, @"\s+", " ");
            List<string> wordList = new List<string>();

            if (searchText.Contains(','))
            {
                wordList = searchText.Split(',').ToList();
            }
            else if (searchText.Contains('.'))
            {
                wordList = searchText.Split('.').ToList();
            }
            else if (searchText.Contains(' '))
            {
                wordList = searchText.Split(' ').ToList();
            }
            //To  Do val function for search.filter
            if (wordList.Count > 1)
            {
                if (resultList.Count < 1)
                {
                    //search.Filter = string.Format("(&(objectCategory=user)(!userAccountControl:1.2.840.113556.1.4.803:=2)(userAccountControl:1.2.840.113556.1.4.803:=512)((givenname={0})(sn={1})))", wordList[0].Val(), wordList[1].Val());
                    resultList = search.FindAll();
                }

                if (resultList.Count < 1)
                {
                    //search.Filter = string.Format("(&(objectCategory=user)(!userAccountControl:1.2.840.113556.1.4.803:=2)(userAccountControl:1.2.840.113556.1.4.803:=512)((givenname={0})(sn={1})))", wordList[1].Val(), wordList[0].Val());
                    resultList = search.FindAll();
                }

                if (resultList.Count < 1)
                {
                    //search.Filter = string.Format("(&(objectCategory=user)(!userAccountControl:1.2.840.113556.1.4.803:=2)(userAccountControl:1.2.840.113556.1.4.803:=512)((givenname={0}*)(sn={1}*)))", wordList[0].Val(), wordList[1].Val());
                    resultList = search.FindAll();
                }

                if (resultList.Count < 1)
                {
                    //search.Filter = string.Format("(&(objectCategory=user)(!userAccountControl:1.2.840.113556.1.4.803:=2)(userAccountControl:1.2.840.113556.1.4.803:=512)((givenname={0}*)(sn={1}*)))", wordList[1].Val(), wordList[0].Val());
                    resultList = search.FindAll();
                }

                if (resultList.Count < 1)
                {
                    //eg: displayname-"datt,rhe"
                    // search.Filter = string.Format("(&(objectCategory=user)(!userAccountControl:1.2.840.113556.1.4.803:=2)(userAccountControl:1.2.840.113556.1.4.803:=512)(|(displayName={0}*)(displayName={1}*)))", wordList[0].Val(), wordList[1].Val());
                    resultList = search.FindAll();
                }
                if (resultList.Count < 1)
                {
                    //eg: displayname-"rhe,datt"
                    //   search.Filter = string.Format("(&(objectCategory=user)(!userAccountControl:1.2.840.113556.1.4.803:=2)(userAccountControl:1.2.840.113556.1.4.803:=512)(|(displayName={0}*)(displayName={1}*)))", wordList[1].Val(), wordList[0].Val());
                    resultList = search.FindAll();
                }

            }
        }
        #endregion Advanced Search by FirstName & LastName

        //loop through results of search
        foreach (SearchResult result in resultList)
        {
            Dictionary<string, object> userProp = new Dictionary<string, object>();

            ADUser adUser = new ADUser();

            adUser.CorpID = (result.Properties.Contains("sAMAccountName")) ? result.Properties["sAMAccountName"][0].ToString() : "";
            adUser.FullName = (result.Properties.Contains("displayName")) ? result.Properties["displayName"][0].ToString() : "";
            adUser.DisplayName = (result.Properties.Contains("displayName")) ? result.Properties["displayName"][0].ToString() : "";
            adUser.SamaAccountName = (result.Properties.Contains("sAMAccountName")) ? result.Properties["sAMAccountName"][0].ToString() : "";
            adUser.Email = (result.Properties.Contains("mail")) ? result.Properties["mail"][0].ToString() : "";
            adUser.Phone = (result.Properties.Contains("telephonenumber")) ? result.Properties["telephonenumber"][0].ToString() : "";
            adUser.GivenName = (result.Properties.Contains("givenname")) ? result.Properties["givenname"][0].ToString() : "";
            adUser.Surname = (result.Properties.Contains("sn")) ? result.Properties["sn"][0].ToString() : "";
            adUser.PhysicalDeliveryOfficeName = (result.Properties.Contains("physicaldeliveryofficename")) ? result.Properties["physicaldeliveryofficename"][0].ToString() : "";
            adUser.EmployeeType = (result.Properties.Contains("employeetype")) ? result.Properties["employeetype"][0].ToString() : "";
            adUser.EmployeeId = (result.Properties.Contains("employeeid")) ? result.Properties["employeeid"][0].ToString() : "";
            adUser.EmployeeNumber = (result.Properties.Contains("employeenumber")) ? result.Properties["employeenumber"][0].ToString() : "";
            adUser.Title = (result.Properties.Contains("title")) ? result.Properties["title"][0].ToString() : "";
            adUser.Department = (result.Properties.Contains("department")) ? result.Properties["department"][0].ToString() : "";
            adUser.Manager = (result.Properties.Contains("manager")) ? result.Properties["manager"][0].ToString() : "";
            adUser.Division = (result.Properties.Contains("division")) ? result.Properties["division"][0].ToString() : "";
            adUser.ThumbnailPhoto = (result.Properties.Contains("thumbnailphoto")) ? (Byte[])result.Properties["thumbnailphoto"][0] : Array.Empty<byte>();

            adUsers.Add(adUser);


        }

        int searchTotalCount = adUsers.Count;

        return Tuple.Create(adUsers, searchTotalCount);
    }

}


