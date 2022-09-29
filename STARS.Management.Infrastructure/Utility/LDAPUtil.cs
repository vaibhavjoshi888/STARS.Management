using STARS.Management.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;


namespace STARS.Management.Infrastructure.Utility;
public class LDAPUtil
{
    public LDAPUtil()
    {

    }

    public  ADUser GetUserFromAD(string userNameOrEmail, bool isEmail)
    {
        string ldapConnection = ConfigurationManager.AppSettings.Get("LdapConnection");

        ADUser adUser = new ADUser();
        SearchResultCollection resultList = null;
        DirectoryEntry dEntry = new DirectoryEntry(ldapConnection);
        DirectorySearcher search = new DirectorySearcher(dEntry);

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
            // log.Error("LDAPUtil.GetUserFromAD method: An Error occurred while getting user info from AD", ex);
        }

        return adUser;
    }



}
