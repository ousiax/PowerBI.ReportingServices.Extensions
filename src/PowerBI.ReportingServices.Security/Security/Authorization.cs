using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.ReportingServices.Interfaces;

namespace PowerBI.ReportingServices.Security
{
    public sealed partial class Authorization : IAuthorizationExtension
    {
        public string LocalizedName => null; // Return a localized name for this extension

        /// <summary>
        /// Returns a security descriptor that is stored with an individual 
        /// item in the report server database. 
        /// </summary>
        /// <param name="acl">The access code list (ACL) created by the report 
        /// server for the item. It contains a collection of access code entry 
        /// (ACE) structures.</param>
        /// <param name="itemType">The type of item for which the security 
        /// descriptor is created.</param>
        /// <param name="stringSecDesc">Optional. A user-friendly description 
        /// of the security descriptor, used for debugging. This is not stored
        /// by the report server.</param>
        /// <returns>Should be implemented to return a serialized access code 
        /// list for the item.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        public byte[] CreateSecurityDescriptor(
          AceCollection acl,
          SecurityItemType itemType,
          out string stringSecDesc)
        {
            // Creates a memory stream and serializes the ACL for storage.
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream result = new MemoryStream())
            {
                bf.Serialize(result, acl);
                stringSecDesc = null;
                return result.GetBuffer();
            }
        }

        // Used to deserialize the ACL that is stored by the report server.
        private AceCollection DeserializeAcl(byte[] secDesc)
        {
            AceCollection acl = new AceCollection();
            if (secDesc != null)
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream sdStream = new MemoryStream(secDesc))
                {
                    acl = (AceCollection)bf.Deserialize(sdStream);
                }
            }

            return acl;
        }
    }
}
