using System;
using Microsoft.ReportingServices.Interfaces;

namespace PowerBI.ReportingServices.Security
{
    public sealed partial class Authorization
    {
        public bool CheckAccess(
          string userName,
          IntPtr userToken,
          byte[] secDesc,
          ModelItemOperation modelItemOperation)
        {
            // If the user is the administrator, allow unrestricted access.
            // Because SQL Server defaults to case-insensitive, we have to
            // perform a case insensitive comparison. Ideally you would check
            // the SQL Server instance CaseSensitivity property before making
            // a case-insensitive comparison.
            if (adminUserNames.Contains(userName))
            {
                return true;
            }

            AceCollection acl = DeserializeAcl(secDesc);
            foreach (AceStruct ace in acl)
            {
                // First check to see if the user or group has an access control 
                //  entry for the item
                if (string.Equals(userName, ace.PrincipalName, StringComparison.CurrentCultureIgnoreCase))
                {
                    // If an entry is found, 
                    // return true if the given required operation
                    // is contained in the ACE structure
                    foreach (ModelItemOperation aclOperation in ace.ModelItemOperations)
                    {
                        if (aclOperation == modelItemOperation)
                            return true;
                    }
                }
            }

            return false;
        }

        public bool CheckAccess(
         string userName,
         IntPtr userToken,
         byte[] secDesc,
         ModelOperation modelOperation)
        {
            // If the user is the administrator, allow unrestricted access.
            // Because SQL Server defaults to case-insensitive, we have to
            // perform a case insensitive comparison. Ideally you would check
            // the SQL Server instance CaseSensitivity property before making
            // a case-insensitive comparison.
            if (adminUserNames.Contains(userName))
            {
                return true;
            }

            AceCollection acl = DeserializeAcl(secDesc);
            foreach (AceStruct ace in acl)
            {
                // First check to see if the user or group has an access control 
                //  entry for the item
                if (string.Equals(userName, ace.PrincipalName, StringComparison.CurrentCultureIgnoreCase))
                {
                    // If an entry is found, 
                    // return true if the given required operation
                    // is contained in the ACE structure
                    foreach (ModelOperation aclOperation in ace.ModelOperations)
                    {
                        if (aclOperation == modelOperation)
                            return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Indicates whether a given user is authorized to access the item 
        /// for a given catalog operation.
        /// </summary>
        /// <param name="userName">The name of the user as returned by the 
        /// GetUserInfo method.</param>
        /// <param name="userToken">Pointer to the user ID returned by 
        /// GetUserInfo.</param>
        /// <param name="secDesc">The security descriptor returned by 
        /// CreateSecurityDescriptor.</param>
        /// <param name="requiredOperation">The operation being requested by 
        /// the report server for a given user.</param>
        /// <returns>True if the user is authorized.</returns>
        public bool CheckAccess(
           string userName,
           IntPtr userToken,
           byte[] secDesc,
           CatalogOperation requiredOperation)
        {
            // If the user is the administrator, allow unrestricted access.
            // Because SQL Server defaults to case-insensitive, we have to
            // perform a case insensitive comparison. Ideally you would check
            // the SQL Server instance CaseSensitivity property before making
            // a case-insensitive comparison.
            if (adminUserNames.Contains(userName))
            {
                return true;
            }

            AceCollection acl = DeserializeAcl(secDesc);
            foreach (AceStruct ace in acl)
            {
                // First check to see if the user or group has an access control 
                //  entry for the item
                if (string.Equals(userName, ace.PrincipalName, StringComparison.CurrentCultureIgnoreCase))
                {
                    // If an entry is found, 
                    // return true if the given required operation
                    // is contained in the ACE structure
                    foreach (CatalogOperation aclOperation in ace.CatalogOperations)
                    {
                        if (aclOperation == requiredOperation)
                            return true;
                    }
                }
            }

            return false;
        }

        // Overload for array of catalog operations
        public bool CheckAccess(
           string userName,
           IntPtr userToken,
           byte[] secDesc,
           CatalogOperation[] requiredOperations)
        {
            foreach (CatalogOperation operation in requiredOperations)
            {
                if (!CheckAccess(userName, userToken, secDesc, operation))
                    return false;
            }
            return true;
        }

        // Overload for Report operations
        public bool CheckAccess(
           string userName,
           IntPtr userToken,
           byte[] secDesc,
           ReportOperation requiredOperation)
        {
            // If the user is the administrator, allow unrestricted access.
            if (adminUserNames.Contains(userName))
            {
                return true;
            }

            AceCollection acl = DeserializeAcl(secDesc);
            foreach (AceStruct ace in acl)
            {
                if (string.Equals(userName, ace.PrincipalName, StringComparison.CurrentCultureIgnoreCase))
                {
                    foreach (ReportOperation aclOperation in
                       ace.ReportOperations)
                    {
                        if (aclOperation == requiredOperation)
                            return true;
                    }
                }
            }
            return false;
        }

        // Overload for Folder operations
        public bool CheckAccess(
           string userName,
           IntPtr userToken,
           byte[] secDesc,
           FolderOperation requiredOperation)
        {
            // If the user is the administrator, allow unrestricted access.
            if (adminUserNames.Contains(userName))
            {
                return true;
            }

            AceCollection acl = DeserializeAcl(secDesc);
            foreach (AceStruct ace in acl)
            {
                if (string.Equals(userName, ace.PrincipalName, StringComparison.CurrentCultureIgnoreCase))
                {
                    foreach (FolderOperation aclOperation in
                       ace.FolderOperations)
                    {
                        if (aclOperation == requiredOperation)
                            return true;
                    }
                }
            }

            return false;
        }

        // Overload for an array of Folder operations
        public bool CheckAccess(
           string userName,
           IntPtr userToken,
           byte[] secDesc,
           FolderOperation[] requiredOperations)
        {
            foreach (FolderOperation operation in requiredOperations)
            {
                if (!CheckAccess(userName, userToken, secDesc, operation))
                    return false;
            }
            return true;
        }

        // Overload for Resource operations
        public bool CheckAccess(
           string userName,
           IntPtr userToken,
           byte[] secDesc,
           ResourceOperation requiredOperation)
        {
            // If the user is the administrator, allow unrestricted access.
            if (adminUserNames.Contains(userName))
            {
                return true;
            }

            AceCollection acl = DeserializeAcl(secDesc);
            foreach (AceStruct ace in acl)
            {
                if (string.Equals(userName, ace.PrincipalName, StringComparison.CurrentCultureIgnoreCase))
                {
                    foreach (ResourceOperation aclOperation in
                       ace.ResourceOperations)
                    {
                        if (aclOperation == requiredOperation)
                            return true;
                    }
                }
            }

            return false;
        }

        // Overload for an array of Resource operations
        public bool CheckAccess(
           string userName,
           IntPtr userToken,
           byte[] secDesc,
           ResourceOperation[] requiredOperations)
        {
            // If the user is the administrator, allow unrestricted access.
            if (adminUserNames.Contains(userName))
            {
                return true;
            }

            foreach (ResourceOperation operation in requiredOperations)
            {
                if (!CheckAccess(userName, userToken, secDesc, operation))
                    return false;
            }
            return true;
        }

        // Overload for Datasource operations
        public bool CheckAccess(
           string userName,
           IntPtr userToken,
           byte[] secDesc,
           DatasourceOperation requiredOperation)
        {
            // If the user is the administrator, allow unrestricted access.
            if (adminUserNames.Contains(userName))
            {
                return true;
            }

            AceCollection acl = DeserializeAcl(secDesc);
            foreach (AceStruct ace in acl)
            {
                if (string.Equals(userName, ace.PrincipalName, StringComparison.CurrentCultureIgnoreCase))
                {
                    foreach (DatasourceOperation aclOperation in
                       ace.DatasourceOperations)
                    {
                        if (aclOperation == requiredOperation)
                            return true;
                    }
                }
            }

            return false;
        }
    }
}
