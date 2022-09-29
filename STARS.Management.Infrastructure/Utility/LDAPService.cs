using STARS.Management.Core.Interface;
using STARS.Management.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text.RegularExpressions;

namespace STARS.Management.Infrastructure.Utility;
public class LDAPService :ILDAPService
{
    DirectorySearcher search;
    public LDAPService()
    {
        string ldapConnection = ConfigurationManager.AppSettings.Get("LdapConnection");
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
}


