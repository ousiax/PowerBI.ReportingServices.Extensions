using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Xml;
using Microsoft.ReportingServices.Interfaces;

namespace PowerBI.ReportingServices.Security
{
    public sealed partial class Authorization
    {
        private static readonly HashSet<string> adminUserNames = new HashSet<string>();

        private static Hashtable modelItemOperNames;
        private static Hashtable modelOperNames;
        private static Hashtable catOperNames;
        private static Hashtable fldOperNames;
        private static Hashtable rptOperNames;
        private static Hashtable resOperNames;
        private static Hashtable dsOperNames;

        private const int NrRptOperations = 27;
        private const int NrFldOperations = 10;
        private const int NrResOperations = 7;
        private const int NrDSOperations = 7;
        private const int NrCatOperations = 16;
        private const int NrModelOperations = 11;
        private const int NrModelItemOperations = 1;

        static Authorization()
        {
            InitializeMaps();
        }

        // Utility method used to create mappings to the various
        // operations in Reporting Services. These mappings support
        // the implementation of the GetPermissions method.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        private static void InitializeMaps()
        {
            // create model operation names data
            modelItemOperNames = new Hashtable();
            modelItemOperNames.Add(ModelItemOperation.ReadProperties,
               OperationNames.OperReadProperties);

            if (modelItemOperNames.Count != NrModelItemOperations)
            {
                //Model item name mismatch
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                    Resources.OperationNameError));
            }

            // create model operation names data
            modelOperNames = new Hashtable();
            modelOperNames.Add(ModelOperation.Delete,
               OperationNames.OperDelete);
            modelOperNames.Add(ModelOperation.ReadAuthorizationPolicy,
              OperationNames.OperReadAuthorizationPolicy);
            modelOperNames.Add(ModelOperation.ReadContent,
              OperationNames.OperReadContent);
            modelOperNames.Add(ModelOperation.ReadDatasource,
              OperationNames.OperReadDatasources);
            modelOperNames.Add(ModelOperation.ReadModelItemAuthorizationPolicies,
              OperationNames.OperReadModelItemSecurityPolicies);
            modelOperNames.Add(ModelOperation.ReadProperties,
              OperationNames.OperReadProperties);
            modelOperNames.Add(ModelOperation.UpdateContent,
              OperationNames.OperUpdateContent);
            modelOperNames.Add(ModelOperation.UpdateDatasource,
              OperationNames.OperUpdateDatasources);
            modelOperNames.Add(ModelOperation.UpdateDeleteAuthorizationPolicy,
              OperationNames.OperUpdateDeleteAuthorizationPolicy);
            modelOperNames.Add(ModelOperation.UpdateModelItemAuthorizationPolicies,
              OperationNames.OperUpdateModelItemSecurityPolicies);
            modelOperNames.Add(ModelOperation.UpdateProperties,
              OperationNames.OperUpdatePolicy);

            if (modelOperNames.Count != NrModelOperations)
            {
                //Model name mismatch
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                   Resources.OperationNameError));
            }

            // create operation names data
            catOperNames = new Hashtable();
            catOperNames.Add(CatalogOperation.CreateRoles,
               OperationNames.OperCreateRoles);
            catOperNames.Add(CatalogOperation.DeleteRoles,
               OperationNames.OperDeleteRoles);
            catOperNames.Add(CatalogOperation.ReadRoleProperties,
               OperationNames.OperReadRoleProperties);
            catOperNames.Add(CatalogOperation.UpdateRoleProperties,
               OperationNames.OperUpdateRoleProperties);
            catOperNames.Add(CatalogOperation.ReadSystemProperties,
               OperationNames.OperReadSystemProperties);
            catOperNames.Add(CatalogOperation.UpdateSystemProperties,
               OperationNames.OperUpdateSystemProperties);
            catOperNames.Add(CatalogOperation.GenerateEvents,
               OperationNames.OperGenerateEvents);
            catOperNames.Add(CatalogOperation.ReadSystemSecurityPolicy,
               OperationNames.OperReadSystemSecurityPolicy);
            catOperNames.Add(CatalogOperation.UpdateSystemSecurityPolicy,
               OperationNames.OperUpdateSystemSecurityPolicy);
            catOperNames.Add(CatalogOperation.CreateSchedules,
               OperationNames.OperCreateSchedules);
            catOperNames.Add(CatalogOperation.DeleteSchedules,
               OperationNames.OperDeleteSchedules);
            catOperNames.Add(CatalogOperation.ReadSchedules,
               OperationNames.OperReadSchedules);
            catOperNames.Add(CatalogOperation.UpdateSchedules,
               OperationNames.OperUpdateSchedules);
            catOperNames.Add(CatalogOperation.ListJobs,
               OperationNames.OperListJobs);
            catOperNames.Add(CatalogOperation.CancelJobs,
               OperationNames.OperCancelJobs);
            catOperNames.Add(CatalogOperation.ExecuteReportDefinition,
             OperationNames.ExecuteReportDefinition);
            if (catOperNames.Count != NrCatOperations)
            {
                //Catalog name mismatch
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                 Resources.OperationNameError));
            }

            fldOperNames = new Hashtable();
            fldOperNames.Add(FolderOperation.CreateFolder,
               OperationNames.OperCreateFolder);
            fldOperNames.Add(FolderOperation.Delete,
               OperationNames.OperDelete);
            fldOperNames.Add(FolderOperation.ReadProperties,
               OperationNames.OperReadProperties);
            fldOperNames.Add(FolderOperation.UpdateProperties,
               OperationNames.OperUpdateProperties);
            fldOperNames.Add(FolderOperation.CreateReport,
               OperationNames.OperCreateReport);
            fldOperNames.Add(FolderOperation.CreateResource,
               OperationNames.OperCreateResource);
            fldOperNames.Add(FolderOperation.ReadAuthorizationPolicy,
               OperationNames.OperReadAuthorizationPolicy);
            fldOperNames.Add(FolderOperation.UpdateDeleteAuthorizationPolicy,
               OperationNames.OperUpdateDeleteAuthorizationPolicy);
            fldOperNames.Add(FolderOperation.CreateDatasource,
               OperationNames.OperCreateDatasource);
            fldOperNames.Add(FolderOperation.CreateModel,
               OperationNames.OperCreateModel);
            if (fldOperNames.Count != NrFldOperations)
            {
                //Folder name mismatch
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                 Resources.OperationNameError));
            }

            rptOperNames = new Hashtable();
            rptOperNames.Add(ReportOperation.Delete,
               OperationNames.OperDelete);
            rptOperNames.Add(ReportOperation.ReadProperties,
               OperationNames.OperReadProperties);
            rptOperNames.Add(ReportOperation.UpdateProperties,
               OperationNames.OperUpdateProperties);
            rptOperNames.Add(ReportOperation.UpdateParameters,
               OperationNames.OperUpdateParameters);
            rptOperNames.Add(ReportOperation.ReadDatasource,
               OperationNames.OperReadDatasources);
            rptOperNames.Add(ReportOperation.UpdateDatasource,
               OperationNames.OperUpdateDatasources);
            rptOperNames.Add(ReportOperation.ReadReportDefinition,
               OperationNames.OperReadReportDefinition);
            rptOperNames.Add(ReportOperation.UpdateReportDefinition,
               OperationNames.OperUpdateReportDefinition);
            rptOperNames.Add(ReportOperation.CreateSubscription,
               OperationNames.OperCreateSubscription);
            rptOperNames.Add(ReportOperation.DeleteSubscription,
               OperationNames.OperDeleteSubscription);
            rptOperNames.Add(ReportOperation.ReadSubscription,
               OperationNames.OperReadSubscription);
            rptOperNames.Add(ReportOperation.UpdateSubscription,
               OperationNames.OperUpdateSubscription);
            rptOperNames.Add(ReportOperation.CreateAnySubscription,
               OperationNames.OperCreateAnySubscription);
            rptOperNames.Add(ReportOperation.DeleteAnySubscription,
               OperationNames.OperDeleteAnySubscription);
            rptOperNames.Add(ReportOperation.ReadAnySubscription,
               OperationNames.OperReadAnySubscription);
            rptOperNames.Add(ReportOperation.UpdateAnySubscription,
               OperationNames.OperUpdateAnySubscription);
            rptOperNames.Add(ReportOperation.UpdatePolicy,
               OperationNames.OperUpdatePolicy);
            rptOperNames.Add(ReportOperation.ReadPolicy,
               OperationNames.OperReadPolicy);
            rptOperNames.Add(ReportOperation.DeleteHistory,
               OperationNames.OperDeleteHistory);
            rptOperNames.Add(ReportOperation.ListHistory,
               OperationNames.OperListHistory);
            rptOperNames.Add(ReportOperation.ExecuteAndView,
               OperationNames.OperExecuteAndView);
            rptOperNames.Add(ReportOperation.CreateResource,
               OperationNames.OperCreateResource);
            rptOperNames.Add(ReportOperation.CreateSnapshot,
               OperationNames.OperCreateSnapshot);
            rptOperNames.Add(ReportOperation.ReadAuthorizationPolicy,
               OperationNames.OperReadAuthorizationPolicy);
            rptOperNames.Add(ReportOperation.UpdateDeleteAuthorizationPolicy,
               OperationNames.OperUpdateDeleteAuthorizationPolicy);
            rptOperNames.Add(ReportOperation.Execute,
               OperationNames.OperExecute);
            rptOperNames.Add(ReportOperation.CreateLink,
               OperationNames.OperCreateLink);

            if (rptOperNames.Count != NrRptOperations)
            {
                //Report name mismatch
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                 Resources.OperationNameError));
            }

            resOperNames = new Hashtable();
            resOperNames.Add(ResourceOperation.Delete,
               OperationNames.OperDelete);
            resOperNames.Add(ResourceOperation.ReadProperties,
               OperationNames.OperReadProperties);
            resOperNames.Add(ResourceOperation.UpdateProperties,
               OperationNames.OperUpdateProperties);
            resOperNames.Add(ResourceOperation.ReadContent,
               OperationNames.OperReadContent);
            resOperNames.Add(ResourceOperation.UpdateContent,
               OperationNames.OperUpdateContent);
            resOperNames.Add(ResourceOperation.ReadAuthorizationPolicy,
               OperationNames.OperReadAuthorizationPolicy);
            resOperNames.Add(ResourceOperation.UpdateDeleteAuthorizationPolicy,
               OperationNames.OperUpdateDeleteAuthorizationPolicy);

            if (resOperNames.Count != NrResOperations)
            {
                //Resource name mismatch
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                 Resources.OperationNameError));
            }

            dsOperNames = new Hashtable();
            dsOperNames.Add(DatasourceOperation.Delete,
               OperationNames.OperDelete);
            dsOperNames.Add(DatasourceOperation.ReadProperties,
               OperationNames.OperReadProperties);
            dsOperNames.Add(DatasourceOperation.UpdateProperties,
               OperationNames.OperUpdateProperties);
            dsOperNames.Add(DatasourceOperation.ReadContent,
               OperationNames.OperReadContent);
            dsOperNames.Add(DatasourceOperation.UpdateContent,
               OperationNames.OperUpdateContent);
            dsOperNames.Add(DatasourceOperation.ReadAuthorizationPolicy,
               OperationNames.OperReadAuthorizationPolicy);
            dsOperNames.Add(DatasourceOperation.UpdateDeleteAuthorizationPolicy,
               OperationNames.OperUpdateDeleteAuthorizationPolicy);

            if (dsOperNames.Count != NrDSOperations)
            {
                //Datasource name mismatch
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                 Resources.OperationNameError));
            }
        }

        /// <summary>
        /// Returns the set of permissions a specific user has for a specific 
        /// item managed in the report server database. This provides underlying 
        /// support for the Web service method GetPermissions().
        /// </summary>
        /// <param name="userName">The name of the user as returned by the 
        /// GetUserInfo method.</param>
        /// <param name="userToken">Pointer to the user ID returned by 
        /// GetUserInfo.</param>
        /// <param name="itemType">The type of item for which the permissions 
        /// are returned.</param>
        /// <param name="secDesc">The security descriptor associated with the 
        /// item.</param>
        /// <returns></returns>
        public StringCollection GetPermissions(string userName, IntPtr userToken,
           SecurityItemType itemType, byte[] secDesc)
        {
            StringCollection permissions = new StringCollection();
            if (adminUserNames.Contains(userName))
            {
                SetAdmin(permissions);
            }
            else
            {
                SetNormal(userName, secDesc, permissions);
            }

            return permissions;
        }

        private void SetNormal(string userName, byte[] secDesc, StringCollection permissions)
        {
            AceCollection acl = DeserializeAcl(secDesc);
            foreach (AceStruct ace in acl)
            {
                if (0 == String.Compare(userName, ace.PrincipalName, true, CultureInfo.CurrentCulture))
                {
                    foreach (ModelItemOperation aclOperation in ace.ModelItemOperations)
                    {
                        if (!permissions.Contains((string)modelItemOperNames[aclOperation]))
                            permissions.Add((string)modelItemOperNames[aclOperation]);
                    }
                    foreach (ModelOperation aclOperation in ace.ModelOperations)
                    {
                        if (!permissions.Contains((string)modelOperNames[aclOperation]))
                            permissions.Add((string)modelOperNames[aclOperation]);
                    }
                    foreach (CatalogOperation aclOperation in
                       ace.CatalogOperations)
                    {
                        if (!permissions.Contains((string)catOperNames[aclOperation]))
                            permissions.Add((string)catOperNames[aclOperation]);
                    }
                    foreach (ReportOperation aclOperation in ace.ReportOperations)
                    {
                        if (!permissions.Contains((string)rptOperNames[aclOperation]))
                            permissions.Add((string)rptOperNames[aclOperation]);
                    }
                    foreach (FolderOperation aclOperation in ace.FolderOperations)
                    {
                        if (!permissions.Contains((string)fldOperNames[aclOperation]))
                            permissions.Add((string)fldOperNames[aclOperation]);
                    }
                    foreach (ResourceOperation aclOperation in ace.ResourceOperations)
                    {
                        if (!permissions.Contains((string)resOperNames[aclOperation]))
                            permissions.Add((string)resOperNames[aclOperation]);
                    }
                    foreach (DatasourceOperation aclOperation in ace.DatasourceOperations)
                    {
                        if (!permissions.Contains((string)dsOperNames[aclOperation]))
                            permissions.Add((string)dsOperNames[aclOperation]);
                    }
                }
            }
        }

        private static void SetAdmin(StringCollection permissions)
        {
            foreach (CatalogOperation oper in catOperNames.Keys)
            {
                if (!permissions.Contains((string)catOperNames[oper]))
                    permissions.Add((string)catOperNames[oper]);
            }
            foreach (ModelItemOperation oper in modelItemOperNames.Keys)
            {
                if (!permissions.Contains((string)modelItemOperNames[oper]))
                    permissions.Add((string)modelItemOperNames[oper]);
            }
            foreach (ModelOperation oper in modelOperNames.Keys)
            {
                if (!permissions.Contains((string)modelOperNames[oper]))
                    permissions.Add((string)modelOperNames[oper]);
            }
            foreach (CatalogOperation oper in catOperNames.Keys)
            {
                if (!permissions.Contains((string)catOperNames[oper]))
                    permissions.Add((string)catOperNames[oper]);
            }
            foreach (ReportOperation oper in rptOperNames.Keys)
            {
                if (!permissions.Contains((string)rptOperNames[oper]))
                    permissions.Add((string)rptOperNames[oper]);
            }
            foreach (FolderOperation oper in fldOperNames.Keys)
            {
                if (!permissions.Contains((string)fldOperNames[oper]))
                    permissions.Add((string)fldOperNames[oper]);
            }
            foreach (ResourceOperation oper in resOperNames.Keys)
            {
                if (!permissions.Contains((string)resOperNames[oper]))
                    permissions.Add((string)resOperNames[oper]);
            }
            foreach (DatasourceOperation oper in dsOperNames.Keys)
            {
                if (!permissions.Contains((string)dsOperNames[oper]))
                    permissions.Add((string)dsOperNames[oper]);
            }
        }

        /// <summary>
        /// You must implement SetConfiguration as required by IExtension
        /// </summary>
        /// <param name="configuration">Configuration data as an XML
        /// string that is stored along with the Extension element in
        /// the configuration file.</param>
        public void SetConfiguration(string configuration)
        {
            // Retrieve admin user and password from the config settings
            // and verify
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(configuration);
            if (doc.DocumentElement.Name == "AdminConfiguration")
            {
                foreach (XmlNode child in doc.DocumentElement.ChildNodes)
                {
                    if (child.Name == "UserName")
                    {
                        var names = child.InnerText;
                        foreach (var name in names.Split(','))
                        {
                            adminUserNames.Add(name);
                        }
                    }
                    else
                    {
                        throw new Exception(string.Format(CultureInfo.InvariantCulture,
                          Resources.UnrecognizedElement));
                    }
                }
            }
            else
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                   Resources.AdminConfiguration));
            }
        }
    }
}
